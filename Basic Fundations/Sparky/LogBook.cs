using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky;

public interface ILogBook
{
	public int LogSeverity { get; set; }
	public string Logtype { get; set; }

	void Message(string message);
	bool LogToDb(string message);
	bool LogBalanceAfterWithdraw(int balanceAfterWithdraw);
	string MessgeWithReturnStr(string message);
	bool LogWithOutputResult(string str, out string outputStr);
	bool LogWithRefObj(ref Customer customer);
}

public class LogBook : ILogBook
{
	public int LogSeverity { get; set; }
	public string Logtype { get; set; }

	public bool LogBalanceAfterWithdraw(int balanceAfterWithdraw)
	{
		if (balanceAfterWithdraw >= 0)
		{
			Console.WriteLine("Success");
			return true;
		}
		Console.WriteLine("Failure");
		return false;
	}

	public bool LogToDb(string message)
	{
		Console.WriteLine(message);
		return true;
	}

	public bool LogWithOutputResult(string str, out string outputStr)
	{
		outputStr = "Hello " + str;
		return true;
	}

	public bool LogWithRefObj(ref Customer customer)
	{
		return true;
	}

	public void Message(string message)
	{
		Console.WriteLine(message);
	}

	public string MessgeWithReturnStr(string message)
	{
		Console.WriteLine(message);
		return message.ToLower();
	}
}

//public class LogFakker : ILogBook
//{
//	public void Message(string messgage)
//	{
//	}
//}