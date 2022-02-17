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
	//	Assert.That(account.GetBalanace, Is.EqualTo(100));
	//}

	[Test]
	public void BankDeposit_Add100_ReturnTrue()
	{
		var logMock = new Mock<ILogBook>();
		logMock.Setup(x => x.Message("My mock message"));
		var account = new BankAccount(logMock.Object);
		var result = account.Deposit(100);
		Assert.IsTrue(result);
		Assert.That(account.GetBalanace, Is.EqualTo(100));
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

}

