using ToyRobot.Domain;

namespace ToyRobot.UnitTests.Application.TestData
{
	public static class RobotServiceTestData
	{
		public static IEnumerable<object[]> ValidPlacePositions =>
			new List<object[]>
			{
				new object[] { 1, 1, Facing.North },
				new object[] { 2, 3, Facing.South },
				new object[] { 5, 5, Facing.East },
				new object[] { 4, 2, Facing.West }
			};

		public static IEnumerable<object[]> TurnLeftExpectations =>
			new List<object[]>
			{
				new object[] { Facing.North, Facing.West },
				new object[] { Facing.West, Facing.South },
				new object[] { Facing.South, Facing.East },
				new object[] { Facing.East, Facing.North }
			};

		public static IEnumerable<object[]> TurnRightExpectations =>
			new List<object[]>
			{
				new object[] { Facing.North, Facing.East },
				new object[] { Facing.East, Facing.South },
				new object[] { Facing.South, Facing.West },
				new object[] { Facing.West, Facing.North }
			};

		public static IEnumerable<object[]> InvalidPositions =>
			new List<object[]>
			{
				new object[] { 0, 1 },
				new object[] { 6, 2 },
				new object[] { 3, 0 },
				new object[] { 2, 6 }
			};
	}
}
