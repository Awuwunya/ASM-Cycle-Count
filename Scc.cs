namespace asmCycleCount {
	internal class Scc : GenericInstruction {
		private bool isdn;
		private string ins;
		long cycles, write, read;

		public Scc(int lnum, uint tabs, string ins, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.ins = ins;
			if (!checkArgNums()) {
				return;
			}

			isdn = ArgEnum.get(parts[0]).Type == typeof(ArgDataDirect);
			if (!isdn && ArgEnum.get(parts[0]).Type == typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument can not be immediate value!", "Error!");
			}

			cycles = (isdn ? 6 : 8 + parts[0].getCycles());
			read = 1 + parts[0].getReadCycles();
			write = (isdn ? 0 : 1 + parts[0].getWriteCycles());
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
			return false;
		}

		public override uint getCycleCount() {
			return (uint) (isdn ? 4 : 8);
		}

		public override uint getReadCycleCount() {
			return 1;
		}

		public override uint getWriteCycleCount() {
			return (uint) (isdn ? 0 : 1);
		}

		public override string getNote() {
			return " false, "+ cycles +'('+ read +'/'+ write +") true";
		}
	}
}