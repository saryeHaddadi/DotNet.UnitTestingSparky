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
	private Customer customer;

	[SetUp]
	public void Setup()
	{
		customer = new Customer();
	}

	[Test]
	public void GreetAndCombineName_InputFirstAndLastName_ReturnFullName()
	{
		// Arrange

		// Act
		customer.GreetAndCombineNames("Ben", "Spark");

		// Assert
		var expected = "Hello, Ben Spark";
		Assert.Multiple(() =>
		{
			Assert.AreEqual(expected, customer.GreetMessage);
			Assert.That(customer.GreetMessage, Is.EqualTo(expected));
			Assert.That(customer.GreetMessage, Does.Contain("ben spark").IgnoreCase);
			Assert.That(customer.GreetMessage, Does.StartWith("Hello"));
			Assert.That(customer.GreetMessage, Does.EndWith("Spark"));
			Assert.That(customer.GreetMessage, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
		});
	}

	[Test]
	public void GreetAndCombineName_NotGreeted_ReturnNull()
	{
		// Arrange

		// Act

		// Assert
		Assert.IsNull(customer.GreetMessage);
	}

	[Test]
	public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
	{
		var actualResult = customer.Discount;
		Assert.That(actualResult, Is.InRange(10, 25));
	}

	[Test]
	public void GreetMessage_GreetedWithoutLastName_ReturnsNotNull()
	{
		customer.GreetAndCombineNames("ben", "");
		Assert.IsNotNull(customer.GreetMessage);
	}

	[Test]
	public void GreetMessage_GreetedWithoutFirstName_ThrowsException()
	{
		var exceptionDetails = Assert.Throws<ArgumentException>(() =>
			customer.GreetAndCombineNames("", "Spark")
		);
		Assert.AreEqual("Empty First Name", exceptionDetails.Message);
		Assert.That(() => customer.GreetAndCombineNames("", "Spark"),
			Throws.ArgumentException.With.Message.EqualTo("Empty First Name"));
	}

	[Test]
	public void GreetMessage_GreetedWithoutFirstName_ThrowsExceptionV2()
	{
		Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Spark"));
		Assert.That(() => customer.GreetAndCombineNames("", "Spark"), Throws.ArgumentException);
	}

	[Test]
	public void CustomerType_CreateCustomerWithLessThan100Order_ReturnBasicCustomer()
	{
		customer.OrderTotal = 10;
		var actualResult = customer.GetCustomerType();
		Assert.That(actualResult, Is.TypeOf<BasicCustomer>());
	}

	[Test]
	public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnPlatinumCustomer()
	{
		customer.OrderTotal = 100;
		var actualResult = customer.GetCustomerType();
		Assert.That(actualResult, Is.TypeOf<PlatinumCustomer>());
	}
}
