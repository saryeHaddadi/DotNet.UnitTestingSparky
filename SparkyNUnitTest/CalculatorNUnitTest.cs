using NUnit.Framework;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkyNUnitTest;

[TestFixture]
public class CalculatorNUnitTest
{
	[Test]
	public void AddNumbers_InputTwoInt_GetCorrectAddition()
	{
		// Arrange
		var calc = new Calculator();

		// Act
		var actualResult = calc.AddNumbers(10, 20);

		// Assert
		var expected = 30;
		Assert.AreEqual(expected, actualResult);
	}
}
