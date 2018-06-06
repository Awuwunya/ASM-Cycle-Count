using System;

namespace asmCycleCount {
	internal class ArgImmediateValue : ArgumentPart {
		private string data;
		private int lnum;
		private char size;
		private bool blocked;

		public ArgImmediateValue(int lnum, string data, char size) {
			this.lnum = lnum;
			this.data = data;
			this.size = size;
			blocked = false;
		}

		public override bool hasCycles() {
			return true;
		}

		public void block(bool b) {
			blocked = b;
		}

		public override uint getByteCount() {
			switch (size) {
				case 'b':case '1':case 'w':case '2':case '\0':
					return 2;

				case 'l':case '4':
					return 4;

				default:
					Window.ShowParseError(lnum, "Could not recognize instrument size '"+ size +"'!\nParsing will now terminate.", "Error!");
					return 0;
			}
		}

		public override uint getCycles() {
			if(blocked) return 0;
			switch (size) {
				case 'b':case '1':case 'w':case '2':
					return 4;

				case 'l':case '4':
					return 8;

				case '\0':
					return 0;

				default:
					Window.ShowParseError(lnum, "Could not recognize instrument size '" + size + "'!\nParsing will now terminate.", "Error!");
					return 0;
			}
		}

		public override uint getReadCycles() {
			if(blocked) return 0;
			switch (size) {
				case 'b':case '1':case 'w':case '2':
					return 1;

				case 'l':case '4':
					return 2;

				case '\0':
					return 0;

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