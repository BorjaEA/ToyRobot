using ToyRobot.Domain;

namespace ToyRobot.UnitTests.Application.TestData
{
	public static class RobotServiceCommandTestData
	{
		public static IEnumerable<object[]> CommandSequences =>
			new List<object[]>
			{
                // Simple move north
                new object[]
				{
					new string[] { "PLACE 1,1,NORTH", "MOVE" },
					new Position(2, 1),
					Facing.North
				},
                // Turn left then move
                new object[]
				{
					new string[] { "PLACE 2,2,EAST", "LEFT", "MOVE" },
					new Position(3, 2),
					Facing.North
				},
                // Turn right then move
                new object[]
				{
					new string[] { "PLACE 3,3,SOUTH", "RIGHT", "MOVE" },
					new Position(3, 2),
					Facing.West
				},
                // Ignore MOVE into wall
                new object[]
				{
					new string[] { "PLACE 2,2,NORTH", "MOVE" }, // assume wall at 3,2
                    new Position(2,2),
					Facing.North,
					new List<Position> { new Position(3,2) } // walls
                },
                // Multiple moves and turns
                new object[]
				{
					new string[] { "PLACE 1,1,NORTH", "MOVE", "RIGHT", "MOVE", "LEFT", "MOVE" },
					new Position(3,2),
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
					new Position(1, 4),   // Expected row
                    Facing.East,           // Expected facing
                    new List<Position> { new Position(3,5) } // Walls
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
					new Position(3, 2),   // Expected row
                    Facing.East,           // Expected facing
                    new List<Position>
					{
						new Position(1,1),
						new Position(2,2),
						new Position(1,3)
					} // Walls
                }
		};
	}
}
