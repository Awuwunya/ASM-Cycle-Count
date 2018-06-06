using System;

namespace asmCycleCount {
	internal class ArgMovemReg : ArgumentPart {
		private string data;
		private int lnum, cycles, read, write;
		private char size;

		// NOTE: the MOVEM base cycles are also calculated here. Therefore, MOVEM should have base clock of 0 cycles
		public ArgMovemReg(int lnum, string data, char size, int index) {
			this.lnum = lnum;
			this.data = data;
			this.size = size;

			if (size != 'b' && size != 'w' && size != 'l' && size != '1' && size != '2' && size != '4' && size != '\0') {
				Window.ShowParseError(lnum, "Could not recognize instruction size '" + size + "'!\nParsing will now terminate.", "Error!");
				return;
			}

			int regs = 0;
			foreach (string s in data.Split('/')) {
				if (s.Contains("-")) {
					string[] p = s.Split('-');
					if (p[0].Length != 2 && p[1].Length != 2) {
						Window.ShowParseError(lnum, "Could not recognize MOVEM argument '" + s + "'!\nParsing will now terminate.", "Error!");
						return;
					}

					int[] r = new int[2];
					for (int i = 0;i < 2;i++) {
						if (p[i].StartsWith("a")) {
							r[i] = Convert.ToInt32(p[i].ToCharArray()[1].ToString()) + 8;

						} else if (p[0].StartsWith("d")) {
							r[i] = Convert.ToInt32(p[i].ToCharArray()[1].ToString());

						} else {
							Window.ShowParseError(lnum, "Could not recognize register '" + p[i] + "'!\nParsing will now terminate.", "Error!");
							return;
						}
					}

					if(r[0] >= r[1]) {
						Window.ShowParseError(lnum, "Illegal statement '" + s + "' (First register larger or same as second register)!\n"+
							"Parsing will now terminate.", "Error!");
						return;
					}

					regs += r[1] - r[0] + 1;

				} else {
					regs++;
				}
			}

			if (index == 0) {
				// R -> M
				if(size == 'b' || size == 'w' || size == '1' || size == '2' || size == '\0') {
					cycles = 4 * regs;
					read = 0;
					write = regs;

				} else {
					cycles = 8 * regs;
					read = 0;
					write = 2 * regs;
				}

			} else {
				// M -> R
				if (size == 'b' || size == 'w' || size == '1' || size == '2' || size == '\0') {
					cycles = 4 * regs;
					read = regs;
					write = 0;

				} else {
					cycles = 8 * regs;
					read = regs * 2;
					write = 0;
				}
			}
		}

		public override uint getByteCount() {
			return 0;
		}

		public override bool hasCycles() {
			return true;
		}

		public override uint getCycles() {
			return (uint)cycles;
		}

		public override uint getReadCycles() {
			return (uint) read;
		}

		public override uint getWriteCycles() {
			return (uint) write;
		}

		public override string getLine() {
			return data;
		}
	}
}