namespace asmCycleCount {
	internal interface Instruction {
		// gets the instruction line number
		int getLineNum();

		// gets the instruction argument number
		uint getArgNum();

		// returns if the instruction can have cycle counts. False usually means lable or comment only line
		bool hasCycles();
		
		// return all cycles of the instruction
		uint getCycles();

		// return read cycles of the instruction
		uint getReadCycles();

		// return write cycles of the instruction
		uint getWriteCycles();

		// returns if the instruction can have byte counts. False usually means lable or comment only line
		bool hasBytes();

		// return amount of bytes this instruction needs
		uint getByteCount();

		// gets the instruction line
		string getLine();

		// gets any particular note attached to the instruction
		string getNote();
	}
}