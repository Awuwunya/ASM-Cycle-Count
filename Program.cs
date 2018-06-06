using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asmCycleCount {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			if (args.Length == 0) {
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Window());

			} else {
				Window.now = true;
				Window.instructionCycles = !args[0].Equals("f");
				Window.instructionSizes = !args[1].Equals("f");
				Window.tx = args[2];
				Window.bw_DoWork(null, null);
				new GUIConsoleWriter().WriteLine(Window.cmd);
			}
		}
	}

	// This always writes to the parent console window and also to a redirected stdout if there is one.
	// It would be better to do the relevant thing (eg write to the redirected file if there is one, otherwise
	// write to the console) but it doesn't seem possible.
	public class GUIConsoleWriter {
		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		private static extern bool AttachConsole(int dwProcessId);

		private const int ATTACH_PARENT_PROCESS = -1;

		StreamWriter _stdOutWriter;

		// this must be called early in the program
		public GUIConsoleWriter() {
			// this needs to happen before attachconsole.
			// If the output is not redirected we still get a valid stream but it doesn't appear to write anywhere
			// I guess it probably does write somewhere, but nowhere I can find out about
			var stdout = Console.OpenStandardOutput();
			_stdOutWriter = new StreamWriter(stdout);
			_stdOutWriter.AutoFlush = true;

			AttachConsole(ATTACH_PARENT_PROCESS);
		}

		public void WriteLine(string line) {
			_stdOutWriter.WriteLine(line);
			Console.WriteLine(line);
		}
	}
}
