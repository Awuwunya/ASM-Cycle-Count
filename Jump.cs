using System;

namespace asmCycleCount {
	internal class Jump : GenericInstruction {
		int cycles, read, write;
		private string ins;

		public Jump(int lnum, uint tabs, string ins, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.ins = ins;
			if (!checkArgNums()) {
				return;
			}

			int add = ins.Equals("jmp") ? 0 : 8, wa = ins.Equals("jmp") ? 0 : 2;
			switch (ArgEnum.get(parts[0]).Index) {
				case 2:
					cycles = 8 + add;
					read = 2;
					write = wa;
					break;

				case 5:case 7:case 10:
					cycles = 10 + add;
					read = 2;
					write = wa;
					break;

				case 9:case 16:
					cycles = 12 + add;
					read = 2;
					write = wa;
					break;

				case 6:case 8:
					cycles = 14 + add;
					read = 3;
					write = wa;
					break;

				default:
					Window.ShowParseError(lnum, "Illegal argument '" + parts[0] + "'!\nParsing will now terminate.", "Error!");
					return;
			}
		}

		public override string getInsName() {
			return ins;
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