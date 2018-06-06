using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using ScintillaNET;

namespace asmCycleCount {
	public partial class Window : Form {
		private static string url = "";
		private static readonly int SAVE_BIT_ASKOVERWRITE = 1;
		private static readonly int SAVE_BIT_OVERWRITE = 2;
		private static int maxoffs = 0;
		public static bool parse = true, instructionSizes, instructionCycles, now = false;
		private static Timer timer = new Timer();

		public Window() {
			InitializeComponent();

			// read settings file
			instructionSizes = instructionSizesToolStripMenuItem.Checked = Properties.Settings.Default.instructionSizes;
			instructionCycles = instructionCyclesToolStripMenuItem.Checked = Properties.Settings.Default.instructionCycles;

			// fonts
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
			TextElement.Font = (Font) converter.ConvertFromString(Properties.Settings.Default.Font);

			// last directory
			FileInfo f = new FileInfo(Properties.Settings.Default.LastOpenFile);
			if (f.Exists && (f.Attributes & FileAttributes.Directory) != FileAttributes.Directory) {
				TextElement.Text = File.ReadAllText(Properties.Settings.Default.LastOpenFile);
				url = Properties.Settings.Default.LastOpenFile;
			}

			if(Properties.Settings.Default.WinH != 0) {
				//	StartPosition = FormStartPosition.Manual;
				Location = new Point(Properties.Settings.Default.WinX, Properties.Settings.Default.WinY);
				Size = new Size(Properties.Settings.Default.WinH, Properties.Settings.Default.WinW);
				WindowState = FormWindowState.Normal;

				if(Properties.Settings.Default.Maximized) {
					Timer t = new Timer();
					t.Interval = 50;
					t.Tick += (a, b) => {
						Invoke(new MethodInvoker(delegate { WindowState = FormWindowState.Maximized; }));
						t.Stop();
					};
					t.Start();
				}
			}

			// resize event (for flags)
			Resize += Window_ResizeInit;
			Move += Window_Resize;

			// set progress bar stuff
			progressBar.Style = ProgressBarStyle.Blocks;

			// create backgroundworker
			bw = new BackgroundWorker();
			bw.WorkerSupportsCancellation = false;
			bw.WorkerReportsProgress = true;
			bw.DoWork += new DoWorkEventHandler(bw_DoWork);
			bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
			TextElement.TextChanged += TextElement_TextChanged;
			SetLineNumSize();

			// set TextElement style
			TextElement.Caret.CurrentLineBackgroundColor = Color.FromArgb(0x3c4b54);
		}

		private void Window_ResizeInit(object sender, EventArgs e) {
			Resize -= Window_ResizeInit;
			Resize += Window_Resize;
			
		}

		private void Window_Resize(object sender, EventArgs e) {
			Properties.Settings.Default.WinX = Location.X;
			Properties.Settings.Default.WinY = Location.Y;
			Properties.Settings.Default.WinH = Size.Width;
			Properties.Settings.Default.WinW = Size.Height;

			if(WindowState == FormWindowState.Maximized) {
				Properties.Settings.Default.Maximized = true;

			} else if(WindowState == FormWindowState.Normal) {
				Properties.Settings.Default.Maximized = false;
			}

			timer.Interval = 50;
			timer.Tick += (xsender, xe) => {
				Properties.Settings.Default.Save();
				timer.Stop();
			};
			timer.Start();
		}

		private void Timer_Tick(object sender, EventArgs e) {
			throw new NotImplementedException();
		}

		private void TextElement_TextChanged(object sender, EventArgs e) {
			SetLineNumSize();
		}

		private void SetLineNumSize() {
			int lines = TextElement.Text.Split('\n').Length, mx = 10, sz = 10;
			while (lines >= mx) {
				sz += 8;
				mx *= 10;
			}

			TextElement.Margins[0].Width = sz;
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) {
			TextElement.Selection.SelectAll();
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
			TextElement.Clipboard.Copy();
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e) {
			TextElement.Clipboard.Cut();
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
			TextElement.Clipboard.Paste();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			System.Windows.Forms.Application.Exit();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e) {
			url = "";
			TextElement.Text = "";
		}

		private void instructionSizesToolStripMenuItem_Click(object sender, EventArgs e) {
			Properties.Settings.Default.instructionSizes = instructionSizes = ((ToolStripMenuItem) sender).Checked = !instructionSizes;
			Properties.Settings.Default.Save();
		}

		private void instructionCyclesToolStripMenuItem_Click(object sender, EventArgs e) {
			Properties.Settings.Default.instructionCycles = instructionCycles = ((ToolStripMenuItem) sender).Checked = !instructionCycles;
			Properties.Settings.Default.Save();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e) {
			if (openFile()) {
				LoadLable.Text = "Loading file";
				TextElement.Text = File.ReadAllText(url);
				LoadLable.Text = "Idle";
			}
		}

		private void fontToolStripMenuItem_Click(object sender, EventArgs e) {
			LoadLable.Text = "Waiting for font selection";
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
			fontDialog1.Font = (Font) converter.ConvertFromString(Properties.Settings.Default.Font);

			if (fontDialog1.ShowDialog() == DialogResult.OK & !String.IsNullOrEmpty(TextElement.Text)) {
				TextElement.Font = fontDialog1.Font;

				// we want to store the font settings too
				Properties.Settings.Default.Font = converter.ConvertToString(fontDialog1.Font);
				Properties.Settings.Default.Save();
			}

			LoadLable.Text = "Idle";
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
			if (url.Equals("")) saveAsToolStripMenuItem_Click(sender, e);

			if (shouldOverwriteFile()) {
				LoadLable.Text = "Saving file";
				File.WriteAllText(url, TextElement.Text);
			}

			LoadLable.Text = "Idle";
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
			if (saveFile() && shouldOverwriteFile()) {
				LoadLable.Text = "Saving file";
				File.WriteAllText(url, TextElement.Text);
			}

			LoadLable.Text = "Idle";
		}

		private bool shouldOverwriteFile() {
			// if file doesnt exist, then we can write
			if (!File.Exists(url)) return true;

			// else check settings and open dialog if unclear
			if ((Properties.Settings.Default.SaveDialogSettings & SAVE_BIT_ASKOVERWRITE) == 0) {
				LoadLable.Text = "Waiting for dialog";
				OverWriteForm owf = new OverWriteForm(url);
				DialogResult result = owf.ShowDialog();

				if (owf.remember) {
					if (result == DialogResult.Yes) {
						Properties.Settings.Default.SaveDialogSettings = (byte) (SAVE_BIT_OVERWRITE | SAVE_BIT_ASKOVERWRITE);

					} else if (result == DialogResult.No) {
						Properties.Settings.Default.SaveDialogSettings = (byte) (SAVE_BIT_ASKOVERWRITE);
					}

				} else {
					Properties.Settings.Default.SaveDialogSettings = 0;
				}

				Properties.Settings.Default.Save();
				return result == DialogResult.Yes;

			} else {
				return (Properties.Settings.Default.SaveDialogSettings & SAVE_BIT_OVERWRITE) != 0;
			}
		}

		private bool saveFile() {
			LoadLable.Text = "Waiting for file selection";
			SaveFileDialog theDialog = new SaveFileDialog();
			theDialog.OverwritePrompt = false;  // do not warn user about overwriting the file, we'll do it later
			theDialog.Title = "Open Text Documents";
			theDialog.Filter = "All Text Documents|*.txt;*.asm;*.68k|All Files|*.*";
			theDialog.InitialDirectory = Path.GetDirectoryName(Properties.Settings.Default.LastSaveFile);

			if (theDialog.ShowDialog() == DialogResult.OK) {
				url = theDialog.FileName;
				Properties.Settings.Default.LastSaveFile = theDialog.FileName;
				Properties.Settings.Default.Save();
				return true;

			} else {
				return false;
			}
		}

		private bool openFile() {
			LoadLable.Text = "Waiting for file selection";
			OpenFileDialog theDialog = new OpenFileDialog();
			theDialog.Title = "Open Text Documents";
			theDialog.Filter = "All Text Documents|*.txt;*.asm;*.68k|All Files|*.*";
			theDialog.InitialDirectory = Path.GetDirectoryName(Properties.Settings.Default.LastOpenFile);

			if (theDialog.ShowDialog() == DialogResult.OK) {
				url = theDialog.FileName;
				Properties.Settings.Default.LastOpenFile = theDialog.FileName;
				Properties.Settings.Default.Save();
				return true;

			} else {
				return false;
			}
		}

		private void calculateToolStripMenuItem_Click(object sender, EventArgs e) {
			if (bw.IsBusy != true) {
				tx = TextElement.Text;
				bw.RunWorkerAsync();
			}
		}

		private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			progressBar.Value = e.ProgressPercentage;

			if (e.UserState != null && e.UserState is string) {
				LoadLable.Text = e.UserState as string;

				if(((string)e.UserState).Equals("Writing file")) {
					TextElement.ResetText();

				} else if(((string) e.UserState).StartsWith("Append ")) {
					TextElement.AppendText(((string) e.UserState).Substring(7));
				}

			} else if (e.UserState != null && e.UserState is int) {
				progressBar.Maximum = (int)e.UserState;
			}
		}

		public static string cmd;
		public static string tx;
		private static List<Instruction> list;
		public static void bw_DoWork(object sender, DoWorkEventArgs e) {
			BackgroundWorker worker = sender as BackgroundWorker;
			// Perform a time consuming operation and report progress.
			try {
				parse = true;
				if (worker != null) {
					worker.ReportProgress(0, "Interpreting file");
					worker.ReportProgress(0, (int) tx.Split('\n').Length);

				} else {
					cmd = "";
				}

				int lnum = -1;
				list = new List<Instruction>();

				// parse the text file
				foreach (string line in tx.Replace("\r", "").Split('\n')) {
					++lnum;
					// set progress bar value
					if (worker != null) worker.ReportProgress(lnum);
					// parse instruction
					Instruction ins = getInstruction(line, lnum);

					// if instruction is null or parse failed, reset progress and quit
					if (ins == null || !parse) {
						if (worker != null) worker.ReportProgress(0, "Idle");
						return;
					}

					list.Add(ins);

					int len = FixTabs(ins.getLine(), 0).Length;
					// set max size of a line
					if (maxoffs < len) maxoffs = len;
				}

				// align to next tab size
				maxoffs = maxoffs + (8 - (maxoffs % 8));
				if (worker != null) worker.ReportProgress(0, "Writing file");
				int lbl = lnum = 0;
				// write instructions back to the text field
				foreach (Instruction i in list) {
					if (lnum < i.getLineNum()) {
						// if the line num of the instruction is higher than current line num, instead newline
						if (worker != null) worker.ReportProgress(lnum, "Append \r\n");
						else cmd += "\r\n";
						lnum = i.getLineNum();
						lbl = 0;
					}

					// append instruction
					if (worker != null) worker.ReportProgress(lnum, "Append "+ i.getLine());
					else cmd += i.getLine();
					lbl += FixTabs(i.getLine(), lbl).Length;

					if (i.hasCycles() || i.hasBytes()) {
						// process tab amount
						int t = (maxoffs - lbl) / 8;
						string o = "";
						for (int z = 0;z <= t;z ++) o += '\t';

						// write cycle count
						if (instructionSizes && instructionCycles && i.hasCycles() && i.hasBytes()) {
							if (worker != null) worker.ReportProgress(lnum, "Append " + o + "; " + i.getByteCount() + " bytes " + i.getCycles() + '(' + i.getReadCycles() + '/' + i.getWriteCycles() + ") " + i.getNote());
							else cmd += o + "; " + i.getByteCount() + " bytes " + i.getCycles() + '(' + i.getReadCycles() + '/' + i.getWriteCycles() + ") " + i.getNote();

						} else if (instructionSizes && i.hasBytes()) {
							if (worker != null) worker.ReportProgress(lnum, "Append " + o + "; " + i.getByteCount() + " bytes "+ i.getNote());
							else cmd += o + "; " + i.getByteCount() + " bytes " + i.getNote();

						} else if (instructionCycles && i.hasCycles()) {
							if (worker != null) worker.ReportProgress(lnum, "Append " + o + "; "+ i.getCycles() + '(' + i.getReadCycles() + '/' + i.getWriteCycles() + ") " + i.getNote());
							else cmd += o + "; " + i.getCycles() + '(' + i.getReadCycles() + '/' + i.getWriteCycles() + ") " + i.getNote();
						}

					} else if (!i.getNote().Equals("")) {
						// write note only
						if (worker != null) worker.ReportProgress(lnum, "Append \t; " + i.getNote());
						else cmd += "\t; " + i.getNote();
					}
				}
				
				if (worker != null) worker.ReportProgress(0, "Idle");

			} catch (Exception ex) {
				ShowParseError(-1, ex.ToString(), "Error!");
			}

			list = null;
			maxoffs = 0;
		}

		private static string FixTabs(string s, int off) {
			while(s.Contains('\t')) {
				int x = s.IndexOf('\t');
				if (s.ElementAt(x) == '\t') {
					string t = "";
					int p = 8 - ((x + off) % 8);
					for (int z = 0;z < p;z ++) t += ' ';

					if(x > 0) {
						s = s.Substring(0, x) + t + s.Substring(x + 1);

					} else {
						s = t + s.Substring(x + 1);
					}
				}
			}

			return s;
		}

		private static Instruction getInstruction(string line, int lnum) {
			List<string> parts = new List<string>();

			int x = 0, i;
			uint tabs = 0;
			bool haslable = false, hasRightParen = false;
			for (i = 0;i < line.Length && parse;i++) {
				char c = line.ElementAt(i);

				if (c == ';') {
					// this means its a comment
					if (parts.Count == 0) return new Comment(lnum, line);
					// if the last part is not split, split it
					if (i > x + 1) {
						parts.Add(line.Substring(x, i - x));
						if (x == 0) haslable = true;
					}

					parts.Add(line.Substring(i - 1, line.Length - i + 1));
					x = i = line.Length;

				} else if (!hasRightParen && (c == ' ' || c == '\t' || c == ',')) {
					// split a part
					if (i > x) {
						parts.Add(line.Substring(x, i - x));
						if (x == 0) haslable = true;
					}

					if ((parts.Count == 0 || (haslable && parts.Count <= 1)) && c == '\t') {
						tabs++;
					}

					if (i <= x && c == ',') {
						ShowParseError(lnum, "Could not determine argument with length under 1!\nParsing will now terminate.", "Error!");
						return null;
					}

					x = i + 1;

				} else if (hasRightParen && c == ')') {
					hasRightParen = false;

				} else if (c == '(') {
					hasRightParen = true;
				}
			}

			// if the last part is not split, split it
			if (i > x && x + (i - x) <= line.Length) {
				parts.Add(line.Substring(x, i - x));
				if (x == 0) haslable = true;
			}

			// if there are 0 parts, return a newline
			if (parts.Count == 0) return new NullInstruction(lnum);
			// if there is 1 part and a lable, return a lable
			if (haslable && parts.Count <= 1) return new Lable(lnum, parts[0]);
			// else add lable to the list and then remove it from the parts list
			else if (haslable) {
				list.Add(new Lable(lnum, parts[0]));
				parts.RemoveAt(0);
			}

			string ins;
			char size = '\0';

			// process the size and exact instruction
			if (parts[0].Contains('.')) {
				string xy = parts[0];
				ins = parts[0].Substring(0, parts[0].IndexOf('.'));
				size = xy.Replace(ins, "").Replace(".", "").ElementAt(0);

			} else {
				ins = parts[0];
			}

			// remove instruction
			parts.RemoveAt(0);
			switch (ins.ToLower()) {
				case "":
					ShowParseError(lnum, "Could not determine instruction with length 0!\nParsing will now terminate.", "Error!");
					return null;

				case "nop":
					return new Nop(lnum, tabs, parseArgs(lnum, parts, size));

				case "rts":case "rte":case "rtr":
					return new Rtx(lnum, tabs, ins.ElementAt(2), parseArgs(lnum, parts, size));

				case "illegal":
					return new Illegal(lnum, tabs, parseArgs(lnum, parts, size));

				case "reset":
					return new Reset(lnum, tabs, parseArgs(lnum, parts, size));

				case "trapv":
					return new Trapv(lnum, tabs, parseArgs(lnum, parts, size));

				case "trap":
					return new Trap(lnum, tabs, parseArgs(lnum, parts, size));

				case "stop":
					return new Stop(lnum, tabs, parseArgs(lnum, parts, size));

				case "unlk":
					return new Unlink(lnum, tabs, parseArgs(lnum, parts, size));

				case "link":
					return new Link(lnum, tabs, parseArgs(lnum, parts, size));

				case "swap":
					return new Swap(lnum, tabs, parseArgs(lnum, parts, size));

				case "ext":
					return new Ext(lnum, tabs, size, parseArgs(lnum, parts, size));

				case "jmp":case "jsr":
					return new Jump(lnum, tabs, ins, parseArgs(lnum, parts, size));

				case "clr":case "neg":case "not":case "negx":
					return new Clr(lnum, tabs, size, ins, parseArgs(lnum, parts, size));

				case "tas":
					return new Tas(lnum, tabs, parseArgs(lnum, parts, size));

				case "tst":
					return new Tst(lnum, tabs, size, parseArgs(lnum, parts, size));

				case "st":case "sf":case "svs":case "svc":case "scc":case "scs":case "smi":case "spl":case "seq":
				case "sne":case "sgt":case "sge":case "slt":case "sle":case "shs":case "shi":case "sls":case "slo":
					return new Scc(lnum, tabs, ins, parseArgs(lnum, parts, size));

				case "pea":
					return new Pea(lnum, tabs, parseArgs(lnum, parts, size));

				case "lea":
					return new Lea(lnum, tabs, parseArgs(lnum, parts, size));

				case "chk":
					return new Chk(lnum, tabs, parseArgs(lnum, parts, size));

				case "asr":case "asl":case "lsr":case "lsl":case "ror":case "rol":case "roxr":case "roxl":
					return new BitShift(lnum, tabs, ins, size, parseArgs(lnum, parts, size));

				case "btst":case "bclr":case "bchg":case "bset":
					return new Bit(lnum, tabs, ins, parseArgs(lnum, parts, size));

				case "exg":
				    return new Exg(lnum, tabs, parseArgs(lnum, parts, size));

				case "subq":case "addq":
				    return new SAddq(lnum, tabs, size, ins, parseArgs(lnum, parts, size));

				case "muls":case "mulu":case "divu":case "divs":
				    return new MulDiv(lnum, tabs, ins, size, parseArgs(lnum, parts, '\0'));

				case "xor":case "eor":case "xori":case "eori":
					ArgumentPart[] p3 = parseArgs(lnum, parts, size);
					if (ArgEnum.get(p3[1]).Type == typeof(ArgCCR) || ArgEnum.get(p3[1]).Type == typeof(ArgSR)) {
						return new AndOrEorCCRorSR(lnum, tabs, size, ins, p3);

					} else {
						return new Xor(lnum, tabs, size, ins, p3);
					}

				case "cmp":case "cmpi":case "cmpa":
					return new Cmp(lnum, tabs, size, ins, parseArgs(lnum, parts, size));

				case "add":case "addi":case "adda":
				case "sub":case "subi":case "suba":
					return new AddSub(lnum, tabs, size, ins, parseArgs(lnum, parts, size));

				case "and":case "andi":
				case "or":case "ori":
					ArgumentPart[] p1 = parseArgs(lnum, parts, size);
					if (ArgEnum.get(p1[1]).Type == typeof(ArgCCR) || ArgEnum.get(p1[1]).Type == typeof(ArgSR)) {
						return new AndOrEorCCRorSR(lnum, tabs, size, ins, p1);

					} else {
						return new AndOr(lnum, tabs, size, ins, p1);
					}

				case "move":case "movea":
					ArgumentPart[] p2 = parseArgs(lnum, parts, size);
					if (p2.Length == 2 && !ins.EndsWith("a") && (ArgEnum.get(p2[1]).Type == typeof(ArgCCR) || ArgEnum.get(p2[1]).Type == typeof(ArgSR) || ArgEnum.get(p2[1]).Type == typeof(ArgUSP) ||
						ArgEnum.get(p2[0]).Type == typeof(ArgSR) || ArgEnum.get(p2[0]).Type == typeof(ArgUSP))) {
						return new MoveToCCRSRUSP(lnum, tabs, size, p2);

					} else {
						return new Move(lnum, tabs, size, ins, p2);
					}

				case "moveq":
					return new Moveq(lnum, tabs, parseArgs(lnum, parts, size));

				case "movep":
					return new MoveP(lnum, tabs, size, parseArgs(lnum, parts, size));

				case "movem":
					return new Movem(lnum, tabs, size, parseArgs(lnum, parts, size));

				case "abcd":case "sbcd":
					return new ASbcd(lnum, tabs, ins, parseArgs(lnum, parts, size));

				case "subx":case "addx":
					return new ASddx(lnum, tabs, ins, size, parseArgs(lnum, parts, size));

				case "nbcd":
					return new Nbcd(lnum, tabs, parseArgs(lnum, parts, size));

				case "cmpm":
					return new Cmpm(lnum, tabs, size, parseArgs(lnum, parts, size));

				case "bra":case "bvs":case "bvc":case "bcc":case "bcs":case "bmi":case "bpl":case "beq":case "bne":
				case "bsr":case "bgt":case "bge":case "blt":case "ble":case "bhs":case "bhi":case "bls":case "blo":
					return new Bcc(lnum, tabs, ins, size, parseArgs(lnum, parts, size));

				case "dbt":case "dbf":case "dbra":case "dbvs":case "dbvc":case "dbcc":case "dbcs":case "dbmi":case "dbpl":case "dbeq":
				case "dbne":case "dbgt":case "dbge":case "dblt":case "dble":case "dbhs":case "dbhi":case "dbls":case "dblo":
					return new Dbcc(lnum, tabs, ins, parseArgs(lnum, parts, size));

				case "dc":case "dcb":
					return new Data(lnum, tabs, ins, size, parseArgs(lnum, parts, size));

				default:
					ShowParseError(lnum, "Could not recognize instruction '" + ins + "'!\nParsing will now terminate.", "Error!");
					return null;
			}
		}

		// our patternmatching to allow us to determine the rest of the arguments
		private static Regex[] regex = {
			new Regex(@"^([a-z0-9_\$\%\+\-\*\/\(\)\@\.]*)\(a([0-7]),d([0-7])\.w\)$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// d8(An,ix.w)
			new Regex(@"^([a-z0-9_\$\%\+\-\*\/\(\)\@\.]*)\(a([0-7]),d([0-7])\.l\)$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// d8(An,ix.l)
			new Regex(@"^([a-z0-9_\$\%\+\-\*\/\(\)\@\.]*)\(a([0-7]),d([0-7])\)$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// d8(An,ix)
			new Regex(@"^([a-z0-9_\$\%\+\-\*\/\(\)\@\.]*)\(PC,d([0-7])\.w\)$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// d8(PC,ix.w)
			new Regex(@"^([a-z0-9_\$\%\+\-\*\/\(\)\@\.]*)\(PC,d([0-7])\.l\)$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// d8(PC,ix.l)
			new Regex(@"^([a-z0-9_\$\%\+\-\*\/\(\)\@\.]*)\(PC,d([0-7])\)$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// d8(PC,ix)
			new Regex(@"^([a-z0-9_\$\%\+\-\*\/\(\)\@\.]*)\(PC\)$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// d16(PC)
			new Regex(@"^\(a([0-7])\)\+$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// (An)+
			new Regex(@"^\-\(a([0-7])\)$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// -(An)
			new Regex(@"^\(a([0-7])\)$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// (An)
			new Regex(@"^([a-z0-9\$\%\+\-\*\/\(\)\@\.]+)\(a([0-7])\)$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// d16(An)
			new Regex(@"^\(d([0-7])\)$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// (Dn)
			new Regex(@"^a([0-7])$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// An
			new Regex(@"^d([0-7])$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// Dn
			new Regex(@"^([ad]{1})([0-7]{1})([\/\-]{1})([ad0-7\/\-]*)$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// whatever movem uses
			new Regex(@"^ccr$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// ccr
			new Regex(@"^sr$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// sr
			new Regex(@"^usp$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// usp
			new Regex(@"^([a-z0-9\$\%\+\-\*\/\(\)_\@\.]*).w$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// xxx.w
			new Regex(@"^([a-z0-9\$\%\+\-\*\/\(\)_\@\.]*).l$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// xxx.L
			new Regex(@"^([a-z0-9\$\%\+\-\*\/\(\)_\@\.]*)$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// xxx
			new Regex(@"^(.)*$",RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace),	// anything (usually lable)
		};

		private static Regex cycleRegex = new Regex(@"([\t]*)\;\s([0-9]+)\sbytes\s([0-9]+)\(([0-9]+)\/([0-9]+)\)([\s]*)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

		// this is the code that deals with creating proper objects for above regexes
		private static Func<int, string, char, int, ArgumentPart>[] funcs = {
					(lnum, data, size, index) => { return new ArgAddressIndirectWithIndex(lnum, data, size); },	// d8(An,ix.w)
			(lnum, data, size, index) => { return new ArgAddressIndirectWithIndex(lnum, data, size); },	// d8(An,ix.l)
			(lnum, data, size, index) => { return new ArgAddressIndirectWithIndex(lnum, data, size); },	// d8(An,ix)
			(lnum, data, size, index) => { return new ArgPCRelativeWithIndex(lnum, data, size); },	// d8(PC,ix.w)
			(lnum, data, size, index) => { return new ArgPCRelativeWithIndex(lnum, data, size); },	// d8(PC,ix.l)
			(lnum, data, size, index) => { return new ArgPCRelativeWithIndex(lnum, data, size); },	// d8(PC,ix)
			(lnum, data, size, index) => { return new ArgPCRelativeWithOffset(lnum, data, size); },	// d16(PC)
			(lnum, data, size, index) => { return new ArgAddressIndirectWithPostIncrement(lnum, data, size); },	// (An)+
			(lnum, data, size, index) => { return new ArgAddressIndirectWithPreDecrement(lnum, data, size); },	// -(An)
			(lnum, data, size, index) => { return new ArgAddressIndirect(lnum, data, size); },	// (An)
			(lnum, data, size, index) => { return new ArgAddressIndirectWithOffset(lnum, data, size); },	// d16(An)
			(lnum, data, size, index) => {
				ShowParseError(lnum, "M68000 does not support Data Register Indirect mode!\nParsing will now terminate.", "Error!");
				return null;
			},	// (Dn)
			(lnum, data, size, index) => { return new ArgAddressDirect(lnum, data, size); },	// An
			(lnum, data, size, index) => { return new ArgDataDirect(lnum, data, size); },	// Dn
			(lnum, data, size, index) => { return new ArgMovemReg(lnum, data, size, index); },	// whatever movem uses
			(lnum, data, size, index) => { return new ArgCCR(lnum, data, size); },	// CCR
			(lnum, data, size, index) => { return new ArgSR(lnum, data, size); },	// SR
			(lnum, data, size, index) => { return new ArgUSP(lnum, data, size); },	// USP
			(lnum, data, size, index) => { return new ArgAbsoluteShort(lnum, data, size); },	// xxx.w
			(lnum, data, size, index) => { return new ArgAbsoluteLong(lnum, data, size); },	// xxx.l
			(lnum, data, size, index) => { return new ArgAbsoluteLong(lnum, data, size); },	// xxx
			(lnum, data, size, index) => { return new ArgLable(lnum, data, size); },	// anything (usually lable)
		};
		private BackgroundWorker bw;

		private static ArgumentPart[] parseArgs(int lnum, List<string> parts, char size) {
			ArgumentPart[] ret = new ArgumentPart[parts.Count];

			for (int i = 0;i < parts.Count && parse;i++) {
				char c = parts[i].ElementAt(0);

				if (c == '#') {
					ret[i] = new ArgImmediateValue(lnum, parts[i], size);

				} else if (parts[i].Contains(';')) {
					if (parts[i].StartsWith("\t")) {
						if(cycleRegex.Matches(parts[i]).Count > 0) {
							// this is comment about cycle counts more likely. Try to remove it.
							ArgumentPart[] p = ret;
							ret = new ArgumentPart[p.Length - 1];

							for(int q = 0;q < i;q++) {
								ret[q] = p[q];
							}

						} else {
							// not cycle count comment
							ret[i] = new Comment(lnum, parts[i]);
						}

					} else {
						ret[i] = new Comment(lnum, parts[i]);
					}

				} else {
					bool hadSP = false;
					// change sp to a7 so we can deal with it easier
					if(!parts[i].ToLower().Contains("usp") && parts[i].ToLower().Contains("sp")){
						parts[i] = parts[i].ToLower().Replace("sp", "a7");
						hadSP = true;
					}

					// we found no 'easy' match, therefore attempt patternmatching via regex
					int rx = -1;
					foreach(Regex r in regex) {
						++rx;
						if (r.Matches(parts[i]).Count > 0) {
							ret[i] = funcs[rx](lnum, hadSP ? parts[i].Replace("a7", "sp") : parts[i], size, i);
							break;
						}
					}

					if(ret[i] == null) {
						ShowParseError(lnum, "Could not find regex that matches '"+ parts[i] + "'!\nParsing will now terminate.", "Error!");
						return null;
					}
				}
			}

			return ret;
		}

		private static int getArgNum(List<string> parts) {
			int arg = 0;
			foreach(string s in parts) {
				if (!s.Contains(';')){
					arg++;

				} else {
					return arg;
				}
			}

			return arg;
		}

		public static int getArgNum(ArgumentPart[] parts) {
			int arg = 0;
			foreach (ArgumentPart s in parts) {
				if (s.GetType() != typeof(Comment)) {
					arg++;

				} else {
					return arg;
				}
			}

			return arg;
		}

		public static void ShowParseError(int lnum, string error, string note) {
			string start;
			if (url.Equals("")) {
				if (lnum == -1) {
					start = "";

				} else {
					start = "line " + (lnum + 1) + ": ";
				}

			} else if (lnum == -1) {
				start = Path.GetFileName(url) + ": ";

			} else {
				start = Path.GetFileName(url) +":line " + (lnum + 1) + ": ";
			}

			if(!now) MessageBox.Show(start + error, note, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			else cmd += start + error;
			parse = false;
		}
	}
}
