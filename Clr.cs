namespace asmCycleCount {
	internal class Clr : GenericInstruction {
		private char size;
		private bool isdn;
		private string ins;

		public Clr(int lnum, uint tabs, char size, string ins, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.size = size;
			this.ins = ins;
			if (!checkArgNums()) {
				return;
			}

			isdn = ArgEnum.get(parts[0]).Type == typeof(ArgDataDirect);

			if (!isdn && ArgEnum.get(parts[0]).Type == typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument can not be immediate value!", "Error!");
			}
		}

		public override string getInsName() {
			return ins;
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
			if (size == 'l' || size == '4') {
				return (uint) (isdn ? 6 : 12);

			} else {
				return (uint) (isdn ? 4 : 8);
			}
		}

		public override uint getReadCycleCount() {
			return 1;
		}

		public override uint getWriteCycleCount() {
			if (size == 'l' || size == '4') {
				return (uint) (isdn ? 0 : 2);

			} else {
				return (uint) (isdn ? 0 : 1);
			}
		}

		public override string getNote() {
			return "";
		}
	}
}