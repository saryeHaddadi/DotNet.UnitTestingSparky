using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky;

public class BankAccount
{
	private readonly ILogBook _logBook;
	public int balance { get; set; }

	public BankAccount(ILogBook logbook)
	{
		_logBook = logbook;
		balance = 0;
	}

	public bool Deposit(int amount)
	{
		_logBook.Message("Deposit invoked"); // true
		balance += amount;
		return true;
	}

	public bool Withdraw(int amount)
	{
		if(amount <= balance)
		{
			balance -= amount;
			return true;
		}
		return false;
	}

	public int GetBalanace()
	{
		return balance;
	}
}
