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
				throw new ArgumentOutOfRangeException("The height and the width must be greater that 0");

			Height = height;
			Width = width;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Board"/> class with a robot and the specified dimensions.
		/// </summary>
		/// <param name="robot">The robot to place on the board.</param>
		/// <param name="height">The number of rows on the board. Must be greater than 0.</param>
		/// <param name="width">The number of columns on the board. Must be greater than 0.</param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when height or width is less than 1.</exception>
		/// <exception cref="NullReferenceException">Thrown when the robot is null or its position is invalid.</exception>
		public Board(Robot robot, int height = 5, int width = 5)
		{
			if (height < 1 || width < 1)
				throw new ArgumentOutOfRangeException("The height and the width must be greater that 0");

			Robot = IsValidPosition(robot.Position) ? robot : throw new NullReferenceException("Robot cannot be null or undefined");
			Height = height;
			Width = width;
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
		public Board(List<Wall>? walls = null, Robot? robot = null, int height = 5, int width = 5)
		{
			if (height < 1 || width < 1)
				throw new ArgumentOutOfRangeException("The height and the width must be greater that 0");

			_walls = walls ?? new List<Wall>();
			if (robot != null)
			{
				Robot = IsValidPosition(robot.Position) ? robot : throw new NullReferenceException("Robot cannot be null or undefined");
			}

			Height = height;
			Width = width;
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
