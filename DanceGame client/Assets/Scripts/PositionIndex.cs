public enum PositionIndex
{
	LeftShoulderBend = 0,
	LeftForearmBend,
	LeftHand,
	LeftThumb2,
	LeftMid1,
	
	RightShoulderBend,
	RightForearmBend,
	RightHand,
	RightThumb2,
	RightMid1,
	
	RightEar,
	RightEye,
	LeftEar,
	LeftEye,
	Nose,

	LeftThighBend,
	LeftShin,
	LeftFoot,
	LeftToe,

	RightThighBend,
	RightShin,
	RightFoot,
	RightToe,

	AbdomenUpper,

	// Calculated coordinates.
	Hip,
	Head,
	Neck,
	Spine,

	Count,
	None,
}

public static class PositionIndexExtension
{
	public static int Int(this PositionIndex i)
	{
		return (int)i;
	}
}