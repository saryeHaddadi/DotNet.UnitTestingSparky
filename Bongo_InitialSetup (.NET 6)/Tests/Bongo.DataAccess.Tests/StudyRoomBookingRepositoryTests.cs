using Bongo.DataAccess.Repository;
using Bongo.Models.Model;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections;

namespace Bongo.DataAccess.Tests;

[TestFixture]
public class StudyRoomBookingRepositoryTests
{
	private StudyRoomBooking studyRoomBooking_One;
	private StudyRoomBooking studyRoomBooking_Two;
	private DbContextOptions<ApplicationDbContext> options;

	public StudyRoomBookingRepositoryTests()
	{
		studyRoomBooking_One = new StudyRoomBooking()
		{
			FirstName = "Ben1",
			LastName = "Spark1",
			Date = new DateTime(2023, 1, 1),
			Email = "ben1@gmail.coom",
			BookingId = 11,
			StudyRoomId = 1
		};


		studyRoomBooking_Two = new StudyRoomBooking()
		{
			FirstName = "Ben2",
			LastName = "Spark2",
			Date = new DateTime(2023, 2, 2),
			Email = "ben2@gmail.coom",
			BookingId = 22,
			StudyRoomId = 2
		};
	}

	[SetUp]
	public void Setup()
	{
		options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase(databaseName: "temp_Bongo").Options;
	}

	[Test]
	[Order(1)]
	public void SaveBooking_BookingOne_CheckTheValuesFromDatabase()
	{
		// Arrange

		// Act
		using (var context = new ApplicationDbContext(options))
		{

			var repository = new StudyRoomBookingRepository(context);
			repository.Book(studyRoomBooking_One);
		}

		using (var context = new ApplicationDbContext(options))
		{
			var repository = new StudyRoomBookingRepository(context);
			repository.Book(studyRoomBooking_Two);
		}

		// Assert
		using (var context = new ApplicationDbContext(options))
		{
			var bookingFromDb = context.StudyRoomBookings.FirstOrDefault(u => u.BookingId == studyRoomBooking_One.BookingId);
			Assert.AreEqual(studyRoomBooking_One.BookingId, bookingFromDb.BookingId);
			Assert.AreEqual(studyRoomBooking_One.FirstName, bookingFromDb.FirstName);
			Assert.AreEqual(studyRoomBooking_One.Date, bookingFromDb.Date);
			Assert.AreEqual(studyRoomBooking_One.Email, bookingFromDb.Email);
			Assert.AreEqual(studyRoomBooking_One.StudyRoomId, bookingFromDb.StudyRoomId);
		}
	}

	[Test]
	[Order(2)]
	public void GetAllBooking_BookingOneAndTwo_CheckGotBothBookingFromDatabase()
	{
		// Arrange
		var expectedResult = new List<StudyRoomBooking> { studyRoomBooking_One, studyRoomBooking_Two };

		using (var context = new ApplicationDbContext(options))
		{
			context.Database.EnsureDeleted();
			var repository = new StudyRoomBookingRepository(context);
			repository.Book(studyRoomBooking_One);
			repository.Book(studyRoomBooking_Two);
		}

		// Act
		List<StudyRoomBooking> actualResult;
		using (var context = new ApplicationDbContext(options))
		{
			var repository = new StudyRoomBookingRepository(context);
			actualResult = repository.GetAll(null).ToList();
		}

		// Assert
		CollectionAssert.AreEqual(expectedResult, actualResult, new BookingComparer());
	}

	private class BookingComparer : IComparer
	{
		public int Compare(object? x, object? y)
		{
			var bookingA = (StudyRoomBooking) x;
			var bookingB = (StudyRoomBooking) y;

			if (bookingA.BookingId != bookingB.BookingId)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}
	}

}
