namespace asmCycleCount {
	internal class MoveToCCRSRUSP : GenericInstruction {
		private char size;
		int cycles, write, read;

		public MoveToCCRSRUSP(int lnum, uint tabs, char size, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.size = size;
			if (!checkArgNums()) {
				return;
			}

			if (ArgEnum.get(parts[1]).Type == typeof(ArgCCR)) {
				if (size != 'b' && size != '1' && size != '\0') {
					Window.ShowParseError(lnum, "Illegal instruction size! MOVE to CCR expects byte operation!\nParsing will now terminate.", "Error!");
					return;
				}

				if (ArgEnum.get(parts[0]).Type == typeof(ArgAddressDirect) || ArgEnum.get(parts[1]).Type == typeof(ArgAddressDirect)) {
					Window.ShowParseError(lnum, "Either argument must not be address register direct!\nParsing will now terminate.", "Error!");
					return;
				}

				cycles = 12;
				read = 1;
				write = 0;

			} else if (ArgEnum.get(parts[0]).Type == typeof(ArgSR) || ArgEnum.get(parts[1]).Type == typeof(ArgSR)) {
				if (size != 'w' && size != '2' && size != '\0') {
					Window.ShowParseError(lnum, "Illegal instruction size! MOVE to SR expects word operation!\nParsing will now terminate.", "Error!");
					return;
				}

				if (ArgEnum.get(parts[0]).Type == typeof(ArgAddressDirect) || ArgEnum.get(parts[1]).Type == typeof(ArgAddressDirect)) {
					Window.ShowParseError(lnum, "Either argument must not be address register direct!\nParsing will now terminate.", "Error!");
					return;
				}

				if (ArgEnum.get(parts[0]).Type == typeof(ArgSR)) {
					if(ArgEnum.get(parts[1]).Type == typeof(ArgDataDirect)) {
						// Dn,SR
						cycles = 6;
						read = 1;
						write = 0;

					} else {
						// <ae>,SR
						cycles = 8;
						read = 1;
						write = 1;
					}

				} else {
					// to SR
					cycles = 12;
					read = 1;
					write = 0;
				}

			} else if (ArgEnum.get(parts[0]).Type == typeof(ArgUSP) || ArgEnum.get(parts[1]).Type == typeof(ArgUSP)) {
				if(ArgEnum.get(parts[0]).Type != typeof(ArgAddressDirect) && ArgEnum.get(parts[1]).Type != typeof(ArgAddressDirect)) {
					Window.ShowParseError(lnum, "Either argument must be address register direct!\nParsing will now terminate.", "Error!");
				}

				cycles = 4;
				read = 1;
				write = 0;

			} else {
				Window.ShowParseError(lnum, "Unknown error with MoveToCCRSRUSP in line " + lnum + "\nParsing will now terminate.", "Error!");
			}
		}

		public override string getInsName() {
			return "move";
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