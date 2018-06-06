namespace asmCycleCount {
	internal class Lea : GenericInstruction {
		int cycles, read, write;

		public Lea(int lnum, uint tabs, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			if (!checkArgNums()) {
				return;
			}

			if (ArgEnum.get(parts[1]).Type != typeof(ArgAddressDirect)) {
				Window.ShowParseError(lnum, "Argument 2 must be a address register, but is '" + parts[1] + "' instead!\nParsing will now terminate.", "Error!");
			}

			switch (ArgEnum.get(parts[0]).Index) {
				case 2:
					cycles = 4;
					read = 1;
					write = 0;
					break;

				case 5:case 7:case 10:
					cycles = 8;
					read = 2;
					write = 0;
					break;

				case 9:
					cycles = 12;
					read = 3;
					write = 0;
					break;

				case 6:case 8:
					cycles = 12;
					read = 2;
					write = 0;
					break;

				default:
					Window.ShowParseError(lnum, "Illegal argument '" + parts[0] + "'!\nParsing will now terminate.", "Error!");
					return;
			}
		}

		public override string getInsName() {
			return "lea";
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