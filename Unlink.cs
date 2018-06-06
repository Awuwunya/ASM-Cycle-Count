namespace asmCycleCount {
	internal class Unlink : GenericInstruction {
		public Unlink(int lnum, uint tabs, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			if (!checkArgNums()) {
				return;
			}

			if (ArgEnum.get(parts[0]).Type != typeof(ArgAddressDirect)) {
				Window.ShowParseError(lnum, "Argument must be a address register, but is '" + parts[0] + "' instead!\nParsing will now terminate.", "Error!");
			}
		}

		public override string getInsName() {
			return "unlk";
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
			return 12;
		}

		public override uint getReadCycleCount() {
			return 3;
		}

		public override uint getWriteCycleCount() {
			return 0;
		}

		public override string getNote() {
			return "";
		}
	}
}