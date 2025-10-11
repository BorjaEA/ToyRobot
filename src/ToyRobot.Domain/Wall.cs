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
		/// Determines whether this wall is equal to another wall by comparing positions.
		/// </summary>
		/// <param name="obj">The object to compare with.</param>
		/// <returns><c>true</c> if the walls are at the same position; otherwise, <c>false</c>.</returns>
		public override bool Equals(object? obj)
		{
			if (obj is not Wall other) return false;
			return Position.Equals(other.Position);
		}

		/// <summary>
		/// Returns a hash code for this wall based on its position.
		/// </summary>
		/// <returns>An integer hash code.</returns>
		public override int GetHashCode() => Position.GetHashCode();

		/// <summary>
		/// Returns a string representation of the wall's position.
		/// </summary>
		/// <returns>A string in the format "Wall position at Row,Col".</returns>
		public override string ToString() => $"Wall position at {Position}";
	}
}
