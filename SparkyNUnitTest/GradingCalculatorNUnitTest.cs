using NUnit.Framework;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkyNUnitTest;

[TestFixture]
public class GradingCalculatorNUnitTest
{
	private GradingCalculator grader { get; set; }

	[SetUp]
	public void Setup()
	{
		grader = new GradingCalculator();
	}

}

