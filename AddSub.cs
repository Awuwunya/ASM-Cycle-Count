namespace asmCycleCount {
	internal class AddSub : GenericInstruction {
		private string ins;
		private char size;
		int cycles, write, read;
		bool overwrite = false;

		public AddSub(int lnum, uint tabs, char size, string ins, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.ins = ins;
			this.size = size;
			if (!checkArgNums()) {
				return;
			}

			if (ins.EndsWith("i") && ArgEnum.get(parts[0]).Type != typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument 1 must be a immediate value, but was '" + parts[0] + "' instead!\nParsing will now terminate.", "Error!");

			} else if (ins.EndsWith("a") && ArgEnum.get(parts[1]).Type != typeof(ArgAddressDirect)) {
				Window.ShowParseError(lnum, "Argument 2 must be a address register direct, but was '" + parts[1] + "' instead!\nParsing will now terminate.", "Error!");
			}

			if (ArgEnum.get(parts[1]).Type == typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument 2 can not be immediate value!\nParsing will now terminate.", "Error!");
			}

			if ((ArgEnum.get(parts[1]).Type == typeof(ArgAddressDirect) || ArgEnum.get(parts[0]).Type == typeof(ArgAddressDirect)) && (size == 'b' || size == '1')) {
				Window.ShowParseError(lnum, "Instruction size needs to be word or long when either argument is address register direct!\n"+
					"Parsing will now terminate.", "Error!");
			}

			if (ArgEnum.get(parts[0]).Type == typeof(ArgImmediateValue)) {
				if (ArgEnum.get(parts[1]).Type == typeof(ArgDataDirect)) {
					// #,Dn
					cycles = (size == 'l' || size == '4') ? 16 : 8;
					read = (size == 'l' || size == '4') ? 3 : 2;
					write = 0;
					overwrite = true;

				} else if (ArgEnum.get(parts[1]).Type == typeof(ArgAddressDirect)) {
					// #,An
					cycles = 8;
					read = 1;
					write = 0;

				} else {
					cycles = (size == 'l' || size == '4') ? 20 : 12;
					read = (size == 'l' || size == '4') ? 3 : 2;
					write = (size == 'l' || size == '4') ? 2 : 1;
				}
			} else {
				if (ArgEnum.get(parts[1]).Type == typeof(ArgAddressDirect)) {
					// <ea>,An
					cycles = 8;
					read = 1;
					write = 0;

				} else if (ArgEnum.get(parts[1]).Type == typeof(ArgDataDirect)) {
					cycles = (size == 'l' || size == '4') ? 8 : 4;
					read = 1;
					write = 0;

				} else {
					// Dn,<M>
					cycles = (size == 'l' || size == '4') ? 12 : 8;
					read = 1;
					write = (size == 'l' || size == '4') ? 2 : 1;
				}
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

		public override uint getBytesCount() {
			return 2;
		}

		public override bool overridesCycles() {
			return overwrite;
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