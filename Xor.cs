namespace asmCycleCount {
	internal class Xor : GenericInstruction {
		private string ins;
		private char size;
		uint cycles, write, read;
		bool overwrite = false;

		public Xor(int lnum, uint tabs, char size, string ins, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.ins = ins;
			this.size = size;
			if (!checkArgNums()) {
				return;
			}

			if (ins.EndsWith("i") && ArgEnum.get(parts[0]).Type != typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument 1 must be a immediate value, but was '" + parts[0] + "' instead!\nParsing will now terminate.", "Error!");

			} else if(ArgEnum.get(parts[0]).Type != typeof(ArgImmediateValue) && ArgEnum.get(parts[0]).Type != typeof(ArgDataDirect)){
				Window.ShowParseError(lnum, "Argument 1 must be a data register or immediate value, but was '" + parts[0] + "' instead!\nParsing will now terminate.", "Error!");
			}

			if (ArgEnum.get(parts[1]).Type == typeof(ArgImmediateValue) || ArgEnum.get(parts[1]).Type == typeof(ArgAddressDirect)) {
				Window.ShowParseError(lnum, "Argument 2 can not be immediate value or address direct!\nParsing will now terminate.", "Error!");
			}

			if (ArgEnum.get(parts[0]).Type == typeof(ArgImmediateValue)) {
				((ArgImmediateValue)parts[0]).block(true);

				if (ArgEnum.get(parts[1]).Type == typeof(ArgDataDirect)) {
					cycles = 4;
					read = 1;
					write = 0;
					overwrite = true;

				} else {
					cycles = 8;
					read = 1;
					write = 1;
				}
			} else {
				if (ArgEnum.get(parts[1]).Type == typeof(ArgDataDirect)) {
					cycles = 8;
					read = 2;
					write = 0;
					overwrite = true;

				} else {
					cycles = 12;
					read = 2;
					write = 1;
				}
			}
		}

		public override string getInsName() {
			return ins;
		}

		public override char getInsSize() {
			return size;
		}

		public override uint getArgNum() {
			return 2;
		}

		public override uint getBytesCount() {
			return 2;
		}

		public override bool overridesCycles() {
			return overwrite;
		}

		public override uint getCycleCount() {
			return cycles;
		}

		public override uint getReadCycleCount() {
			return read;
		}

		public override uint getWriteCycleCount() {
			return write;
		}

		public override string getNote() {
			return "";
		}
	}
}