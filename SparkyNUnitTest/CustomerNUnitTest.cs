using NUnit.Framework;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkyNUnitTest;

[TestFixture]
public class CustomerNUnitTest
{
	private Customer _customer;

	[SetUp]
	public void Setup()
	{
		_customer = new Customer();
	}

	[Test]
	public void GreetAndCombineName_InputFirstAndLastName_ReturnFullName()
	{
		// Arrange

		// Act
		_customer.GreetAndCombineNames("Ben", "Spark");

		// Assert
		var expected = "Hello, Ben Spark";
		Assert.AreEqual(expected, _customer.GreetMessage);
		Assert.That(_customer.GreetMessage, Is.EqualTo(expected));
		Assert.That(_customer.GreetMessage, Does.Contain("ben spark").IgnoreCase);
		Assert.That(_customer.GreetMessage, Does.StartWith("Hello"));
		Assert.That(_customer.GreetMessage, Does.EndWith("Spark"));
		Assert.That(_customer.GreetMessage, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
	}

	[Test]
	public void GreetAndCombineName_NotGreeted_ReturnNull()
	{
		// Arrange

		// Act

		// Assert
		Assert.IsNull(_customer.GreetMessage);
	}
}
