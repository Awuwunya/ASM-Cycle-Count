namespace asmCycleCount {
	internal class Illegal : GenericInstruction {
		public Illegal(int lnum, uint tabs, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			checkArgNums();
		}

		public override string getInsName() {
			return "illegal";
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
			return 34;
		}

		public override uint getReadCycleCount() {
			return 4;
		}

		public override uint getWriteCycleCount() {
			return 3;
		}

		public override string getNote() {
			return "";
		}
	}
}