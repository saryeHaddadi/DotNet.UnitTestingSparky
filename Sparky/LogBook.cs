using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky;

public interface ILogBook
{
	void Message(string messgage);
}

public class LogBook : ILogBook
{
	public void Message(string messgage)
	{
		Console.WriteLine(Message);
	}
}

public class LogFakker : ILogBook
{
	public void Message(string messgage)
	{
	}
}