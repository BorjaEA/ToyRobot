using ToyRobot.Domain;

namespace ToyRobot.UnitTests.Domain.TestData
{
	public static class BoardTestData
	{
		public static IEnumerable<object[]> InvalidDimensions =>
			new List<object[]>
			{
				new object[] { 0, 5 },
				new object[] { 5, 0 },
				new object[] { -1, 5 },
				new object[] { 5, -1 }
			};

		public static IEnumerable<object[]> TurnLeftData =>
			new List<object[]>
			{
				new object[] { Facing.North, Facing.West },
				new object[] { Facing.West, Facing.South },
				new object[] { Facing.South, Facing.East },
				new object[] { Facing.East, Facing.North }
			};

		public static IEnumerable<object[]> TurnRightData =>
			new List<object[]>
			{
				new object[] { Facing.North, Facing.East },
				new object[] { Facing.East, Facing.South },
				new object[] { Facing.South, Facing.West },
				new object[] { Facing.West, Facing.North }
			};
		public static IEnumerable<object[]> OutOfBoundsPositions =>
			new List<object[]>
			{
				new object[] { 0, 3 },
				new object[] { 6, 3 },
				new object[] { 3, 0 },
				new object[] { 3, 6 },
				new object[] { 0, 0 },
				new object[] { 6, 6 }
			};

	}
}
