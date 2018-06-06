namespace asmCycleCount {
	internal class Stop : GenericInstruction {
		public Stop(int lnum, uint tabs, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			checkArgNums();

			if (ArgEnum.get(parts[0]).Type != typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument must be immediate value, but is '" + parts[0] + "' instead!\nParsing will now terminate.", "Error!");
			}
		}

		public override string getInsName() {
			return "stop";
		}

		public override char getInsSize() {
			return '\0';
		}

		public override uint getArgNum() {
			return 1;
		}

		public override uint getBytesCount() {
			return 4;
		}

		public override bool overridesCycles() {
			return false;
		}

		public override uint getCycleCount() {
			return 4;
		}

		public override uint getReadCycleCount() {
			return 0;
		}

		public override uint getWriteCycleCount() {
			return 0;
		}

		public override string getNote() {
			return "";
		}
	}
}