namespace asmCycleCount {
	internal class Trap : GenericInstruction {
		public Trap(int lnum, uint tabs, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			if (!checkArgNums()) {
				return;
			}

			if (ArgEnum.get(parts[0]).Type != typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument must be a immediate value, but is '" + parts[0] + "' instead!\nParsing will now terminate.", "Error!");
			}
		}

		public override string getInsName() {
			return "trap";
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
			return 38;
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