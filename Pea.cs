namespace asmCycleCount {
	internal class Pea : GenericInstruction {
		int cycles, read, write;

		public Pea(int lnum, uint tabs, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			if (!checkArgNums()) {
				return;
			}

			switch (ArgEnum.get(parts[0]).Index) {
				case 2:
					cycles = 12;
					read = 1;
					write = 2;
					break;

				case 5:case 7:case 10:
					cycles = 16;
					read = 2;
					write = 2;
					break;

				case 9:
					cycles = 20;
					read = 3;
					write = 2;
					break;

				case 6:case 8:
					cycles = 20;
					read = 2;
					write = 2;
					break;

				default:
					Window.ShowParseError(lnum, "Illegal argument '" + parts[0] + "'!\nParsing will now terminate.", "Error!");
					return;
			}
		}

		public override string getInsName() {
			return "pea";
		}

		public override char getInsSize() {
			return '\0';
		}

		public override uint getArgNum() {
			return 1;
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