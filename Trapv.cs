namespace asmCycleCount {
	internal class Trapv : GenericInstruction {
		public Trapv(int lnum, uint tabs, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			checkArgNums();
		}

		public override string getInsName() {
			return "trapv";
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
			return 4;
		}

		public override uint getReadCycleCount() {
			return 1;
		}

		public override uint getWriteCycleCount() {
			return 0;
		}

		public override string getNote() {
			return "Trap not taken, 34(4/3) Trap taken";
		}
	}
}