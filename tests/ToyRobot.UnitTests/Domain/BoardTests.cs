using ToyRobot.Domain;
using ToyRobot.UnitTests.Domain.TestData;
using ToyRobot.Domain.Exceptions;
using System.Reflection;

namespace ToyRobot.UnitTests.Domain
{
	public class BoardTests
	{
		private readonly int boardHeight;
		private readonly int boardWidth;

		private readonly int robotRow;
		private readonly int robotCol;
		private readonly int wallRow;
		private readonly int wallCol;

		private readonly Position initialRobotPosition;
		private readonly Facing initialFacing;

		public BoardTests()
		{
			// Board dimensions
			boardHeight = 5;
			boardWidth = 5;

			// Robot position
			robotRow = 1;
			robotCol = 1;
			initialRobotPosition = new Position(robotRow, robotCol);
			initialFacing = Facing.North;

			// Wall position
			wallRow = 2;
			wallCol = 2;
		}

		[Fact]
		public void Constructor_DefaultInputs_ShouldCreateBoardCorrectly()
		{
			var board = new Board();

			Assert.IsType<Board>(board);
			Assert.Equal(boardHeight, board.Height);
			Assert.Equal(boardWidth, board.Width);
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

		[Fact]
		public void PlaceRobot_ValidPosition_ShouldPlaceRobot()
		{
			var board = new Board(boardHeight, boardWidth);

			board.PlaceRobot(initialRobotPosition, initialFacing);

			Assert.NotNull(board.Robot);
			Assert.Equal(initialRobotPosition.Row, board.Robot.Position.Row);
			Assert.Equal(initialRobotPosition.Col, board.Robot.Position.Col);
			Assert.Equal(initialFacing, board.Robot.Facing);
		}

		[Fact]
		public void PlaceRobot_PositionOccupiedByWall_ShouldThrowRobotPlacementException()
		{
			var board = new Board(boardHeight, boardWidth);
			var wallPosition = new Position(wallRow, wallCol);
			board.PlaceWall(wallPosition);

			Assert.Throws<RobotPlacementException>(() => board.PlaceRobot(wallPosition, initialFacing));
		}

		[Theory]
		[MemberData(nameof(BoardTestData.TurnLeftData), MemberType = typeof(BoardTestData))]
		public void TurnRobotLeft_ShouldTurnCorrectly(Facing initial, Facing expected)
		{
			var board = new Board(boardHeight, boardWidth);
			board.PlaceRobot(initialRobotPosition, initial);

			board.TurnRobotLeft();

			Assert.NotNull(board.Robot);
			Assert.Equal(expected, board.Robot.Facing);
		}

		[Theory]
		[MemberData(nameof(BoardTestData.TurnRightData), MemberType = typeof(BoardTestData))]
		public void TurnRobotRight_ShouldTurnCorrectly(Facing initial, Facing expected)
		{
			var board = new Board(boardHeight, boardWidth);
			board.PlaceRobot(initialRobotPosition, initial);

			board.TurnRobotRight();

			Assert.NotNull(board.Robot);
			Assert.Equal(expected, board.Robot.Facing);
		}

		[Fact]
		public void Report_WithRobot_ShouldReturnCorrectFormat()
		{
			var board = new Board(boardHeight, boardWidth);
			board.PlaceRobot(initialRobotPosition, Facing.East);

			var report = board.Report();

			Assert.Equal($"{initialRobotPosition},{Facing.East.ToString().ToUpper()}", report);
		}

		[Fact]
		public void Report_WithoutRobot_ShouldReturnEmptyString()
		{
			var board = new Board(boardHeight, boardWidth);

			var report = board.Report();

			Assert.Equal(string.Empty, report);
		}

		[Fact]
		public void MoveRobot_ForwardWithoutWall_ShouldUpdatePosition()
		{
			var board = new Board(boardHeight, boardWidth);
			board.PlaceRobot(initialRobotPosition, Facing.North);

			// North decreases row
			var expectedRow = initialRobotPosition.Row - 1 < 1 ? boardHeight : initialRobotPosition.Row - 1;
			var expectedCol = initialRobotPosition.Col;

			board.MoveRobot();

			Assert.NotNull(board.Robot);
			Assert.Equal(expectedRow, board.Robot.Position.Row);
			Assert.Equal(expectedCol, board.Robot.Position.Col);
		}

		[Fact]
		public void MoveRobot_ForwardIntoWall_ShouldNotMove()
		{
			var board = new Board(boardHeight, boardWidth);
			var expectedCol = initialRobotPosition.Row - 1 < 1 ? boardHeight : initialRobotPosition.Row - 1;
			var wallPosition = new Position(initialRobotPosition.Col, expectedCol);
			board.PlaceWall(wallPosition);
			board.PlaceRobot(initialRobotPosition, Facing.North);

			board.MoveRobot();

			Assert.NotNull(board.Robot);
			Assert.Equal(initialRobotPosition.Row, board.Robot.Position.Row);
			Assert.Equal(initialRobotPosition.Col, board.Robot.Position.Col);
		}

		[Fact]
		public void MoveRobot_WrapAroundEdge_ShouldWrapPosition()
		{
			var board = new Board(boardHeight, boardWidth);
			// Place robot at top edge (row = 1)
			var topEdgePosition = new Position(3, 1);
			board.PlaceRobot(topEdgePosition, Facing.North);

			board.MoveRobot();

			var expectedCol = topEdgePosition.Col;
			var expectedRow = board.Width;

			Assert.NotNull(board.Robot);
			Assert.Equal(expectedRow, board.Robot.Position.Row);
			Assert.Equal(expectedCol, board.Robot.Position.Col);
		}


		[Fact]
		public void MoveRobot_WrapAroundEdge_EastAndWest_ShouldWrapCorrectly()
		{
			var board = new Board(boardHeight, boardWidth);

			// East wrap
			var eastEdge = new Position(boardWidth, 3);
			board.PlaceRobot(eastEdge, Facing.East);
			board.MoveRobot();

			Assert.NotNull(board.Robot);
			Assert.Equal(1, board.Robot.Position.Col);
			Assert.Equal(3, board.Robot.Position.Row);

			// West wrap
			var westEdge = new Position(1, 3);
			board.PlaceRobot(westEdge, Facing.West);
			board.MoveRobot();
			Assert.Equal(board.Width, board.Robot.Position.Col);
			Assert.Equal(3, board.Robot.Position.Row);
		}
	}
}
