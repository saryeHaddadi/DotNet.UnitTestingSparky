using Moq;
using NUnit.Framework;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkyNUnitTest;

[TestFixture]
public class ProductNUnitTest
{
	[Test]
	public void GetPrice_PlatinumCustomer_ReturnPriceWith20Discount()
	{
		var product = new Product() { Price = 50 };
		var result = product.GetPrice(new Customer() { IsPlatinum = true });
		Assert.That(result, Is.EqualTo(40));

	}

	[Test]
	public void GetPriceMoqAbuse_PlatinumCustomer_ReturnPriceWith20Discount()
	{
		// Arrange
		var customer = new Mock<ICustomer>();
		customer.Setup(u => u.IsPlatinum).Returns(true);


		var product = new Product() { Price = 50 };
		var result = product.GetPrice(customer.Object);
		Assert.That(result, Is.EqualTo(40));

	}
}
