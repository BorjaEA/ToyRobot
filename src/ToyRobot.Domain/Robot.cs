namespace ToyRobot.Domain
{
	public class Robot
	{
		public Position Position { get; private set; }
		public Facing Facing { get; private set; }

		public Robot(Position position, Facing facing)
		{
			Position = position ?? throw new ArgumentNullException(nameof(position));
			Facing = facing;
		}
	}
}
