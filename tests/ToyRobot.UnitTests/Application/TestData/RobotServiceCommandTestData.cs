using ToyRobot.Domain;

namespace ToyRobot.UnitTests.Application.TestData
{
	public static class RobotServiceCommandTestData
	{
		public static IEnumerable<object[]> CommandSequences =>
			new List<object[]>
			{
				new object[]
				{
					new string[] { "PLACE_ROBOT 1,1,NORTH", "MOVE" },
					new Position(1, 5),
					Facing.North
				},
				new object[]
				{
					new string[] { "PLACE_ROBOT 2,2,EAST", "LEFT", "MOVE" },
					new Position(2, 1),
					Facing.North
				},
				new object[]
				{
					new string[] { "PLACE_ROBOT 3,3,SOUTH", "RIGHT", "MOVE" },
					new Position(2, 3),
					Facing.West
				},
				new object[]
				{
					new string[] { "PLACE_ROBOT 2,2,NORTH", "MOVE" },
					new Position(2, 2),
					Facing.North,
					new List<Position> { new Position(2, 1) }
				},
				new object[]
				{
					new string[] { "PLACE_ROBOT 1,1,NORTH", "MOVE", "RIGHT", "MOVE", "LEFT", "MOVE" },
					new Position(2, 4),
					Facing.North
				}
			};

		public static IEnumerable<object[]> CommandSequencesFromPDF =>
			new List<object[]>
			{
				new object[]
				{
					new string[]
					{
						"PLACE_ROBOT 3,3,NORTH",
						"PLACE_WALL 3,5",
						"MOVE",
						"MOVE",
						"RIGHT",
						"MOVE",
						"MOVE",
						"MOVE",
						"REPORT"
					},
					new Position(1, 1),
					Facing.East
				},
				new object[]
				{
					new string[]
					{
						"PLACE_ROBOT 2,2,WEST",
						"PLACE_WALL 1,1",
						"PLACE_WALL 2,2",
						"PLACE_WALL 1,3",
						"LEFT",
						"LEFT",
						"MOVE",
						"REPORT"
					},
					new Position(3, 2),
					Facing.East
				}
			};
	}
}
