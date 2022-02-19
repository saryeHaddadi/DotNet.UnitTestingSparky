using Bongo.Models.ModelValidations;
using NUnit.Framework;

namespace Bongo.Models.Tests;

[TestFixture]
public class DateInFutureAttributeTests
{
	[TestCase(100, ExpectedResult = true)]
	[TestCase(-100, ExpectedResult = false)]
	[TestCase(0, ExpectedResult = false)]
	public bool DateValidator_InputExpectedDateRange_DateValidity(int addTime)
	{
		// Arrange
		var dateInFutureAttribute = new DateInFutureAttribute(() => DateTime.Now);

		// Act
		return dateInFutureAttribute.IsValid(DateTime.Now.AddSeconds(addTime));
;	}

	[Test]
	public void DateValidator_AnyDate_ReturnErrorMassage()
	{
		var actualResult = new DateInFutureAttribute();
		var expeced = "Date must be in the future";
		Assert.AreEqual(expeced, actualResult.ErrorMessage);
	}
}
