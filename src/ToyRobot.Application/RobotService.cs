using ToyRobot.Domain;
using ToyRobot.Domain.Exceptions;

namespace ToyRobot.Application
{
	public class RobotService
	{
		private readonly Board _board;

		public RobotService(Board board)
		{
			_board = board;
		}

		public void PlaceRobot(int row, int col, Facing facing)
		{
			var position = new Position(row, col);
			_board.PlaceRobot(position, facing);
		}

		public void MoveRobot() => _board.MoveRobot();

		public void TurnRobotLeft() => _board.TurnRobotLeft();

		public void TurnRobotRight() => _board.TurnRobotRight();

		public string Report() => _board.Report();

		/// <summary>
		/// Executes a single command string, e.g., "PLACE_WALL 1,2,NORTH", "MOVE", "LEFT".
		/// </summary>
		public string ExecuteCommand(string input)
		{
			if (string.IsNullOrWhiteSpace(input)) return string.Empty;

			try
			{
				var parts = input.Trim().Split(' ');
				var command = parts[0].ToUpper();

				switch (command)
				{
					case "PLACE_ROBOT":
						var args = parts[1].Split(',');
						int row = int.Parse(args[0]);
						int col = int.Parse(args[1]);
						Facing facing = Enum.Parse<Facing>(args[2], true);
						PlaceRobot(row, col, facing);
						break;

					case "PLACE_WALL":
						var wallArgs = parts[1].Split(',');
						int wallRow = int.Parse(wallArgs[0]);
						int wallCol = int.Parse(wallArgs[1]);
						_board.PlaceWall(new Position(wallRow, wallCol));
						break;

					case "MOVE":
						MoveRobot();
						break;

					case "LEFT":
						TurnRobotLeft();
						break;

					case "RIGHT":
						TurnRobotRight();
						break;

					case "REPORT":
						Report();
						break;

					default:
						// Ignore unknown commands
						break;
				}
			}
			catch (Exception)
			{
				// Ignore invalid command or invalid position and continue
			}
			return string.Empty;
		}

		/// <summary>
		/// Executes a sequence of commands in order.
		/// </summary>
		public string ExecuteCommands(IEnumerable<string> commands)
		{
			string lastReport = string.Empty;
			foreach (var cmd in commands)
			{
				var result = ExecuteCommand(cmd);
				if (!string.IsNullOrEmpty(result))
					lastReport = result;
			}
			return lastReport;
		}
	}
}
