namespace ToyRobot.Domain.Exceptions
{
	/// <summary>
	/// Represents an error that occurs when a position (row, column, or both) is outside the valid boundaries of the board.
	/// </summary>
	/// <remarks>
	/// This exception is typically thrown from the <see cref="Position"/> class when attempting to create
	/// a position with invalid row or column values. Use the static factory methods to create
	/// standardized exceptions for row, column, or both out-of-bound errors.
	/// </remarks>
	public class InvalidPositionException(string message, string paramName)
		: ArgumentException(message, paramName)
	{
		/// <summary>
		/// Creates a new <see cref="InvalidPositionException"/> for an invalid row value.
		/// </summary>
		/// <param name="row">The row value that caused the exception.</param>
		/// <returns>
		/// A new instance of <see cref="InvalidPositionException"/> with a message indicating that the row is out of bounds.
		/// </returns>
		public static InvalidPositionException ForRow(int row) =>
			new InvalidPositionException($"Row '{row}' is out of bounds.", nameof(row));

		/// <summary>
		/// Creates a new <see cref="InvalidPositionException"/> for an invalid column value.
		/// </summary>
		/// <param name="col">The column value that caused the exception.</param>
		/// <returns>
		/// A new instance of <see cref="InvalidPositionException"/> with a message indicating that the column is out of bounds.
		/// </returns>
		public static InvalidPositionException ForColumn(int col) =>
			new InvalidPositionException($"Column '{col}' is out of bounds.", nameof(col));

		/// <summary>
		/// Creates a new <see cref="InvalidPositionException"/> for invalid row and column values.
		/// </summary>
		/// <param name="row">The row value that caused the exception.</param>
		/// <param name="col">The column value that caused the exception.</param>
		/// <returns>
		/// A new instance of <see cref="InvalidPositionException"/> with a message indicating that
		/// both the row and column are out of bounds.
		/// </returns>
		public static InvalidPositionException ForRowAndColumn(int row, int col) =>
			new InvalidPositionException(
				$"Row '{row}' or Column '{col}' are out of bounds.",
				$"{nameof(row)}, {nameof(col)}"
			);
	}
}
