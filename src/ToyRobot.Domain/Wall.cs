using System;

namespace ToyRobot.Domain
{
	/// <summary>
	/// Represents an immovable wall placed on the board at a specific position.
	/// </summary>
	public class Wall
	{
		/// <summary>
		/// Gets the position of the wall on the board.
		/// </summary>
		public Position Position { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Wall"/> class with the specified position.
		/// </summary>
		/// <param name="position">The position of the wall.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="position"/> is null.</exception>
		public Wall(Position position)
		{
			Position = position ?? throw new ArgumentNullException(nameof(position));
		}

		/// <summary>
		/// Returns a string representation of the wall's position.
		/// </summary>
		/// <returns>A string in the format "Wall position at Row,Col".</returns>
		public override string ToString()
		{
			return $"Wall position at {Position}";
		}
	}
}
