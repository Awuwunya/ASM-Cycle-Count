namespace asmCycleCount {
	internal class BitShift : GenericInstruction {
		private string ins;
		private char size;
		private bool isdn;
		private uint anum;
		private ArgumentPart[] parts;

		public BitShift(int lnum, uint tabs, string ins, char size, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.ins = ins;
			this.size = size;
			this.parts = parts;
			anum = (uint)Window.getArgNum(parts);

			if (anum != 1 && anum != 2) {
				Window.ShowParseError(lnum, ins.ToUpper() +" instruction requires 1 or 2 arguments, but "+ anum +" were found!\nParsing will now terminate.", "Error!");
				return;
			}

			if (anum == 1 && (size != 'w' && size != '2' && size != '\0')) {
				Window.ShowParseError(lnum, "Instruction size can only be word when bit count is not specified!", "Error!");
			}

			if (anum == 2 && ArgEnum.get(parts[1]).Type != typeof(ArgDataDirect)) {
				Window.ShowParseError(lnum, "Argument 2 can only be data register when bitcount is specified!", "Error!");
			}

			if (anum == 2 && ArgEnum.get(parts[1]).Type == typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument 2 can not be immediate value!", "Error!");
			}

			isdn = ArgEnum.get(parts[anum - 1]).Type == typeof(ArgDataDirect);
		}

		public override string getInsName() {
			return ins;
		}

		public override char getInsSize() {
			return size;
		}

		public override uint getArgNum() {
			return anum;
		}

		public override uint getBytesCount() {
			return ArgEnum.get(parts[0]).Type == typeof(ArgImmediateValue) ? (uint) 0 : 2;
		}

		public override bool overridesCycles() {
			return true;
		}

		public override uint getCycleCount() {
			return (uint) (isdn && (size != 'l' && size != '4') ? 6 : 8) + (anum == 1 ? parts[0].getCycles() : parts[1].getCycles());
		}

		public override uint getReadCycleCount() {
			return 1 + (anum == 1 ? parts[0].getReadCycles() : parts[1].getReadCycles());
		}

		public override uint getWriteCycleCount() {
			return (uint) (isdn ? 0 : 1) + (anum == 1 ? parts[0].getWriteCycles() : parts[1].getWriteCycles());
		}

		public override string getNote() {
			return isdn ? "+ 2n(0/0) where n is shift or rotate count" : "";
		}
	}
}