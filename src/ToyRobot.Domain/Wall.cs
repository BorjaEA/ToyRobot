namespace ToyRobot.Domain
{
	public class Wall
	{
		public Position Position { get; }

		public Wall(Position position)
		{
			Position = position ?? throw new ArgumentNullException(nameof(position));
		}

		public override string ToString()
		{
			return $"Wall postion at {Position}";
		}
	}
}
