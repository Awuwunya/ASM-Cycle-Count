namespace asmCycleCount {
	internal class Moveq : GenericInstruction {
		public Moveq(int lnum, uint tabs, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			if (!checkArgNums()) {
				return;
			}

			if (ArgEnum.get(parts[0]).Type != typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument 2 must be an immediate value, but was '" + parts[0] + "' instead!\nParsing will now terminate.", "Error!");
			}

			if (ArgEnum.get(parts[1]).Type != typeof(ArgDataDirect)) {
				Window.ShowParseError(lnum, "Argument 2 must be an data register, but was '" + parts[1] + "' instead!\nParsing will now terminate.", "Error!");
			}
		}

		public override string getInsName() {
			return "moveq";
		}

		public override char getInsSize() {
			return '\0';
		}

		public override uint getArgNum() {
			return 2;
		}

		public override uint getByteCount() {
			return 2;
		}

		public override uint getBytesCount() {
			return 2;
		}

		public override bool overridesCycles() {
			return true;
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