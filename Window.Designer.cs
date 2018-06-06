namespace asmCycleCount {
	partial class Window {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.AppMenu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.calculateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.instructionSizesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.instructionCyclesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.LoadLable = new System.Windows.Forms.ToolStripLabel();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.TextElement = new ScintillaNET.Scintilla();
			this.AppMenu.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TextElement)).BeginInit();
			// 
			// AppMenu
			// 
			this.AppMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.settingsToolStripMenuItem});
			this.AppMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.AppMenu.Location = new System.Drawing.Point(0, 0);
			this.AppMenu.Name = "AppMenu";
			this.AppMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.AppMenu.Size = new System.Drawing.Size(704, 23);
			this.AppMenu.TabIndex = 0;
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator5,
            this.calculateToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 19);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.cutToolStripMenuItem.Text = "Cut";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.pasteToolStripMenuItem.Text = "&Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(161, 6);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.selectAllToolStripMenuItem.Text = "Select &All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(161, 6);
			// 
			// calculateToolStripMenuItem
			// 
			this.calculateToolStripMenuItem.Name = "calculateToolStripMenuItem";
			this.calculateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.calculateToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.calculateToolStripMenuItem.Text = "Calculat&e";
			this.calculateToolStripMenuItem.Click += new System.EventHandler(this.calculateToolStripMenuItem_Click);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontToolStripMenuItem,
            this.instructionSizesToolStripMenuItem,
            this.instructionCyclesToolStripMenuItem});
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 19);
			this.settingsToolStripMenuItem.Text = "&Settings";
			// 
			// fontToolStripMenuItem
			// 
			this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
			this.fontToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.fontToolStripMenuItem.Text = "Font";
			this.fontToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem_Click);
			// 
			// TextElement
			// 
			this.TextElement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.TextElement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
			this.TextElement.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextElement.CallTip.BackColor = System.Drawing.Color.Maroon;
			this.TextElement.CallTip.ForeColor = System.Drawing.Color.SeaShell;
			this.TextElement.CallTip.HighlightTextColor = System.Drawing.Color.MediumVioletRed;
			this.TextElement.Caret.HighlightCurrentLine = true;
			this.TextElement.EndOfLine.Mode = ScintillaNET.EndOfLineMode.LF;
			this.TextElement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
			this.TextElement.Indentation.TabWidth = 16;
			this.TextElement.Lexing.Lexer = ScintillaNET.Lexer.Null;
			this.TextElement.Lexing.LexerName = "";
			this.TextElement.Lexing.LineCommentPrefix = "";
			this.TextElement.Lexing.StreamCommentPrefix = "";
			this.TextElement.Lexing.StreamCommentSufix = "";
			this.TextElement.Location = new System.Drawing.Point(12, 26);
			this.TextElement.Margins.Margin1.Width = 1;
			this.TextElement.MatchBraces = false;
			this.TextElement.Name = "TextElement";
			this.TextElement.Selection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(110)))));
			this.TextElement.Selection.BackColorUnfocused = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(72)))));
			this.TextElement.Size = new System.Drawing.Size(680, 408);
			this.TextElement.Styles.BraceBad.FontName = "Verdana0";
			this.TextElement.Styles.BraceLight.FontName = "Verdana";
			this.TextElement.Styles.CallTip.BackColor = System.Drawing.Color.Maroon;
			this.TextElement.Styles.CallTip.FontName = "Segoe UI";
			this.TextElement.Styles.CallTip.ForeColor = System.Drawing.Color.SeaShell;
			this.TextElement.Styles.ControlChar.FontName = "Verdana";
			this.TextElement.Styles.Default.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
			this.TextElement.Styles.Default.FontName = "Verdana";
			this.TextElement.Styles.IndentGuide.FontName = "Verdana";
			this.TextElement.Styles.LastPredefined.FontName = "Verdana";
			this.TextElement.Styles.LineNumber.FontName = "Verdana";
			this.TextElement.Styles.Max.FontName = "Verdana";
			this.TextElement.TabIndex = 2;
			this.TextElement.TabStop = false;
			this.TextElement.Caret.Color = System.Drawing.Color.White;
			// 
			// instructionSizesToolStripMenuItem
			// 
			this.instructionSizesToolStripMenuItem.Checked = true;
			this.instructionSizesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.instructionSizesToolStripMenuItem.Name = "instructionSizesToolStripMenuItem";
			this.instructionSizesToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.instructionSizesToolStripMenuItem.Text = "Instruction sizes";
			this.instructionSizesToolStripMenuItem.Click += new System.EventHandler(this.instructionSizesToolStripMenuItem_Click);
			// 
			// instructionCyclesToolStripMenuItem
			// 
			this.instructionCyclesToolStripMenuItem.Checked = true;
			this.instructionCyclesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.instructionCyclesToolStripMenuItem.Name = "instructionCyclesToolStripMenuItem";
			this.instructionCyclesToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.instructionCyclesToolStripMenuItem.Text = "Instruction cycles";
			this.instructionCyclesToolStripMenuItem.Click += new System.EventHandler(this.instructionCyclesToolStripMenuItem_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.LoadLable});
			this.toolStrip1.Location = new System.Drawing.Point(0, 437);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(704, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// progressBar
			// 
			this.progressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.progressBar.Margin = new System.Windows.Forms.Padding(1, 2, 10, 1);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(100, 22);
			// 
			// LoadLable
			// 
			this.LoadLable.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.LoadLable.Name = "LoadLable";
			this.LoadLable.Size = new System.Drawing.Size(26, 22);
			this.LoadLable.Text = "Idle";
			// 
			// fontDialog1
			// 
			this.fontDialog1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			// 
			// Window
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(704, 462);
			this.Controls.Add(this.TextElement);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.AppMenu);
			this.ForeColor = System.Drawing.Color.Black;
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(400, 300);
			this.Name = "Window";
			this.ShowIcon = false;
			this.Text = " ASM cycle calculator";
			this.TransparencyKey = System.Drawing.Color.LightPink;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.AppMenu.ResumeLayout(false);
			this.AppMenu.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.TextElement)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.MenuStrip AppMenu;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem calculateToolStripMenuItem;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripLabel LoadLable;
		private System.Windows.Forms.ToolStripProgressBar progressBar;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem;
		private System.Windows.Forms.FontDialog fontDialog1;
		private System.Windows.Forms.ToolStripMenuItem instructionSizesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem instructionCyclesToolStripMenuItem;
		private ScintillaNET.Scintilla TextElement;
	}
}

