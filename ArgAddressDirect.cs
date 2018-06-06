namespace asmCycleCount {
	internal class ArgAddressDirect : ArgumentPart {
		private string data;
		private int lnum;
		private char size;

		public ArgAddressDirect(int lnum, string data, char size) {
			this.size = size;
			this.lnum = lnum;
			this.data = data;
		}

		public override uint getByteCount() {
			return 0;
		}

		public override bool hasCycles() {
			return true;
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

		public override string getLine() {
			return data;
		}
	}
}