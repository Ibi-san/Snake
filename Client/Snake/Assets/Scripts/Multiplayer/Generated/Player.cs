// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.46
// 

using Colyseus.Schema;

public partial class Player : Schema {
	[Type(0, "string")]
	public string login = default(string);

	[Type(1, "number")]
	public float x = default(float);

	[Type(2, "number")]
	public float z = default(float);

	[Type(3, "uint8")]
	public byte d = default(byte);

	[Type(4, "number")]
	public float h = default(float);

	[Type(5, "uint16")]
	public ushort score = default(ushort);
}

