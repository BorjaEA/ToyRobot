using System;

namespace ToyRobot.Domain
{
	/// <summary>
	/// Represents the robot placed on the board, holding its position and facing direction.
	/// </summary>
	public class Robot
	{
		/// <summary>
		/// Gets the current position of the robot on the board.
		/// </summary>
		public Position Position { get; private set; }

		/// <summary>
		/// Gets the direction the robot is currently facing.
		/// </summary>
		public Facing Facing { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Robot"/> class with a position and facing direction.
		/// </summary>
		/// <param name="position">The initial position of the robot.</param>
		/// <param name="facing">The initial facing direction of the robot.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="position"/> is null.</exception>
		public Robot(Position position, Facing facing)
		{
			Position = position ?? throw new ArgumentNullException(nameof(position));
			Facing = facing;
		}
	}
}
