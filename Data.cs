using System;

namespace asmCycleCount {
	internal class Data : GenericInstruction {
		private int lnum;
		private char size;
		private uint tabs, argnum, arg;
		private string ins, d;

		public Data(int lnum, uint tabs, string ins, char size, ArgumentPart[] args) : base(lnum, tabs, args) {
			this.lnum = lnum;
			this.tabs = tabs;
			this.size = size;
			this.ins = ins;
			argnum = (uint) Window.getArgNum(args);
			if (ins == "dc") {
				arg = argnum;

			} else {
				arg = 1;
				d = args[0].getLine();
			}
		}

		public override uint getArgNum() {
			return argnum;
		}

		public override uint getBytesCount() {
			return 0;
		}

		public override uint getByteCount() {
			switch (size) {
				case 'b':case '1':
					return arg;

				case 'w':case '\0':case '2':
					return arg * 2;

				case 'l':case '4':
					return arg * 4;
			}

			return 0;
		}

		public override string getInsName() {
			return ins;
		}

		public override char getInsSize() {
			return size;
		}

		public override string getNote() {
			if(arg == argnum) return "";
			else return "* "+ d;
		}

		public override bool hasCycles() {
			return false;
		}

		public override uint getCycleCount() {
			return 0;
		}

		public override uint getReadCycleCount() {
			return 0;
		}

		public override uint getWriteCycleCount() {
			return 0;
		}

		public override bool overridesCycles() {
			return true;
		}
	}
}