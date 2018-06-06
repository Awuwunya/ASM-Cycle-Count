namespace asmCycleCount {
	internal class Reset : GenericInstruction {
		public Reset(int lnum, uint tabs, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			checkArgNums();
		}

		public override string getInsName() {
			return "reset";
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
			return 132;
		}

		public override uint getReadCycleCount() {
			return 1;
		}

		public override uint getWriteCycleCount() {
			return 3;
		}

		public override string getNote() {
			return "base, 40(6/0) Exception processing";
		}
	}
}