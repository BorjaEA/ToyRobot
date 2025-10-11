using ToyRobot.Domain.Exceptions;

namespace ToyRobot.Domain
{
	/// <summary>
	/// Represents a position on the board using row and column coordinates.
	/// </summary>
	public class Position
	{
		/// <summary>
		/// Gets the row coordinate of the position.
		/// </summary>
		public int Row { get; }

		/// <summary>
		/// Gets the column coordinate of the position.
		/// </summary>
		public int Col { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Position"/> class with the specified row and column.
		/// </summary>
		/// <param name="row">The row coordinate (must be between 1 and 5).</param>
		/// <param name="col">The column coordinate (must be between 1 and 5).</param>
		/// <exception cref="ArgumentException">
		/// Thrown when <paramref name="row"/> or <paramref name="col"/> is out of the allowed range.
		/// </exception>
		public Position(int row, int col)
		{

			if (row < 1)
				throw InvalidPositionException.ForRow(row);

			if (col < 1)
				throw InvalidPositionException.ForColumn(col);

			Row = row;
			Col = col;
		}

		/// <summary>
		/// Returns a string representation of the position in the format "Row,Col".
		/// </summary>
		/// <returns>A string representing the current position.</returns>
		public override string ToString()
		{
			return $"{Row},{Col}";
		}
	}
}
