using System;

namespace asmCycleCount {
	internal class Lable : Instruction {
		private int lnum;
		private string lable;

		public Lable(int lnum, string labl) {
			this.lnum = lnum;
			lable = labl;
		}

		public int getLineNum() {
			return lnum;
		}

		public uint getArgNum() {
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

		public uint getByteCount() {
			return 0;
		}

		public string getLine() {
			return lable;
		}

		public string getNote() {
			return "";
		}
	}
}