namespace asmCycleCount {
	internal class MulDiv : GenericInstruction {
		private string ins;
		uint cycles, read, write;
		uint bytes;
		char size;

		public MulDiv(int lnum, uint tabs, string ins, char size, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.ins = ins;
			this.size = size;
			if (!checkArgNums()) {
				return;
			}

			if(size != '\0' && size != 'w' && size != '2') {
				Window.ShowParseError(lnum, "Intruction size must be word!\nParsing will now terminate.", "Error!");
				return;
			}

			if (ArgEnum.get(parts[1]).Type != typeof(ArgDataDirect)) {
				Window.ShowParseError(lnum, "Argument 2 must be an data register, but was '" + parts[1] + "' instead!\nParsing will now terminate.", "Error!");
			}
			
			if (ArgEnum.get(parts[0]).Type == typeof(ArgImmediateValue)) {
				bytes = 4;

			} else {
				bytes = 2 + parts[0].getByteCount();
			}

			if (ins.Equals("divs")) {
				cycles = 158 + parts[0].getCycles();
				read = 1 + parts[0].getReadCycles();
				write = 0 + parts[0].getWriteCycles();

			} else if (ins.Equals("divu")) {
				cycles = 140 + parts[0].getCycles();
				read = 1 + parts[0].getReadCycles();
				write = 0 + parts[0].getWriteCycles();

			} else {
				cycles = 70 + parts[0].getCycles();
				read = 1 + parts[0].getReadCycles();
				write = 0 + parts[0].getWriteCycles();
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
			return cycles;
		}

		public override uint getReadCycleCount() {
			return read;
		}

		public override uint getWriteCycleCount() {
			return write;
		}

		public override string getNote() {
			return "max";
		}
	}
}