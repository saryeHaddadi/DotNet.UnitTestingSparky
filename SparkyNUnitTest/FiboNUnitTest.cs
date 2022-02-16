using NUnit.Framework;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkyNUnitTest;

[TestFixture]
public class FiboNUnitTest
{
	private Fibo fiboGenerator;
	[SetUp]
	public void SetUp()
	{
		fiboGenerator = new Fibo();
	}

	[Test]
	public void Fibo_InputRange1_ReturnsRightFiboSerie()
	{
		// Arrange
		fiboGenerator.Range = 1;

		// Act
		var actualResult = fiboGenerator.GetFiboSeries();

		// Assert
		var expected = new List<int> { 0 };
		Assert.That(actualResult, Is.Not.Empty);
		Assert.That(actualResult, Is.Ordered);
		Assert.That(actualResult, Is.EquivalentTo(expected));
	}

	[Test]
	public void Fibo_InputRange6_ReturnsCount6()
	{
		// Arrange
		fiboGenerator.Range = 6;

		// Act
		var actualResult = fiboGenerator.GetFiboSeries().Count;

		// Assert
		var expected = 6;
		Assert.AreEqual(expected, actualResult);
	}

	[Test]
	public void Fibo_InputRange6_ReturnsContains3()
	{
		// Arrange
		fiboGenerator.Range = 6;

		// Act
		var actualResult = fiboGenerator.GetFiboSeries();

		// Assert
		Assert.That(actualResult, Does.Contain(3));
	}

	[Test]
	public void Fibo_InputRange6_ReturnsDontInclude()
	{
		// Arrange
		fiboGenerator.Range = 6;

		// Act
		var actualResult = fiboGenerator.GetFiboSeries();

		// Assert
		Assert.That(actualResult, Does.Not.Contain(4));	
	}

	[Test]
	public void Fibo_InputRange6_ReturnsRightResult()
	{
		// Arrange
		fiboGenerator.Range = 6;

		// Act
		var actualResult = fiboGenerator.GetFiboSeries();

		// Assert
		var expected = new List<int>() { 0, 1, 1, 2, 3, 5 };
		Assert.AreEqual(expected, actualResult);
	}
}

