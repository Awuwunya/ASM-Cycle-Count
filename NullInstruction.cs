using System;

namespace asmCycleCount {
	internal class NullInstruction : Instruction {
		private int lnum;

		public NullInstruction(int linenum) {
			lnum = linenum;
		}

		public int getLineNum() {
			return lnum;
		}

		public uint getArgNum() {
			return 0;
		}

		public uint getByteCount() {
			return 0;
		}

		public bool hasCycles() {
			return false;
		}

		public bool hasBytes() {
			return false;
		}

		public uint getCycles() {
			return 0;
		}

		public uint getReadCycles() {
			return 0;
		}

		public uint getWriteCycles() {
			return 0;
		}

		public string getLine() {
			return "";
		}

		public string getNote() {
			return "";
		}
	}
}