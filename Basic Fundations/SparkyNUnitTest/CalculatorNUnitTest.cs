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

	[Test]
	public void IsOddNumber_InputOddNumber_ReturnTrue()
	{
		// Arrange
		var calc = new Calculator();
		var oddNumber = 3;

		// Act
		var actualResult = calc.IsOddNumber(oddNumber);

		// Assert
		var expected = true;
		Assert.AreEqual(expected, actualResult);
	}


	/// <summary>
	/// Test for multiple values
	/// </summary>
	/// <param name="evenNumber"></param>
	[Test]
	[TestCase(4)]
	[TestCase(6)]
	public void IsOddNumber_InputEvenNumber_ReturnFalse(int evenNumber)
	{
		// Arrange
		var calc = new Calculator();

		// Act
		var actualResult = calc.IsOddNumber(evenNumber);

		// Assert
		var expected = false;
		Assert.AreEqual(expected, actualResult);
		//Assert.That(isOdd, Is.EqualTo(false));
		//Assert.IsFalse();
	}

	[Test]
	[TestCase(10, ExpectedResult = false)]
	[TestCase(11, ExpectedResult = true)]
	public bool IsOddNumber_InputnNumber_ReturnTrueIfOdd(int number)
	{
		// Arrange
		var calc = new Calculator();

		// Act
		var actualResult = calc.IsOddNumber(number);

		// Assert
		return actualResult;
	}

	[Test]
	[TestCase(5.4, 10.5)] // 15.9
	[TestCase(5.43, 10.53)] // 15.96
	[TestCase(5.49, 10.59)] // 16.08
	public void AddNumbersDouble_InputTwoDouble_GetCorrectAddition(double a, double b)
	{
		// Arrange
		var calc = new Calculator();

		// Act
		var actualResult = calc.AddNumbersDouble(a, b);

		// Assert
		Assert.AreEqual(15.9, actualResult, delta: 0.2);
	}


	[Test]
	public void GetOddRange_InputMinAndMaxRange_ReturnValidOddNumberRange()
	{
		// Arrange
		var calc = new Calculator();

		// Act
		var actualResult = calc.GetOddRange(5, 10);

		// Assert
		var expectedOddRange = new List<int>() { 5, 7, 9 };
		Assert.That(actualResult, Is.EquivalentTo(expectedOddRange));
		Assert.AreEqual(expectedOddRange, actualResult);
		Assert.Contains(7, actualResult);
		Assert.That(actualResult, Does.Contain(7));
		Assert.That(actualResult, Is.Not.Empty);
		Assert.That(actualResult.Count, Is.EqualTo(3));
		Assert.That(actualResult, Has.No.Member(6));
		Assert.That(actualResult, Is.Ordered); // Is.Ordered.Descending
		Assert.That(actualResult, Is.Unique);
	}
}
