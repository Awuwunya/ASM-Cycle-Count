namespace asmCycleCount {
	internal class ArgPCRelativeWithIndex : ArgumentPart {
		private string data;
		private int lnum;
		private char size;

		public ArgPCRelativeWithIndex(int lnum, string data, char size) {
			this.size = size;
			this.lnum = lnum;
			this.data = data;
		}

		public override uint getByteCount() {
			return 2;
		}

		public override bool hasCycles() {
			return true;
		}

		public override uint getCycles() {
			switch (size) {
				case 'b':case '1':case 'w':case '2':
					return 10;

				case 'l':case '4':
					return 14;

				default:
					Window.ShowParseError(lnum, "Could not recognize instrument size '" + size + "'!\nParsing will now terminate.", "Error!");
					return 0;
			}
		}

		public override uint getReadCycles() {
			switch (size) {
				case 'b':case '1':case 'w':case '2':
					return 2;

				case 'l':case '4':
					return 3;

				default:
					Window.ShowParseError(lnum, "Could not recognize instrument size '" + size + "'!\nParsing will now terminate.", "Error!");
					return 0;
			}
		}

		public override uint getWriteCycles() {
			return 0;
		}

		public override string getLine() {
			return data;
		}
	}
}