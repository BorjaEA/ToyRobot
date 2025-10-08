using System.Data;

namespace ToyRobot.Domain
{
	public class Position
	{
		public int Row { get; }
		public int Col { get; }

		public Position(int row, int col)
		{
			/*
			TODO: Implement custom exception for outOfBound
			Maybe implement one for each axis
			*/
			if (row < 1 || row > 5)
			{
				throw new ArgumentException("Row is out of bounds", nameof(row));
			}
			if (col < 1 || col > 5)
			{
				throw new ArgumentException("Col is out of bounds", nameof(col));
			}

			Row = row;
			Col = col;
		}
		public override string ToString()
		{
			return $"{Row},{Col}";
		}
	}
}
