using ToyRobot.Domain;
using ToyRobot.UnitTests.Domain.TestData;
using ToyRobot.Domain.Exceptions;

namespace ToyRobot.UnitTests.Domain
{
	public class BoardTests
	{
		[Fact]
		public void Constructor_DefaultInputs_ShouldCreateBoardCorrectly()
		{
			var defaultHeight = 5;
			var defaultWidth = 5;

			var board = new Board();

			Assert.IsType<Board>(board);
			Assert.Equal(defaultHeight, board.Height);
			Assert.Equal(defaultWidth, board.Width);
			Assert.Empty(board.Walls);
			Assert.Null(board.Robot);
		}

		[Theory]
		[MemberData(nameof(BoardTestData.InvalidDimensions), MemberType = typeof(BoardTestData))]
		public void Constructor_InvalidDimensions_ShouldThrowException(int height, int width)
		{
			Assert.Throws<InvalidPositionException>(() => new Board(height, width));
		}

		[Fact]
		public void Constructor_WithRobot_ShouldPlaceRobotCorrectly()
		{
			var height = 5;
			var width = 5;
			var row = 2;
			var col = 3;
			var position = new Position(row, col);
			var facing = Facing.North;
			var robot = new Robot(position, facing);

			var board = new Board(robot, height, width);

			Assert.NotNull(board.Robot);
			Assert.Equal(robot, board.Robot);
			Assert.Equal(height, board.Height);
			Assert.Equal(width, board.Width);
		}

		[Fact]
		public void Constructor_WithWallsAndRobot_ShouldInitializeCorrectly()
		{
			var height = 5;
			var width = 5;
			var wallRow = 1;
			var wallCol = 1;
			var robotRow = 3;
			var robotCol = 3;
			var wallPosition = new Position(wallRow, wallCol);
			var robotPosition = new Position(robotRow, robotCol);
			var facing = Facing.East;
			var walls = new List<Wall> { new Wall(wallPosition) };
			var robot = new Robot(robotPosition, facing);

			var board = new Board(height, width, walls, robot);

			Assert.Equal(height, board.Height);
			Assert.Equal(width, board.Width);
			Assert.Single(board.Walls);
			Assert.NotNull(board.Robot);
			Assert.Equal(robot, board.Robot);
		}

		[Fact]
		public void PlaceWall_ValidPosition_ShouldAddWall()
		{
			var height = 5;
			var width = 5;
			var row = 2;
			var col = 2;
			var position = new Position(row, col);

			var board = new Board(height, width);
			var result = board.PlaceWall(position);

			Assert.True(result);
			Assert.Single(board.Walls);
			Assert.True(board.IsWallAt(position));
		}

		[Fact]
		public void PlaceWall_PositionWithRobot_ShouldReturnFalse()
		{
			var height = 5;
			var width = 5;
			var row = 2;
			var col = 2;
			var position = new Position(row, col);
			var facing = Facing.South;
			var robot = new Robot(position, facing);

			var board = new Board(height, width, robot: robot);
			var result = board.PlaceWall(position);

			Assert.False(result);
			Assert.Empty(board.Walls);
		}

		[Fact]
		public void PlaceWall_SamePositionTwice_ShouldReturnFalseSecondTime()
		{
			var height = 5;
			var width = 5;
			var row = 1;
			var col = 1;
			var position = new Position(row, col);

			var board = new Board(height, width);

			var first = board.PlaceWall(position);
			var second = board.PlaceWall(position);

			Assert.True(first);
			Assert.False(second);
			Assert.Single(board.Walls);
		}

		[Fact]
		public void IsWallAt_ReturnsTrue_WhenWallExists()
		{
			var height = 5;
			var width = 5;
			var row = 4;
			var col = 4;
			var position = new Position(row, col);

			var board = new Board(height, width);
			board.PlaceWall(position);

			Assert.True(board.IsWallAt(position));
		}

		[Fact]
		public void IsWallAt_ReturnsFalse_WhenWallDoesNotExist()
		{
			var height = 5;
			var width = 5;
			var row = 3;
			var col = 3;
			var position = new Position(row, col);

			var board = new Board(height, width);

			Assert.False(board.IsWallAt(position));
		}

		[Fact]
		public void IsRobotAt_ReturnsTrue_WhenRobotIsAtPosition()
		{
			var height = 5;
			var width = 5;
			var row = 2;
			var col = 2;
			var position = new Position(row, col);
			var facing = Facing.West;
			var robot = new Robot(position, facing);

			var board = new Board(height, width, robot: robot);

			Assert.True(board.IsRobotAt(position));
		}

		[Fact]
		public void IsRobotAt_ReturnsFalse_WhenNoRobot()
		{
			var height = 5;
			var width = 5;
			var row = 2;
			var col = 2;
			var position = new Position(row, col);
			var board = new Board(height, width);

			Assert.False(board.IsRobotAt(position));
		}

		[Fact]
		public void IsRobotAt_ReturnsFalse_WhenDifferentPosition()
		{
			var height = 5;
			var width = 5;
			var robotRow = 1;
			var robotCol = 1;
			var checkRow = 2;
			var checkCol = 2;
			var robotPosition = new Position(robotRow, robotCol);
			var checkPosition = new Position(checkRow, checkCol);
			var facing = Facing.South;
			var robot = new Robot(robotPosition, facing);

			var board = new Board(height, width, robot: robot);

			Assert.False(board.IsRobotAt(checkPosition));
		}
	}
}
