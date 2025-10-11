using System;
using System.Collections.Generic;

namespace ToyRobot.UnitTests.TestData
{
	public class PositionTestData
	{
		public static IEnumerable<object[]> InvalidRows => new List<object[]>
		{
			new object[]{0,3},
			new object[]{6,3}
		};

		public static IEnumerable<object[]> InvalidCols => new List<object[]>
		{
			new object[]{3,0},
			new object[]{3,6}
		};
	}
}
