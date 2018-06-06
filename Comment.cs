namespace asmCycleCount {
	internal class Comment : ArgumentPart {
		private string comment;
		private int line;

		public Comment(int linenum, string comment) {
			this.comment = comment;
			line = linenum;
		}

		public override uint getByteCount() {
			return 0;
		}

		public override int getLineNum() {
			return line;
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

		public override string getLine() {
			return "";
		}
	}
}