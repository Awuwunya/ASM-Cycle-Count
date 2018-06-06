namespace asmCycleCount {
	internal class SAddq : GenericInstruction {
		private char size;
		private string ins;
		int cycles, read, write;
		uint bytes;

		public SAddq(int lnum, uint tabs, char size, string ins, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.size = size;
			this.ins = ins;
			if (!checkArgNums()) {
				return;
			}

			if (ArgEnum.get(parts[1]).Type == typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument 2 can not be immediate value!", "Error!");
			}

			if (ArgEnum.get(parts[0]).Type != typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument 1 must be an immediate value, but was '" + parts[0] + "' instead!\nParsing will now terminate.", "Error!");
			}

			bytes = 2 + parts[1].getByteCount();
			if (ArgEnum.get(parts[1]).Type == typeof(ArgDataDirect)) {
				cycles = (size == 'l' || size == '4') ? 8 : 4;
				read = 1;
				write = 0;

			} else if(ArgEnum.get(parts[1]).Type == typeof(ArgAddressDirect)){
				if(size != 'w' && size != '2') {
					Window.ShowParseError(lnum, "Instructions size must be word when in address direct mode!\nParsing will now terminate.", "Error!");

				} else {
					cycles = 8;
					read = 1;
					write = 0;
				}

			} else {
				cycles = ((size == 'l' || size == '4') ? 12 : 8) + (int) parts[1].getCycles();
				read = 1 + (int) parts[1].getReadCycles();
				write = ((size == 'l' || size == '4') ? 2 : 1) + (int) parts[1].getWriteCycles();
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

		public override uint getByteCount() {
			return bytes;
		}

		public override uint getBytesCount() {
			return bytes;
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