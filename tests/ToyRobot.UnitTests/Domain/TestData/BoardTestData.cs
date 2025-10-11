using System.Collections.Generic;

namespace ToyRobot.UnitTests.Domain.TestData
{
	public static class BoardTestData
	{
		public static IEnumerable<object[]> InvalidDimensions =>
			new List<object[]>
			{
				new object[] { 0, 5 },
				new object[] { 5, 0 },
				new object[] { -1, 5 },
				new object[] { 5, -1 }
			};
	}
}
