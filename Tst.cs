namespace asmCycleCount {
	internal class Tst : GenericInstruction {
		private char size;

		public Tst(int lnum, uint tabs, char size, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.size = size;
			if (!checkArgNums()) {
				return;
			}

			if (ArgEnum.get(parts[0]).Type == typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument can not be immediate value!", "Error!");
			}
		}

		public override string getInsName() {
			return "tst";
		}

		public override char getInsSize() {
			return size;
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
			return 4;
		}

		public override uint getReadCycleCount() {
			return 1;
		}

		public override uint getWriteCycleCount() {
			return 0;
		}

		public override string getNote() {
			return "";
		}
	}
}