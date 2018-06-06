using System;

namespace asmCycleCount {
	public abstract class ArgumentPart : Instruction {
		public virtual int getLineNum() {
			return -1;
		}

		public uint getArgNum() {
			return 0;
		}

		public string getNote() {
			return "";
		}

		public virtual bool hasBytes() {
			return false;
		}
		public abstract bool hasCycles();
		public abstract uint getCycles();
		public abstract uint getReadCycles();
		public abstract uint getWriteCycles();
		public abstract uint getByteCount();
		public abstract string getLine();
	}
}