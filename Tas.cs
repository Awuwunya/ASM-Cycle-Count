namespace asmCycleCount {
	internal class Tas : GenericInstruction {
		private bool isdn;

		public Tas(int lnum, uint tabs, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			if (!checkArgNums()) {
				return;
			}

			isdn = ArgEnum.get(parts[0]).Type == typeof(ArgDataDirect);
			if (!isdn && ArgEnum.get(parts[0]).Type == typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument can not be immediate value!", "Error!");
			}
		}

		public override string getInsName() {
			return "tas";
		}

		public override char getInsSize() {
			return '\0';
		}

		public override uint getArgNum() {
			return 1;
		}

		public override uint getBytesCount() {
			return 2;
		}

		public override bool overridesCycles() {
			return false;
		}

		public override uint getCycleCount() {
			return (uint) (isdn ? 4 : 10);
		}

		public override uint getReadCycleCount() {
			return 1;
		}

		public override uint getWriteCycleCount() {
			return (uint) (isdn ? 0 : 1);
		}

		public override string getNote() {
			return (isdn ? "" : "WARN: only updates destination in MD3!");
		}
	}
}