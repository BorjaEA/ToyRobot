
namespace ToyRobot.Domain.Exceptions
{
	/// <summary>
	/// Exception thrown when a position is outside the allowed board boundaries.
	/// </summary>
	public class InvalidPositionException(string message, string paramName) : ArgumentException(message, paramName)
	{
		public static InvalidPositionException ForRow(int row) =>
			new InvalidPositionException($"Row '{row}' is out of bounds.", nameof(row));

		public static InvalidPositionException ForColumn(int col) =>
			new InvalidPositionException($"Column '{col}' is out of bounds.", nameof(col));

		public static InvalidPositionException ForRowAndColumn(int row, int col) =>
			new InvalidPositionException($"Row '{row}' or Column '{col}' are out of bounds.", $"{nameof(row)}, {nameof(col)}");
	}
}
