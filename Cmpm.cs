namespace asmCycleCount {
	internal class Cmpm : GenericInstruction {
		private char size;

		public Cmpm(int lnum, uint tabs, char size, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.size = size;
			if (!checkArgNums()) {
				return;
			}

			if (ArgEnum.get(parts[0]).Index != ArgEnum.get(parts[1]).Index) {
				Window.ShowParseError(lnum, "Both arguments need to be of same type!\nParsing will now terminate.", "Error!");
			}

			if (ArgEnum.get(parts[1]).Type != typeof(ArgAddressIndirectWithPostIncrement)) {
				Window.ShowParseError(lnum, "Excpected arguments to be ddress register with post-increment!", "Error!");
			}
		}

		public override string getInsName() {
			return "cmpm";
		}

		public override char getInsSize() {
			return '\0';
		}

		public override uint getArgNum() {
			return 2;
		}

		public override uint getBytesCount() {
			return 2;
		}

		public override bool overridesCycles() {
			return true;
		}

		public override uint getCycleCount() {
			if (size == 'l' || size == '4') {
				return 25;

			} else {
				return 12;
			}
		}

		public override uint getReadCycleCount() {
			if (size == 'l' || size == '4') {
				return 5;

			} else {
				return 3;
			}
		}

		public override uint getWriteCycleCount() {
			if (size == 'l' || size == '4') {
				return 0;

			} else {
				return 0;
			}
		}

		public override string getNote() {
			return "";
		}
	}
}