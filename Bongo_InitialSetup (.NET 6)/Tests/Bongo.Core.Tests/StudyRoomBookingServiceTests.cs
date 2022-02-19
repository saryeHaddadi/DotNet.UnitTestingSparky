using Bongo.Core.Services;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;
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

	[TestCase]
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





}
