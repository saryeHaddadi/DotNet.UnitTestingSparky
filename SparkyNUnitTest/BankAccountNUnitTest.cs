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

	[Test]
	public void BankDepositLogFakker_Add100_ReturnTrue()
	{
		var account = new BankAccount(new LogFakker());
		var result = account.Deposit(100);
		Assert.IsTrue(result);
		Assert.That(account.GetBalanace, Is.EqualTo(100));
	}

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
}

