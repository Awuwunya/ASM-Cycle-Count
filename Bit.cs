namespace asmCycleCount {
	internal class Bit : GenericInstruction {
		private string ins;
		int cycles, read, write;

		public Bit(int lnum, uint tabs, string ins, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.ins = ins;
			if (!checkArgNums()) {
				return;
			}

			if (ArgEnum.get(parts[1]).Type == typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument 2 can not be immediate value!", "Error!");
			}

			if (ArgEnum.get(parts[0]).Type != typeof(ArgImmediateValue) && ArgEnum.get(parts[0]).Type != typeof(ArgDataDirect)) {
				Window.ShowParseError(lnum, "Argument 2 must be a data register or immediate value, but is '" + parts[0] + "' instead!\nParsing will now terminate.", "Error!");
			}

			bool isdn = ArgEnum.get(parts[0]).Type == typeof(ArgDataDirect);
			if (ins.Equals("bchg") || ins.Equals("bset")) {
				cycles = isdn ? 8 : 12;
				read = isdn ? 1 : 2;
				write = ArgEnum.get(parts[1]).Type == typeof(ArgDataDirect) ? 0 : 1;

			} else if (ins.Equals("bclr")) {
				read = isdn ? 1 : 2;
				if (ArgEnum.get(parts[1]).Type == typeof(ArgDataDirect)) {
					write = 0;
					cycles = isdn ? 8 : 12;

				} else {
					cycles = isdn ? 10 : 14;
					write = 1;
				}

			} else if (ins.Equals("btst")) {
				read = isdn ? 1 : 2;
				write = 0;
				if (ArgEnum.get(parts[1]).Type == typeof(ArgDataDirect)) {
					cycles = isdn ? 4 : 8;

				} else {
					cycles = isdn ? 6 : 10;
				}
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
			return (uint) cycles;
		}

		public override uint getReadCycleCount() {
			return (uint) read;
		}

		public override uint getWriteCycleCount() {
			return (uint) write;
		}

		public override string getNote() {
			return "";
		}
	}
}