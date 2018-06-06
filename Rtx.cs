namespace asmCycleCount {
	internal class Rtx : GenericInstruction {
		private char ins;

		public Rtx(int lnum, uint tabs, char ins, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.ins = ins;
			checkArgNums();
		}

		public override string getInsName() {
			return "rt"+ ins;
		}

		public override char getInsSize() {
			return '\0';
		}

		public override uint getArgNum() {
			return 0;
		}

		public override uint getBytesCount() {
			return 2;
		}

		public override bool overridesCycles() {
			return false;
		}

		public override uint getCycleCount() {
			return ins == 's' ? 16 : (uint) 20;
		}

		public override uint getReadCycleCount() {
			return ins == 's' ? 4 : (uint) 5;
		}

		public override uint getWriteCycleCount() {
			return 0;
		}

		public override string getNote() {
			return "";
		}
	}
}