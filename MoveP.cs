namespace asmCycleCount {
	internal class MoveP : GenericInstruction {
		private char size;
		int cycles, read, write;

		public MoveP(int lnum, uint tabs, char size, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.size = size;
			bool datafirst = false;
			if (!checkArgNums()) {
				return;
			}

			for (int i = 0;i < 2;i++) {
				if (ArgEnum.get(parts[i]).Type != typeof(ArgDataDirect) && ArgEnum.get(parts[i]).Type != typeof(ArgAddressIndirectWithIndex) && 
					ArgEnum.get(parts[i]).Type != typeof(ArgAddressIndirect)) {
					Window.ShowParseError(lnum, "Argument "+ i +" must be a data register or address register with offset, but is '" + parts[i] + 
						"' instead!\nParsing will now terminate.", "Error!");
				}

				if(ArgEnum.get(parts[i]).Type == typeof(ArgDataDirect)) {
					datafirst = i == 0;
				}
			}

			if (size == 'w' || size == '\0') {
				cycles = 16;
				read = datafirst ? 2 : 4;
				write = datafirst ? 2 : 0;

			} else if (size == 'l') {
				cycles = 24;
				read = datafirst ? 2 : 6;
				write = datafirst ? 4 : 0;

			} else {
				Window.ShowParseError(lnum, "Instruction size was expected to be word or long, but was '"+ size +"' instead!\nParsing will now terminate.", "Error!");
			}
		}

		public override string getInsName() {
			return "movep";
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