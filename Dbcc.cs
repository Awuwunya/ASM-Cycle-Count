namespace asmCycleCount {
	internal class Dbcc : GenericInstruction {
		private string ins;

		public Dbcc(int lnum, uint tabs, string ins, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.ins = ins;
			if (!checkArgNums()) {
				return;
			}

			if (ArgEnum.get(parts[0]).Type != typeof(ArgDataDirect)) {
				Window.ShowParseError(lnum, "Argument 1 must be a data register, but was '" + parts[0] + "' instead!\nParsing will now terminate.", "Error!");
			}

			if (ArgEnum.get(parts[1]).Type != typeof(ArgAbsoluteLong) && ArgEnum.get(parts[1]).Type != typeof(ArgLable)) {
				Window.ShowParseError(lnum, "Argument 2 must be a lable, but was '" + parts[1] + "' instead!\nParsing will now terminate.", "Error!");
			}
		}

		public override string getInsName() {
			return ins;
		}

		public override char getInsSize() {
			return '\0';
		}

		public override uint getArgNum() {
			return 2;
		}

		public override uint getByteCount() {
			return 4;
		}

		public override uint getBytesCount() {
			return 4;
		}

		public override bool overridesCycles() {
			return true;
		}

		public override uint getCycleCount() {
			return 12;
		}

		public override uint getReadCycleCount() {
			return 2;
		}

		public override uint getWriteCycleCount() {
			return 0;
		}

		public override string getNote() {
			return "CC true, CC false { 10(2/0) branch taken, 14(3/0) branch not taken }";
		}
	}
}