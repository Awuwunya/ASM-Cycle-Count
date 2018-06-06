using System;

namespace asmCycleCount {
	internal class Movem : GenericInstruction {
		int lnum;
		uint tabs, cycles, write, read;
		char size;

		public Movem(int lnum, uint tabs, char size, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.lnum = lnum;
			this.tabs = tabs;
			this.size = size;
			if (!checkArgNums()) {
				return;
			}

			if (size != 'w' && size != 'l' && size != '2' && size != '4') {
				Window.ShowParseError(lnum, "Intruction size not supported '" + size + "'!\nParsing will now terminate.", "Error!");
				return;
			}

			int[] a = { ArgEnum.get(parts[0]).Index, (int) ArgEnum.get(parts[1]).Index };
			if (a[0] == ArgEnum.MOVEM.Index && a[1] != ArgEnum.MOVEM.Index) {
				// R -> M
				switch (a[1]) {
					case 2:case 4:
						cycles = 8 + parts[0].getCycles();
						read = 2 + parts[0].getReadCycles();
						write = parts[0].getWriteCycles();
						break;

					case 5:case 10:
						cycles = 12 + parts[0].getCycles();
						read = 3 + parts[0].getReadCycles();
						write = parts[0].getWriteCycles();
						break;

					case 6:
						cycles = 14 + parts[0].getCycles();
						read = 3 + parts[0].getReadCycles();
						write = parts[0].getWriteCycles();
						break;

					case 9:
						cycles = 16 + parts[0].getCycles();
						read = 4 + parts[0].getReadCycles();
						write = parts[0].getWriteCycles();
						break;

					default:
						Window.ShowParseError(lnum, "Illegal argument '" + parts[0] + "'!\nParsing will now terminate.", "Error!");
						return;
				}

			} else if (a[1] == ArgEnum.MOVEM.Index) {
				// M -> R
				switch (a[0]) {
					case 2:case 3:
						cycles = 12 + parts[1].getCycles();
						read = 3 + parts[1].getReadCycles();
						write = parts[1].getWriteCycles();
						break;

					case 5:case 7:case 10:
						cycles = 16 + parts[1].getCycles();
						read = 4 + parts[1].getReadCycles();
						write = parts[1].getWriteCycles();
						break;

					case 6:case 8:
						cycles = 18 + parts[1].getCycles();
						read = 3 + parts[1].getReadCycles();
						write = parts[1].getWriteCycles();
						break;

					case 9:
						cycles = 20 + parts[1].getCycles();
						read = 5 + parts[1].getReadCycles();
						write = parts[1].getWriteCycles();
						break;

					default:
						Window.ShowParseError(lnum, "Illegal argument '" + parts[1] + "'!\nParsing will now terminate.", "Error!");
						return;
				}

			} else {
				Window.ShowParseError(lnum, "MOVEM should have exactly one of its arguments be register collection!\nParsing will now terminate.", "Error!");
				return;
			}
		}

		public override string getInsName() {
			return "movem";
		}

		public override char getInsSize() {
			return size;
		}

		public override uint getArgNum() {
			return 2;
		}

		public override uint getBytesCount() {
			return 4;
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

		public override uint getCycles() {
			return cycles;
		}

		public override uint getReadCycles() {
			return read;
		}

		public override uint getWriteCycles() {
			return write;
		}

		public override string getNote() {
			return "";
		}
	}
}