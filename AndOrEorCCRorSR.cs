namespace asmCycleCount {
	internal class AndOrEorCCRorSR : GenericInstruction {
		private string ins;
		private char size;
		int cycles, write, read;

		public AndOrEorCCRorSR(int lnum, uint tabs, char size, string ins, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.ins = ins;
			this.size = size;
			if (!checkArgNums()) {
				return;
			}

			if (ArgEnum.get(parts[0]).Type != typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument 1 must be immediate value!\nParsing will now terminate.", "Error!");
				return;
			}

			if (ArgEnum.get(parts[1]).Type == typeof(ArgCCR)) {
				if (size != 'b' && size != '1' && size != '\0') {
					Window.ShowParseError(lnum, "Illegal instruction size! " + ins.ToUpper() + " to CCR expects byte operation!\nParsing will now terminate.", "Error!");
					return;
				}

				cycles = 20;
				read = 3;
				write = 0;

			} else if (ArgEnum.get(parts[1]).Type == typeof(ArgSR)) {
				if (size != 'w' && size != '2' && size != '\0') {
					Window.ShowParseError(lnum, "Illegal instruction size! " + ins.ToUpper() + " to SR expects word operation!\nParsing will now terminate.", "Error!");
					return;
				}

				cycles = 20;
				read = 3;
				write = 0;

			} else {
				Window.ShowParseError(lnum, "Unknown error with AndOrEorCCRorSR in line "+ lnum +"\nParsing will now terminate.", "Error!");
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
			return 4;
		}

		public override uint getBytesCount() {
			return 4;
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