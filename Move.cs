using System;

namespace asmCycleCount {
	internal class Move : GenericInstruction {
		private char size;
		int cycles, read, write;
		private string ins;

		public Move(int lnum, uint tabs, char size, string ins, ArgumentPart[] parts) : base(lnum, tabs, parts) {
			this.size = size;
			this.ins = ins;
			if (!checkArgNums()) {
				return;
			}

			if (ins.EndsWith("a") && ArgEnum.get(parts[0]).Type != typeof(ArgAddressDirect) && ArgEnum.get(parts[1]).Type != typeof(ArgAddressDirect)) {
				Window.ShowParseError(lnum, "Either argument must be an address register direct!\nParsing will now terminate.", "Error!");
			}

			if ((ArgEnum.get(parts[1]).Type == typeof(ArgAddressDirect) || ArgEnum.get(parts[0]).Type == typeof(ArgAddressDirect)) && (size == 'b' || size == '1')) {
				Window.ShowParseError(lnum, "Instruction size needs to be word or long when either argument is address register direct!\n" +
					"Parsing will now terminate.", "Error!");
			}

			if (ArgEnum.get(parts[1]).Type == typeof(ArgImmediateValue)) {
				Window.ShowParseError(lnum, "Argument 2 can not be immediate value!\nParsing will now terminate.", "Error!");
			}

			if (ArgEnum.get(parts[1]).Type == typeof(ArgPCRelativeWithIndex) || ArgEnum.get(parts[1]).Type == typeof(ArgPCRelativeWithOffset)) {
				Window.ShowParseError(lnum, "Argument 2 can not be PC relative!\nParsing will now terminate.", "Error!");
			}

			try {
				int off = getOff(parts[1]) + (getOff(parts[0]) * 9);
				if (size == 'l' || size == '4') {
					cycles = cycleL[off];
					write = readWriteL[off] & 0xF;
					read = (readWriteL[off] >> 4) & 0xF;

				} else {
					cycles = cycleBW[off];
					write = readWriteBW[off] & 0xF;
					read = (readWriteBW[off] >> 4) & 0xF;
				}
			} catch (Exception) {
				Window.ShowParseError(lnum, "Illegal argument pair '"+ ArgEnum.get(parts[0]).Type + "', '"+ ArgEnum.get(parts[1]).Type + "'!", "Error!");
			}
		}

		private int getOff(ArgumentPart a) {
			switch (ArgEnum.get(a).Index) {
				case 7:case 8:
					return ArgEnum.get(a).Index + 2;

				case 10: // xxx.w
					return 7;

				case 9:	// xxx.l
					return 8;

				default:
					return ArgEnum.get(a).Index;
			}
		}

		public static readonly int[] cycleBW = {
			/*           Dn An (An) (An)+ -(An) d(An) d(An,ix) xxx.W xxx.L*/
			/*   Dn   */ 4, 4,  8,   8,     8,   12,    14,     12,   16,
			/*   An   */ 4, 4,  8,   8,     8,   12,    14,     12,   16,
			/*  (An)  */ 8, 8, 12,  12,    12,   16,    18,     16,   20,
			/* (An)+  */ 8, 8, 12,  12,    12,   16,    18,     16,   20,
			/* -(An)  */10,10, 14,  14,    14,   18,    20,     18,   22,
			/* d(An)  */12,12, 16,  16,    16,   20,    22,     20,   24,
			/*d(An,ix)*/14,14, 18,  18,    18,   22,    24,     22,   26,
			/* xxx.w  */12,12, 16,  16,    16,   20,    22,     20,   24,
			/* xxx.l  */16,16, 20,  20,    20,   24,    26,     24,   28,
			/* d(PC)  */12,12, 16,  16,    16,   20,    22,     20,   24,
			/*d(PC,ix)*/14,14, 18,  18,    18,   22,    24,     22,   26,
			/*  #xxx  */ 8, 8, 12,  12,    12,   16,    18,     16,   20,
		};

		public static readonly int[] readWriteBW = {
			/*           Dn   An  (An) (An)+ -(An) d(An) d(An,ix) xxx.W xxx.L*/
			/*   Dn   */0x10,0x10,0x11,0x11, 0x11, 0x21,   0x21,  0x21, 0x31,
			/*   An   */0x10,0x10,0x11,0x11, 0x11, 0x21,   0x21,  0x21, 0x31,
			/*  (An)  */0x20,0x20,0x21,0x21, 0x21, 0x31,   0x31,  0x31, 0x41,
			/* (An)+  */0x20,0x20,0x21,0x21, 0x21, 0x31,   0x31,  0x31, 0x41,
			/* -(An)  */0x20,0x20,0x21,0x21, 0x21, 0x31,   0x41,  0x31, 0x41,
			/* d(An)  */0x30,0x30,0x31,0x31, 0x31, 0x41,   0x41,  0x41, 0x51,
			/*d(An,ix)*/0x30,0x30,0x31,0x31, 0x31, 0x41,   0x41,  0x41, 0x51,
			/* xxx.w  */0x30,0x30,0x31,0x31, 0x31, 0x41,   0x41,  0x41, 0x51,
			/* xxx.l  */0x40,0x40,0x41,0x41, 0x41, 0x51,   0x51,  0x51, 0x61,
			/* d(PC)  */0x30,0x30,0x31,0x31, 0x31, 0x41,   0x41,  0x41, 0x51,
			/*d(PC,ix)*/0x30,0x30,0x31,0x31, 0x31, 0x41,   0x41,  0x41, 0x51,
			/*  #xxx  */0x20,0x20,0x21,0x21, 0x21, 0x31,   0x41,  0x31, 0x41,
		};

		public static readonly int[] cycleL = {
			/*           Dn An (An) (An)+ -(An) d(An) d(An,ix) xxx.W xxx.L*/
			/*   Dn   */ 4, 4,  12,  12,   12,   16,    18,     16,   20,
			/*   An   */ 4, 4,  12,  12,   12,   16,    18,     16,   20,
			/*  (An)  */12,12,  20,  20,   20,   24,    26,     24,   28,
			/* (An)+  */12,12,  20,  20,   20,   24,    26,     24,   28,
			/* -(An)  */14,14,  22,  22,   22,   26,    28,     26,   30,
			/* d(An)  */16,16,  24,  24,   24,   28,    30,     28,   32,
			/*d(An,ix)*/18,18,  26,  26,   26,   30,    32,     30,   34,
			/* xxx.w  */16,16,  24,  24,   24,   28,    30,     28,   32,
			/* xxx.l  */20,20,  28,  28,   28,   32,    34,     32,   36,
			/* d(PC)  */16,16,  24,  24,   24,   28,    30,     28,   32,
			/*d(PC,ix)*/18,18,  26,  26,   26,   30,    32,     30,   34,
			/*  #xxx  */12,12,  20,  20,   20,   24,    26,     24,   28,
		};

		public static readonly int[] readWriteL = {
			/*           Dn   An  (An) (An)+ -(An) d(An) d(An,ix) xxx.W xxx.L*/
			/*   Dn   */0x10,0x10,0x12,0x12, 0x12, 0x22,   0x22,  0x22, 0x32,
			/*   An   */0x10,0x10,0x12,0x12, 0x12, 0x22,   0x22,  0x22, 0x32,
			/*  (An)  */0x30,0x30,0x32,0x32, 0x32, 0x42,   0x42,  0x42, 0x52,
			/* (An)+  */0x30,0x30,0x32,0x32, 0x32, 0x42,   0x42,  0x42, 0x52,
			/* -(An)  */0x30,0x30,0x32,0x32, 0x32, 0x42,   0x42,  0x42, 0x52,
			/* d(An)  */0x40,0x40,0x42,0x42, 0x42, 0x52,   0x52,  0x52, 0x62,
			/*d(An,ix)*/0x40,0x40,0x42,0x42, 0x42, 0x52,   0x52,  0x52, 0x62,
			/* xxx.w  */0x40,0x40,0x42,0x42, 0x42, 0x52,   0x52,  0x52, 0x62,
			/* xxx.l  */0x50,0x50,0x52,0x52, 0x52, 0x62,   0x62,  0x62, 0x72,
			/* d(PC)  */0x40,0x40,0x42,0x42, 0x42, 0x52,   0x52,  0x52, 0x52,
			/*d(PC,ix)*/0x40,0x40,0x42,0x42, 0x42, 0x52,   0x52,  0x52, 0x62,
			/*  #xxx  */0x30,0x30,0x32,0x32, 0x32, 0x42,   0x42,  0x42, 0x52,
		};

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
			return true;
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