using System.Data;
using ToyRobot.Domain.Exceptions;
namespace ToyRobot.Domain
{
	/// <summary>
	/// Represents the board on which the robot and walls are placed.
	/// Manages the robot's position, board dimensions, and wall placements.
	/// </summary>
	public class Board
	{
		private readonly List<Wall> _walls = new();

		/// <summary>
		/// Gets a read-only list of all walls currently placed on the board.
		/// </summary>
		public IReadOnlyList<Wall> Walls => _walls.AsReadOnly();

		/// <summary>
		/// Gets the robot currently placed on the board, if any.
		/// </summary>
		public Robot? Robot { get; private set; }

		/// <summary>
		/// Gets the total number of rows (height) of the board.
		/// </summary>
		public int Height { get; }

		/// <summary>
		/// Gets the total number of columns (width) of the board.
		/// </summary>
		public int Width { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Board"/> class with the specified dimensions.
		/// </summary>
		/// <param name="height">The number of rows on the board. Must be greater than 0.</param>
		/// <param name="width">The number of columns on the board. Must be greater than 0.</param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when height or width is less than 1.</exception>
		public Board(int height = 5, int width = 5)
		{
			if (height < 1 || width < 1)
				throw InvalidPositionException.ForRowAndColumn(height, width);

			Height = height;
			Width = width;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Board"/> class with a robot and the specified dimensions.
		/// </summary>
		/// <param name="robot">The robot to place on the board.</param>
		/// <param name="height">The number of rows on the board. Must be greater than 0.</param>
		/// <param name="width">The number of columns on the board. Must be greater than 0.</param>
		/// <exception cref="InvalidPositionException">Thrown when height or width is less than 1.</exception>
		/// <exception cref="NullReferenceException">Thrown when the robot is null or its position is invalid.</exception>
		public Board(Robot robot, int height = 5, int width = 5)
		{
			if (height < 1 || width < 1)
				throw InvalidPositionException.ForRowAndColumn(height, width);

			Height = height;
			Width = width;
			Robot = IsValidPosition(robot.Position) ? robot : throw RobotPlacementException.BecauseInvalidPosition(robot.Position);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Board"/> class with optional walls, a robot, and specified dimensions.
		/// </summary>
		/// <param name="walls">An optional list of walls to place on the board.</param>
		/// <param name="robot">An optional robot to place on the board.</param>
		/// <param name="height">The number of rows on the board. Must be greater than 0.</param>
		/// <param name="width">The number of columns on the board. Must be greater than 0.</param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when height or width is less than 1.</exception>
		/// <exception cref="NullReferenceException">Thrown when the robot is provided but has an invalid position.</exception>
		public Board(int height, int width, List<Wall>? walls = null, Robot? robot = null)
		{
			if (height < 1 || width < 1)
				throw InvalidPositionException.ForRowAndColumn(height, width);

			Height = height;
			Width = width;
			_walls = walls ?? new List<Wall>();

			if (robot != null)
			{
				Robot = IsValidPosition(robot.Position) ? robot : throw RobotPlacementException.BecauseInvalidPosition(robot.Position);
			}

		}

		/// <summary>
		/// Attempts to place a wall at the specified position.
		/// </summary>
		/// <param name="position">The position where the wall should be placed.</param>
		/// <returns><c>true</c> if the wall was successfully placed; otherwise, <c>false</c>.</returns>
		public bool PlaceWall(Position position)
		{
			if (!IsValidPosition(position) || IsRobotAt(position) || IsWallAt(position))
				return false;

			_walls.Add(new Wall(position));
			return true;
		}

		/// <summary>
		/// Places the robot on the board at the specified position and facing direction.
		/// If a robot already exists, it will be moved to the new position and direction.
		/// </summary>
		/// <param name="position">The position where the robot should be placed.</param>
		/// <param name="facing">The direction the robot should face.</param>
		/// <exception cref="InvalidPositionException">Thrown when the position is outside the board boundaries.</exception>
		/// <exception cref="InvalidOperationException">Thrown when the target position is occupied by a wall.</exception>
		public void PlaceRobot(Position position, Facing facing)
		{
			if (!IsValidPosition(position))
				throw InvalidPositionException.ForRowAndColumn(position.Row, position.Col);

			if (IsWallAt(position))
				throw RobotPlacementException.BecauseWallIsPresent(position);

			Robot = new Robot(position, facing);
		}

		/// <summary>
		/// Moves the robot one space forward in its current facing direction.
		/// If the robot would move beyond the edge of the board, it wraps to the opposite side.
		/// If a wall is in front of the robot, the move is ignored.
		/// </summary>
		public void MoveRobot()
		{
			if (Robot == null) return;

			Position targetPosition = GetNextPosition(Robot.Position, Robot.Facing);

			// Wrap around if outside boundaries
			if (!IsValidPosition(targetPosition))
			{
				targetPosition = WrapPosition(Robot.Position, Robot.Facing);
			}

			// Ignore if there's a wall in front
			if (IsWallAt(targetPosition)) return;

			Robot = new Robot(targetPosition, Robot.Facing);
		}

		/// <summary>
		/// Turns the robot 90 degrees to the left (counter-clockwise).
		/// Does nothing if the robot has not been placed.
		/// </summary>
		public void TurnRobotLeft()
		{
			if (Robot == null) return;

			Facing newFacing = Robot.Facing switch
			{
				Facing.North => Facing.West,
				Facing.West => Facing.South,
				Facing.South => Facing.East,
				Facing.East => Facing.North,
				_ => Robot.Facing
			};

			Robot = new Robot(Robot.Position, newFacing);
		}

		/// <summary>
		/// Turns the robot 90 degrees to the right (clockwise).
		/// Does nothing if the robot has not been placed.
		/// </summary>
		public void TurnRobotRight()
		{
			if (Robot == null) return;

			Facing newFacing = Robot.Facing switch
			{
				Facing.North => Facing.East,
				Facing.East => Facing.South,
				Facing.South => Facing.West,
				Facing.West => Facing.North,
				_ => Robot.Facing
			};

			Robot = new Robot(Robot.Position, newFacing);
		}

		/// <summary>
		/// Reports the robot's current position and facing direction in the format "Row,Col,FACING".
		/// </summary>
		/// <returns>A string representing the robot's current state, or an empty string if no robot is placed.</returns>
		public string Report()
		{
			return Robot == null ? string.Empty : $"{Robot.Position},{Robot.Facing.ToString().ToUpper()}";
		}

		/// <summary>
		/// Calculates the next position based on the current position and facing direction.
		/// </summary>
		/// <param name="current">The current position.</param>
		/// <param name="facing">The direction the robot is facing.</param>
		/// <returns>The next position without applying board wrapping.</returns>
		private Position GetNextPosition(Position current, Facing facing)
		{
			return facing switch
			{
				Facing.North => new Position(current.Row + 1, current.Col),
				Facing.South => new Position(current.Row - 1, current.Col),
				Facing.East => new Position(current.Row, current.Col + 1),
				Facing.West => new Position(current.Row, current.Col - 1),
				_ => current
			};
		}

		/// <summary>
		/// Wraps the robot's position to the opposite side of the board if it moves beyond the boundary.
		/// </summary>
		/// <param name="current">The current position.</param>
		/// <param name="facing">The direction of movement.</param>
		/// <returns>The wrapped position.</returns>
		private Position WrapPosition(Position current, Facing facing)
		{
			return facing switch
			{
				Facing.North => new Position(1, current.Col),
				Facing.South => new Position(Height, current.Col),
				Facing.East => new Position(current.Row, 1),
				Facing.West => new Position(current.Row, Width),
				_ => current
			};
		}


		/// <summary>
		/// Determines whether there is a wall at the specified position.
		/// </summary>
		/// <param name="position">The position to check.</param>
		/// <returns><c>true</c> if a wall exists at the position; otherwise, <c>false</c>.</returns>
		public bool IsWallAt(Position position) => _walls.Any(w => w.Position.Equals(position));

		/// <summary>
		/// Determines whether the robot is currently at the specified position.
		/// </summary>
		/// <param name="position">The position to check.</param>
		/// <returns><c>true</c> if the robot is at the position; otherwise, <c>false</c>.</returns>
		public bool IsRobotAt(Position position) => Robot != null && Robot.Position.Equals(position);

		/// <summary>
		/// Checks whether the specified position is within the board boundaries.
		/// </summary>
		/// <param name="position">The position to validate.</param>
		/// <returns><c>true</c> if the position is within the board boundaries; otherwise, <c>false</c>.</returns>
		private bool IsValidPosition(Position position) =>
			position.Row >= 1 && position.Row <= Height &&
			position.Col >= 1 && position.Col <= Width;
	}
}
