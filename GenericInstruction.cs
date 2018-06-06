using System;

namespace asmCycleCount {
	internal abstract class GenericInstruction : Instruction {
		private int lnum;
		private ArgumentPart[] args;
		private uint tabs;

		public GenericInstruction(int lnum, uint priortabs, ArgumentPart[] args) {
			this.lnum = lnum;
			this.args = args;
			this.tabs = priortabs;
		}

		public bool checkArgNums() {
			if(getArgNum() == Window.getArgNum(args)) {
				return true;

			} else {
				string i = getInsName();
				if(i == null) {
					i = "null";
				}

				Window.ShowParseError(lnum, i.ToUpper() +" instruction requires " + getArgNum() + " arguments, but " + Window.getArgNum(args) + " were found!\nParsing will now terminate.", "Error!");
				return false;
			}
		}

		public abstract uint getArgNum();

		public virtual int getLineNum() {
			return lnum;
		}

		public virtual bool hasCycles() {
			return true;
		}

		public virtual bool hasBytes() {
			return true;
		}

		public virtual uint getCycles() {
			uint c = getCycleCount();

			if (!overridesCycles()) {
				foreach (ArgumentPart a in args) {
					if (a.hasCycles()) c += a.getCycles();
				}
			}

			return c;
		}

		public virtual uint getReadCycles() {
			uint c = getReadCycleCount();

			if (!overridesCycles()) {
				foreach (ArgumentPart a in args) {
					if (a.hasCycles()) c += a.getReadCycles();
				}
			}

			return c;
		}

		public virtual uint getWriteCycles() {
			uint c = getWriteCycleCount();

			if (!overridesCycles()) {
				foreach (ArgumentPart a in args) {
					if (a.hasCycles()) c += a.getWriteCycles();
				}
			}

			return c;
		}

		// method which is used to get cycle count of the instruction
		public abstract uint getCycleCount();

		// method which is used to get read cycle count of the instruction
		public abstract uint getReadCycleCount();

		// method which is used to get write cycle count of the instruction
		public abstract uint getWriteCycleCount();

		// method which is used to check if instruction wants to overrid cycle counts
		public abstract bool overridesCycles();

		public virtual uint getByteCount() {
			uint b = getBytesCount();

			foreach (ArgumentPart a in args) {
				b += a.getByteCount();
			}

			return b;
		}

		// method which is used to get number of bytes the instruction needs
		public abstract uint getBytesCount();

		public virtual string getLine() {
			string o = "";

			for(int z = 0;z < tabs;z++) {
				o += '\t';
			}

			if (getInsSize() == '\0') {
				o += getInsName() +'\t';

			} else {
				o += getInsName() + '.' + getInsSize() + '\t';
			}

			uint i = getArgNum() - 1, x = 0;
			foreach(ArgumentPart a in args) {
				o += a.getLine();
				x++;

				if (i > 0) {
					o += ',';
					i--;

				} else if (x != args.Length) o += '\t';
			}

			return o;
		}

		// method which is used to define instruction name
		public abstract string getInsName();

		// method which is used to define instruction size
		public abstract char getInsSize();

		public abstract string getNote();
	}
}