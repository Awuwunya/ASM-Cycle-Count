using System;

namespace asmCycleCount {
	internal class ArgLable : ArgumentPart {
		private string data;
		private char size;

		public ArgLable(int lnum, string data, char size) {
			this.data = data;
			this.size = size;
		}

		public override string getLine() {
			return data;
		}

		public override uint getByteCount() {
			return (uint)((size == 'l' || size == '4') ? 4 : 2);
		}

		public override bool hasCycles() {
			return false;
		}

		public override uint getCycles() {
			return 0;
		}

		public override uint getReadCycles() {
			return 0;
		}

		public override uint getWriteCycles() {
			return 0;
		}
	}
}