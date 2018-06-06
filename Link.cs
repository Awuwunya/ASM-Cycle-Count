namespace asmCycleCount {
	internal class Link : GenericInstruction {
		public Link(int lnum, uint tabs, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			if (!checkArgNums()) {
				return;
			}

			if (ArgEnum.get(parts[0]).Type != typeof(ArgAddressDirect)) {
				Window.ShowParseError(lnum, "Argument 1 must be a address register, but is '" + parts[0] + "' instead!\nParsing will now terminate.", "Error!");
			}

			if (ArgEnum.get(parts[1]).Type != typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument 2 must be a immediate value, but is '" + parts[1] + "' instead!\nParsing will now terminate.", "Error!");
			}
		}

		public override string getInsName() {
			return "link";
		}

		public override char getInsSize() {
			return '\0';
		}

		public override uint getArgNum() {
			return 2;
		}

		public override uint getBytesCount() {
			return 4;
		}

		public override bool overridesCycles() {
			return false;
		}

		public override uint getCycleCount() {
			return 16;
		}

		public override uint getReadCycleCount() {
			return 2;
		}

		public override uint getWriteCycleCount() {
			return 2;
		}

		public override string getNote() {
			return "";
		}
	}
}