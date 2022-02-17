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
public class BankAccountNUnitTest
{
	private BankAccount bankAccount;

	[SetUp]
	public void Setup()
	{
		
	}

	//[Test]
	//public void BankDepositLogFakker_Add100_ReturnTrue()
	//{
	//	var account = new BankAccount(new LogFakker());
	//	var result = account.Deposit(100);
	//	Assert.IsTrue(result);
	//	Assert.That(account.GetBalance, Is.EqualTo(100));
	//}

	[Test]
	public void BankDeposit_Add100_ReturnTrue()
	{
		var logMock = new Mock<ILogBook>();
		logMock.Setup(x => x.Message("My mock message"));
		var account = new BankAccount(logMock.Object);
		var result = account.Deposit(100);
		Assert.IsTrue(result);
		Assert.That(account.GetBalance, Is.EqualTo(100));
	}

	[Test]
	[TestCase(200, 100)]
	[TestCase(200, 150)]
	public void BankWithdraw_Withdraw100With200Balance_ReturnTrue(int balance, int withdraw)
	{
		// Arrange
		var logMock = new Mock<ILogBook>();
		// If the "It is" is validated, the mock returns true. Otherwise, false.
		// But you can add a line to say it explicitely
		logMock.Setup(u => u.LogBalanceAfterWithdraw(It.Is<int>(x => x >= 0))).Returns(true);
		// logMock.Setup(u => u.LogBalanceAfterWithdraw(It.Is<int>(x => x < 0))).Returns(false);
		var bankAccount = new BankAccount(logMock.Object);
		bankAccount.Deposit(balance);

		// Act
		var actualResult = bankAccount.Withdraw(withdraw);

		// Assert
		Assert.IsTrue(actualResult);
	}

	[Test]
	[TestCase(200, 300)]
	public void BankWithdraw_Withdraw300With200Balance_ReturnFalse(int balance, int withdraw)
	{
		// Arrange
		var logMock = new Mock<ILogBook>();
		// If the "It is" is validated, the mock returns true. Otherwise, false (default values).
		// For string, the default value is null, so beaware!
		// But you can add a line to configure the complementary case explicitely
		logMock.Setup(u => u.LogBalanceAfterWithdraw(It.Is<int>(x => x >= 0))).Returns(true);
		logMock.Setup(u => u.LogBalanceAfterWithdraw(It.Is<int>(x => x < 0))).Returns(false);
		logMock.Setup(u => u.LogBalanceAfterWithdraw(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);
		var bankAccount = new BankAccount(logMock.Object);
		bankAccount.Deposit(balance);

		// Act
		var actualResult = bankAccount.Withdraw(withdraw);

		// Assert
		Assert.IsFalse(actualResult);
	}

	[Test]
	public void BankLogDummy_LogMockString_ReturnTrue()
	{
		// Arrange
		var logMock = new Mock<ILogBook>();
		logMock.Setup(u => u.MessgeWithReturnStr(It.IsAny<string>())).Returns((string str) => str.ToLower());
		var desiredOutput = "hello";

		// Act

		// Assert
		Assert.That(logMock.Object.MessgeWithReturnStr("HELLo"), Is.EqualTo(desiredOutput));
	}


	[Test]
	public void BankLogDummy_LogMockStringOutputStr_ReturnTrue()
	{
		// Arrange
		var logMock = new Mock<ILogBook>();
		string desiredOutput = "hello";
		logMock.Setup(u => u.LogWithOutputResult(It.IsAny<string>(), out desiredOutput)).Returns(true);

		// Act

		// Assert
		var actualOutput = "";
		Assert.IsTrue(logMock.Object.LogWithOutputResult("Ben", out actualOutput));
		Assert.That(actualOutput, Is.EqualTo(desiredOutput));
	}

	[Test]
	public void BankLogDummy_LogRefChecker_ReturnsTrue()
	{
		// Arrange
		var logMock = new Mock<ILogBook>();
		var customer = new Customer();
		var customerNotUsed = new Customer();
		logMock.Setup(u => u.LogWithRefObj(ref customer)).Returns(true);

		// Act

		// Assert
		Assert.IsTrue(logMock.Object.LogWithRefObj(ref customer));
		Assert.IsFalse(logMock.Object.LogWithRefObj(ref customerNotUsed));
	}

	[Test]
	public void BankLogDummy_SetAndGetLogTypeAndSeverityMock_MockTest()
	{
		// Arrange
		var logMock = new Mock<ILogBook>();
		logMock.SetupAllProperties();
		logMock.Setup(u => u.LogSeverity).Returns(10);
		logMock.Setup(u => u.Logtype).Returns("warning");
		// You can not set value to Mock properties like that, it will not work.
		// To make it work, you have to call SetupAllProperties().
		// Calling SetupAllProperties(), will reset what was done before.
		// So mmake sure you call this at the beginning of your script.
		logMock.Object.LogSeverity = 100;

		// Act

		// Assert
		Assert.That(logMock.Object.LogSeverity, Is.EqualTo(100));
		Assert.That(logMock.Object.Logtype, Is.EqualTo("warning"));

		// Callback (str): usefull when you want the returned value to be modified, and then asserted.
		// Here, gets back the value passed to LogToDb, and acts upon it.
		var logTemp = "Hello, ";
		logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
			.Returns(true).Callback((string str) => logTemp += str);

		// Act
		logMock.Object.LogToDb("Ben");

		// Assert
		Assert.That(logTemp, Is.EqualTo("Hello, Ben"));

		// Callback (int). To be fancy, here he use the callback before & after the returns.
		// So it gets executed two times.
		var counter = 5;
		logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
			.Callback((string str) => counter++)
			.Returns(true).Callback((string str) => counter++);

		// Act
		logMock.Object.LogToDb("Ben"); // +2
		logMock.Object.LogToDb("Ben"); // +2

		// Assert
		Assert.That(counter, Is.EqualTo(9));

	}

	/// <summary>
	/// Check whether a method was called, how many time was it called, and if a property was accessed.
	/// </summary>
	[Test]
	public void BankLogDummmy_VerifyExample()
	{
		// Arrange
		var logMock = new Mock<ILogBook>();
		var bankAccount = new BankAccount(logMock.Object);

		// Act
		bankAccount.Deposit(100);

		// Assert
		Assert.That(bankAccount.GetBalance, Is.EqualTo(100));

		// Verity
		logMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(2));
		logMock.Verify(u => u.Message("Test"), Times.AtLeastOnce);
		logMock.VerifySet(u => u.LogSeverity = 101, Times.Once);
		logMock.VerifyGet(u => u.LogSeverity, Times.Once);
	}
}



