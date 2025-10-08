
namespace ToyRobot.Domain
{
	public class Board
	{
		private readonly List<Wall> _walls = new();
		public IReadOnlyList<Wall> Walls => _walls.AsReadOnly();

		public Robot? Robot { get; private set; }
		public int Height { get; }
		public int Width { get; }

		//Board only constructor
		public Board(int height = 5, int width = 5)
		{
			if (height < 1 || width < 1)
			{
				throw new ArgumentOutOfRangeException("The height and the width must be greater that 0");
			}

			Height = height;
			Width = width;
		}

		//Board with robot constructor
		public Board(Robot robot, int height = 5, int width = 5)
		{
			//TODO: Check if robot is inside of the board
			if (robot == null) { throw new NullReferenceException("Robot cannot be null or undefined"); }
			if (height < 1 || width < 1)
			{
				throw new ArgumentOutOfRangeException("The height and the width must be greater that 0");
			}

			Robot = robot;
			Height = height;
			Width = width;
		}

		//Board with Walls constructor
		public Board(List<Wall>? walls = null, Robot? robot = null, int height = 5, int width = 5)
		{
			//TODO: Check if robot is inside of the board
			if (robot == null) throw new NullReferenceException("Robot cannot be null or undefined");
			if (height < 1 || width < 1)
			{
				throw new ArgumentOutOfRangeException("The height and the width must be greater that 0");
			}

			_walls = walls ?? new List<Wall>();
			Robot = robot;
			Height = height;
			Width = width;
		}

		public bool placeWall(Position position)
		{
			if (!IsValidPosition(position) || IsRobotAt(position) || IsWallAt(position)) return false;

			_walls.Add(new Wall(position));
			return true;
		}

		public bool IsWallAt(Position position) => _walls.Any(w => w.Position.Equals(position));
		public bool IsRobotAt(Position position) => Robot != null && Robot.Position.Equals(position);


		private bool IsValidPosition(Position position) =>
			position.Row >= 1 && position.Row <= Height &&
			position.Col >= 1 && position.Col <= Width;
	}
}
