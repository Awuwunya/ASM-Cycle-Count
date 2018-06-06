namespace asmCycleCount {
	internal class ASddx : GenericInstruction {
		private string ins;
		bool isdn = false;
		private char size;

		public ASddx(int lnum, uint tabs, string ins, char size, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.ins = ins;
			this.size = size;
			if (!checkArgNums()) {
				return;
			}

			if (ArgEnum.get(parts[0]).Index != ArgEnum.get(parts[1]).Index) {
				Window.ShowParseError(lnum, "Both arguments need to be of same type!\nParsing will now terminate.", "Error!");
			}

			if (ArgEnum.get(parts[0]).Type != typeof(ArgDataDirect) && ArgEnum.get(parts[1]).Type != typeof(ArgAddressIndirectWithPreDecrement)) {
				Window.ShowParseError(lnum, "Excpected arguments to be either data registers or address registers with pre-decrement!", "Error!");
			}

			if (ArgEnum.get(parts[0]).Type == typeof(ArgDataDirect)) {
				isdn = true;
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

		public override uint getBytesCount() {
			return 2;
		}

		public override bool overridesCycles() {
			return true;
		}

		public override uint getCycleCount() {
			if (size == 'l' || size == '4') {
				return (uint) (isdn ? 8 : 30);

			} else {
				return (uint) (isdn ? 4 : 18);
			}
		}

		public override uint getReadCycleCount() {
			if (size == 'l' || size == '4') {
				return (uint) (isdn ? 1 : 5);

			} else {
				return (uint) (isdn ? 1 : 3);
			}
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