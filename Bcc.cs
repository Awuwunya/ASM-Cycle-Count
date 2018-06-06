namespace asmCycleCount {
	internal class Bcc : GenericInstruction {
		private char size;
		private string ins;

		public Bcc(int lnum, uint tabs, string ins, char size, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.size = size;
			this.ins = ins;
			if (!checkArgNums()) {
				return;
			}

			if (size != 's' && size != 'b' && size != 'w' && size != '1' && size != '2') {
				Window.ShowParseError(lnum, ins.ToUpper() +" can only be byte or word size in M68000!\nParsing will now terminate.", "Error!");
			}

			if(ArgEnum.get(parts[0]).Type != typeof(ArgAbsoluteLong) && ArgEnum.get(parts[0]).Type != typeof(ArgLable)) {
				Window.ShowParseError(lnum, "Argument must be a lable, but was '" + parts[0] + "' instead!\nParsing will now terminate.", "Error!");
			}
		}

		public override string getInsName() {
			return ins;
		}

		public override char getInsSize() {
			return size;
		}

		public override uint getArgNum() {
			return 1;
		}

		public override uint getByteCount() {
			if (size == 'b' || size == '1' || size == 's') {
				return 2;

			} else {
				return 4;
			}
		}

		public override uint getBytesCount() {
			if (size == 'b' || size == '1' || size == 's') {
				return 2;

			} else {
				return 4;
			}
		}

		public override bool overridesCycles() {
			return true;
		}

		public override uint getCycleCount() {
			return (uint) (ins.Equals("bsr") ? 18 : 10);
		}

		public override uint getReadCycleCount() {
			return 2;
		}

		public override uint getWriteCycleCount() {
			return (uint) (ins.Equals("bsr") ? 2 : 0);
		}

		public override string getNote() {
			if (ins.Equals("bsr") || ins.Equals("bra")) {
				return "";

			} else {
				if(size == 's' || size == 'b' || size == '1') {
					return "taken, 8(1/0) not taken";

				} else {
					return "taken, 12(1/0) not taken";
				}
			}
		}
	}
}