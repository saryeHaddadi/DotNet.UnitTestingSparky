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

	[Test]
	public void GetGrade_InputScore95AndAttendance90_ReturnsAGrade()
	{
		// Arrange
		grader.Score = 95;
		grader.AttendancePercentage = 90;

		// Act
		var actualResult = grader.GetGrade();

		// Assert
		var expected = "A";
		Assert.AreEqual(expected, actualResult);
	}

	[Test]
	public void GetGrade_InputScore85AndAttendance90_ReturnsBGrade()
	{
		// Arrange
		grader.Score = 85;
		grader.AttendancePercentage = 90;

		// Act
		var actualResult = grader.GetGrade();

		// Assert
		var expected = "B";
		Assert.AreEqual(expected, actualResult);
	}

	[Test]
	public void GetGrade_InputScore65AndAttendance90_ReturnsCGrade()
	{
		// Arrange
		grader.Score = 65;
		grader.AttendancePercentage = 90;

		// Act
		var actualResult = grader.GetGrade();

		// Assert
		var expected = "C";
		Assert.AreEqual(expected, actualResult);
	}

	[Test]
	public void GetGrade_InputScore95AndAttendance65_ReturnsBGrade()
	{
		// Arrange
		grader.Score = 95;
		grader.AttendancePercentage = 65;

		// Act
		var actualResult = grader.GetGrade();

		// Assert
		var expected = "B";
		Assert.AreEqual(expected, actualResult);
	}

	[Test]
	[TestCase(95, 55)]
	[TestCase(65, 55)]
	[TestCase(50, 90)]
	public void GetGrade_FailureScenarios_ReturnsFGrade(int score, int attendancePercentage)
	{
		// Arrange
		grader.Score = score;
		grader.AttendancePercentage = attendancePercentage;

		// Act
		var actualResult = grader.GetGrade();

		// Assert
		var expected = "F";
		Assert.AreEqual(expected, actualResult);
	}

	[Test]
	[TestCase(95, 90, ExpectedResult = "A")]
	[TestCase(85, 90, ExpectedResult = "B")]
	[TestCase(65, 90, ExpectedResult = "C")]
	[TestCase(95, 65, ExpectedResult = "B")]
	[TestCase(95, 55, ExpectedResult = "F")]
	[TestCase(65, 55, ExpectedResult = "F")]
	[TestCase(50, 90, ExpectedResult = "F")]
	public string GetGrade_AllScenarios_ReturnsGrade(int score, int attendancePercentage)
	{
		// Arrange
		grader.Score = score;
		grader.AttendancePercentage = attendancePercentage;

		// Act
		return grader.GetGrade();
	}

}

