using ToyRobot.Application;
using ToyRobot.Domain;
using ToyRobot.Domain.Exceptions;
using ToyRobot.UnitTests.Application.TestData;
using Xunit;

namespace ToyRobot.UnitTests.Application
{
	public class RobotServiceTests
	{
		private readonly Board _board;
		private readonly RobotService _service;

		public RobotServiceTests()
		{
			_board = new Board();
			_service = new RobotService(_board);
		}

		[Theory]
		[MemberData(nameof(RobotServiceTestData.ValidPlacePositions), MemberType = typeof(RobotServiceTestData))]
		public void PlaceRobot_ValidPosition_ShouldPlaceRobotCorrectly(int col, int row, Facing facing)
		{
			var targetCol = col;
			var targetRow = row;
			var targetFacing = facing;

			_service.PlaceRobot(targetCol, targetRow, targetFacing);

			Assert.NotNull(_board.Robot);
			Assert.Equal(targetCol, _board.Robot.Position.Col);
			Assert.Equal(targetRow, _board.Robot.Position.Row);
			Assert.Equal(targetFacing, _board.Robot.Facing);
		}


		[Theory]
		[MemberData(nameof(RobotServiceTestData.InvalidPositions), MemberType = typeof(RobotServiceTestData))]
		public void PlaceRobot_InvalidPosition_ShouldThrowInvalidPositionException(int row, int col)
		{
			var invalidRow = row;
			var invalidCol = col;

			Assert.Throws<InvalidPositionException>(() => _service.PlaceRobot(invalidRow, invalidCol, Facing.North));
		}

		[Theory]
		[MemberData(nameof(RobotServiceTestData.TurnLeftExpectations), MemberType = typeof(RobotServiceTestData))]
		public void TurnRobotLeft_ShouldUpdateFacing(Facing initial, Facing expected)
		{
			var initialFacing = initial;
			var expectedFacing = expected;
			var startRow = 1;
			var startCol = 1;

			_service.PlaceRobot(startRow, startCol, initialFacing);

			_service.TurnRobotLeft();

			Assert.NotNull(_board.Robot);
			Assert.Equal(expectedFacing, _board.Robot.Facing);
		}

		[Theory]
		[MemberData(nameof(RobotServiceTestData.TurnRightExpectations), MemberType = typeof(RobotServiceTestData))]
		public void TurnRobotRight_ShouldUpdateFacing(Facing initial, Facing expected)
		{
			var initialFacing = initial;
			var expectedFacing = expected;
			var startRow = 1;
			var startCol = 1;

			_service.PlaceRobot(startRow, startCol, initialFacing);

			_service.TurnRobotRight();

			Assert.NotNull(_board.Robot);
			Assert.Equal(expectedFacing, _board.Robot.Facing);
		}

		[Fact]
		public void MoveRobot_Forward_ShouldMoveRobot()
		{
			var initialRow = 2;
			var initialCol = 2;
			var facing = Facing.North;

			_service.PlaceRobot(initialCol, initialRow, facing);
			_service.MoveRobot();

			var expectedRow = initialRow - 1;
			var expectedCol = initialCol;

			Assert.NotNull(_board.Robot);
			Assert.Equal(expectedRow, _board.Robot.Position.Row);
			Assert.Equal(expectedCol, _board.Robot.Position.Col);
		}

		[Fact]
		public void MoveRobot_ForwardIntoWall_ShouldNotMove()
		{
			var robotRow = 2;
			var robotCol = 2;
			var wallRow = robotRow - 1;
			var wallCol = robotCol;
			var facing = Facing.North;

			var wallPosition = new Position(wallCol, wallRow);
			_board.PlaceWall(wallPosition);

			_service.PlaceRobot(robotCol, robotRow, facing);
			_service.MoveRobot();

			Assert.NotNull(_board.Robot);
			Assert.Equal(robotRow, _board.Robot.Position.Row);
			Assert.Equal(robotCol, _board.Robot.Position.Col);
		}

		[Fact]
		public void Report_ShouldReturnCorrectFormat()
		{
			var row = 3;
			var col = 3;
			var facing = Facing.East;

			_service.PlaceRobot(col, row, facing);

			var expectedReport = $"{col},{row},{facing.ToString().ToUpper()}";
			var actualReport = _service.Report();

			Assert.Equal(expectedReport, actualReport);
		}

		[Theory]
		[MemberData(nameof(RobotServiceCommandTestData.CommandSequences), MemberType = typeof(RobotServiceCommandTestData))]
		public void ExecuteCommands_ShouldResultInExpectedPositionAndFacing(
			string[] commands,
			Position expectedPosition,
			Facing expectedFacing,
			List<Position>? walls = null)
		{
			if (walls != null)
			{
				foreach (var wall in walls)
				{
					_board.PlaceWall(wall);
				}
			}

			foreach (var command in commands)
			{
				_service.ExecuteCommand(command);
			}

			Assert.NotNull(_board.Robot);
			Assert.Equal(expectedPosition.Row, _board.Robot.Position.Row);
			Assert.Equal(expectedPosition.Col, _board.Robot.Position.Col);
			Assert.Equal(expectedFacing, _board.Robot.Facing);
		}
		[Theory]
		[MemberData(nameof(RobotServiceCommandTestData.CommandSequencesFromPDF), MemberType = typeof(RobotServiceCommandTestData))]
		public void ExecuteCommandSequence_ShouldEndAtExpectedPosition(
			string[] commands,
			Position expectedPosition,
			Facing expectedFacing,
			List<Position>? walls = null)
		{
			if (walls != null)
			{
				foreach (var wall in walls)
					_board.PlaceWall(wall);
			}

			foreach (var command in commands)
			{
				_service.ExecuteCommand(command);
			}

			Assert.NotNull(_board.Robot);
			Assert.Equal(expectedPosition.Row, _board.Robot.Position.Row);
			Assert.Equal(expectedPosition.Col, _board.Robot.Position.Col);
			Assert.Equal(expectedFacing, _board.Robot.Facing);
			Assert.Equal($"{expectedPosition.Col},{expectedPosition.Row},{expectedFacing.ToString().ToUpper()}", _service.Report());
		}
	}
}
