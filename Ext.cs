namespace asmCycleCount {
	internal class Ext : GenericInstruction {
		private char size;

		public Ext(int lnum, uint tabs, char size, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.size = size;
			if (!checkArgNums()) {
				return;
			}

			if (ArgEnum.get(parts[0]).Type != typeof(ArgDataDirect)) {
				Window.ShowParseError(lnum, "Argument must be a data register, but is '" + parts[0] + "' instead!\nParsing will now terminate.", "Error!");
			}
		}

		public override string getInsName() {
			return "ext";
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