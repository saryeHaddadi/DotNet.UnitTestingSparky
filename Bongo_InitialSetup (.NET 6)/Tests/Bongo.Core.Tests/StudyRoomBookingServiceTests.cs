﻿using Bongo.Core.Services;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Moq;
using NUnit.Framework;

namespace Bongo.Core.Tests;

[TestFixture]
public class StudyRoomBookingServiceTests
{
	private StudyRoomBooking _request;
	private List<StudyRoom> _availableStudyRoom;
	private Mock<IStudyRoomBookingRepository> _studyRoomBookingRepoMock;
	private Mock<IStudyRoomRepository> _studyRoomRepoMock;
	private StudyRoomBookingService _bookingService;

	[SetUp]
	public void Setup()
	{
		_request = new StudyRoomBooking
		{
			FirstName = "Ben",
			LastName = "Spark",
			Email = "ben@grmail.coom",
			Date = new DateTime(2022, 1, 1)
		};

		_availableStudyRoom = new List<StudyRoom>()
		{
			new StudyRoom { Id = 10, RoomName = "Michigan", RoomNumber = "A202"}
		};

		_studyRoomBookingRepoMock = new Mock<IStudyRoomBookingRepository>();
		_studyRoomRepoMock = new Mock<IStudyRoomRepository>();
		_studyRoomRepoMock.Setup(x => x.GetAll()).Returns(_availableStudyRoom);

		_bookingService = new StudyRoomBookingService(
			_studyRoomBookingRepoMock.Object,
			_studyRoomRepoMock.Object);
	}

	[TestCase]
	public void GetAllBooking_InvokeMethod_CheckIfRepoIsCalled()
	{
		_bookingService.GetAllBooking();
		_studyRoomBookingRepoMock.Verify(x => x.GetAll(null), Times.Once);
	}

	[TestCase]
	public void BookingException_NullRequest_ThrowsException()
	{
		var exception = Assert.Throws<ArgumentNullException>(() => _bookingService.BookStudyRoom(null));
		//Assert.AreEqual("Value cannot be null. (Parameter 'request')", exception.Message);
		Assert.AreEqual("request", exception.ParamName);
	}

	[Test]
	public void StudyRoomBooking_SaveBookingWithAvailableRoom_ReturnsResultWithAllValues()
	{
		// Arrange
		StudyRoomBooking savedStudyRoomBooking = null;
		_studyRoomBookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
			.Callback<StudyRoomBooking>(booking =>
			{
				savedStudyRoomBooking = booking;
			});

		// Act
		_bookingService.BookStudyRoom(_request);

		// Assert
		_studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Once);
		Assert.NotNull(savedStudyRoomBooking);
		Assert.AreEqual(_request.FirstName, savedStudyRoomBooking.FirstName);
		Assert.AreEqual(_request.LastName, savedStudyRoomBooking.LastName);
		Assert.AreEqual(_request.Email, savedStudyRoomBooking.Email);
		Assert.AreEqual(_request.Date, savedStudyRoomBooking.Date);
		Assert.AreEqual(_availableStudyRoom.First().Id, savedStudyRoomBooking.StudyRoomId);
	}


	[TestCase]
	public void StudyRoomBooking_InputRequest_ValuesMatchInResult()
	{
		// Arrange

		// Act
		var result = _bookingService.BookStudyRoom(_request);

		// Assert
		Assert.NotNull(result);
		Assert.AreEqual(_request.FirstName, result.FirstName);
		Assert.AreEqual(_request.LastName, result.LastName);
		Assert.AreEqual(_request.Email, result.Email);
		Assert.AreEqual(_request.Date, result.Date);
	}

	[TestCase(true, ExpectedResult = StudyRoomBookingCode.Success)]
	[TestCase(false, ExpectedResult = StudyRoomBookingCode.NoRoomAvailable)]
	public StudyRoomBookingCode ResultCodeSuccess_RoomAvailability_ReturnsSuccessResultCode(bool roomAvailability)
	{
		// Arrange
		if (!roomAvailability)
		{
			_availableStudyRoom.Clear();
		}
		// Act
		return _bookingService.BookStudyRoom(_request).Code;
	}

	[TestCase(0, false)]
	[TestCase(55, true)]
	public void StudyRoomBooking_BookRoomWithAvailability_ReturnsBookingId(int expectedBookingId, bool roomAvailability)
	{
		// Arrange
		if (!roomAvailability)
		{
			_availableStudyRoom.Clear();
		}
		_studyRoomBookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
			.Callback<StudyRoomBooking>(booking =>
			{
				booking.BookingId = 55;
			});

		// Act
		var actualResult = _bookingService.BookStudyRoom(_request);

		// Assert
		Assert.AreEqual(expectedBookingId, actualResult.BookingId);
	}

	[Test]
	public void StudyRoomBooking_SaveBookingWithoutAvailableRoom_BookMethodNotInvoked(int expectedBookingId, bool roomAvailability)
	{
		// Arrange
		_availableStudyRoom.Clear();
		// Act
		var actualResult = _bookingService.BookStudyRoom(_request);

		// Assert
		_studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Never);
	}

}
