using ToyRobot.Domain;
using ToyRobot.UnitTests.TestData;
using ToyRobot.Domain.Exceptions;

namespace ToyRobot.UnitTests.Domain
{
	public class PositionTests
	{
		[Theory]
		[MemberData(nameof(PositionTestData.InvalidRows), MemberType = typeof(PositionTestData))]
		public void Constructor_InvalidRow_ShouldThrowException(int row, int col)
		{
			Assert.Throws<InvalidPositionException>(() => new Position(row, col));
		}

		[Theory]
		[MemberData(nameof(PositionTestData.InvalidCols), MemberType = typeof(PositionTestData))]
		public void Constructor_InvalidCol_ShouldThrowException(int row, int col)
		{
			Assert.Throws<InvalidPositionException>(() => new Position(row, col));
		}
	}
}
