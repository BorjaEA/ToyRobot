using System;
using ToyRobot.Application;
using ToyRobot.Domain;

namespace ToyRobot.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to the Toy Robot Simulator!");
			Console.WriteLine("Type commands to control the robot, or 'EXIT' to quit.");
			Console.WriteLine();
			Console.WriteLine("Available commands:");
			Console.WriteLine("  PLACE_ROBOT X,Y,FACING");
			Console.WriteLine("  PLACE_WALL X,Y");
			Console.WriteLine("  MOVE");
			Console.WriteLine("  LEFT");
			Console.WriteLine("  RIGHT");
			Console.WriteLine("  REPORT");
			Console.WriteLine();

			var board = new Board();
			var service = new RobotService(board);

			while (true)
			{
				Console.Write("> ");
				var input = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(input))
					continue;

				if (input.Trim().Equals("EXIT", StringComparison.OrdinalIgnoreCase))
				{
					Console.WriteLine("Exiting the simulator.");
					break;
				}

				try
				{
					var result = service.ExecuteCommand(input);
					if (!string.IsNullOrEmpty(result))
					{
						Console.WriteLine(result);
					}

				}
				catch
				{
					// The game throws exception but as instructed they arent shown in console
				}
			}
		}
	}
}
