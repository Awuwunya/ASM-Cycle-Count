using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace asmCycleCount {
	class ArgEnum {
		private static int i = -1;
		public readonly static ArgEnumValue DATADIRECT = new ArgEnumValue(typeof(ArgDataDirect), ++i);  // 0 Dn
		public readonly static ArgEnumValue ADDRESSDIRECT = new ArgEnumValue(typeof(ArgAddressDirect), ++i);    // 1 An
		public readonly static ArgEnumValue ADDRESSINDIRECT = new ArgEnumValue(typeof(ArgAddressIndirect), ++i);    // 2 (An)
		public readonly static ArgEnumValue ADDRESSINDIRECTWITHPOSTINCREMENT = new ArgEnumValue(typeof(ArgAddressIndirectWithPostIncrement), ++i);  // 3 (An)+
		public readonly static ArgEnumValue ADDRESSINDIRECTWITHPREDECREMENT = new ArgEnumValue(typeof(ArgAddressIndirectWithPreDecrement), ++i);    // 4 -(An)
		public readonly static ArgEnumValue ADDRESSINDIRECTWITHOFFSET = new ArgEnumValue(typeof(ArgAddressIndirectWithOffset), ++i);    // 5 d16(An)
		public readonly static ArgEnumValue ADDRESSINDIRECTWITHINDEX = new ArgEnumValue(typeof(ArgAddressIndirectWithIndex), ++i);  // 6 d8(An,ix)
		public readonly static ArgEnumValue PCRELATIVEWITHOFFSET = new ArgEnumValue(typeof(ArgPCRelativeWithOffset), ++i);  // 7 d16(PC)
		public readonly static ArgEnumValue PCRELATIVEWITHINDEX = new ArgEnumValue(typeof(ArgPCRelativeWithIndex), ++i);    // 8 d8(PC,ix)
		public readonly static ArgEnumValue ABSOLUTELONG = new ArgEnumValue(typeof(ArgAbsoluteLong), ++i);  // 9 xxx.l
		public readonly static ArgEnumValue ABSOLUTESHORT = new ArgEnumValue(typeof(ArgAbsoluteShort), ++i);    // 10 xxx.w
		public readonly static ArgEnumValue IMMEDIATE = new ArgEnumValue(typeof(ArgImmediateValue), ++i); // 11 #xxx
		public readonly static ArgEnumValue MOVEM = new ArgEnumValue(typeof(ArgMovemReg), ++i); // 12 whatever movem uses
		public readonly static ArgEnumValue CCR = new ArgEnumValue(typeof(ArgCCR), ++i); // 13 ccr
		public readonly static ArgEnumValue SR = new ArgEnumValue(typeof(ArgSR), ++i); // 14 sr
		public readonly static ArgEnumValue USP = new ArgEnumValue(typeof(ArgUSP), ++i); // 15 USP
		public readonly static ArgEnumValue Lable = new ArgEnumValue(typeof(ArgLable), ++i); // 16 lable
		public readonly static ArgEnumValue Comment = new ArgEnumValue(typeof(Comment), ++i); // 17 comment

		public static ArgEnumValue get(object o) {
			foreach(FieldInfo fi in typeof(ArgEnum).GetFields()) {
				if (fi != null) {
					FieldInfo f = typeof(ArgEnum).GetField(fi.Name, BindingFlags.Public | BindingFlags.Static);

					if (f != null) {
						ArgEnumValue a = (ArgEnumValue) f.GetValue(null);
						if (a != null && a.Type == o.GetType()) {
							return a;
						}
					}
				}
			}

			Window.ShowParseError(-1, "Could not collect EnumArg with type '"+ o.GetType() +"'!\nParsing will now terminate.", "Error!");
			return null;
		}
	}

	public class ArgEnumValue {
		public Type Type { get; }
		public int Index { get; }

		public ArgEnumValue(Type type, int v) {
			this.Type = type;
			Index = v;
		}
	}
}
