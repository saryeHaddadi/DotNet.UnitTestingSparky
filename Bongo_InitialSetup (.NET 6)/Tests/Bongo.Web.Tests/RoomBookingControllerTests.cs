using Bongo.Core.Services.IServices;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Bongo.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Bongo.Web.Tests;

[TestFixture]
public class RoomBookingControllerTests
{
	private Mock<IStudyRoomBookingService> _studyRoomBookingService;
	private RoomBookingController _bookingController;

	[SetUp]
	public void Setup()
	{
		_studyRoomBookingService = new Mock<IStudyRoomBookingService>();
		_bookingController = new RoomBookingController(_studyRoomBookingService.Object);
	}

	[Test]
	public void IndexPage_CallRequest_VerifyGetallInvoked()
	{
		_bookingController.Index();
		_studyRoomBookingService.Verify(x => x.GetAllBooking(), Times.Once);
	}

	[Test]
	public void BookRoomCheck_ModelStateInvalid_ReturnView()
	{
		_bookingController.ModelState.AddModelError("test", "test");
		var actualResult = _bookingController.Book(new StudyRoomBooking());
		var viewResult = actualResult as ViewResult;

		Assert.AreEqual("Book", viewResult.ViewName);
	}

	[Test]
	public void BookRoomCheck_NotSuccessful_NoRoomCode()
	{
		_studyRoomBookingService.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
			.Returns(new StudyRoomBookingResult()
			{
				Code = StudyRoomBookingCode.NoRoomAvailable
			});
		var actualResult = _bookingController.Book(new StudyRoomBooking());
		Assert.IsInstanceOf<ViewResult>(actualResult);

		var viewResult = actualResult as ViewResult;
		Assert.AreEqual("No Study Room available for selected date",
			viewResult.ViewData["Error"]);
	}

	[Test]
	public void BookRoomCheck_Successful_Redirect()
	{
		// Arrange
		_studyRoomBookingService.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
			.Returns( (StudyRoomBooking booking) => new StudyRoomBookingResult()
			{
				Code = StudyRoomBookingCode.Success,
				FirstName = booking.FirstName,
				LastName = booking.LastName,
				Date = booking.Date,
				Email = booking.Email
			});

		// Act
		var actualResult = _bookingController.Book(new StudyRoomBooking()
		{
			FirstName = "Hello",
			LastName = "DotNetMastery",
			Date = DateTime.Now,
			Email = "hello@dotnet.com",
			StudyRoomId = 1
		});

		// Assert
		Assert.IsInstanceOf<RedirectToActionResult>(actualResult);

		var actionResult = actualResult as RedirectToActionResult;
		Assert.AreEqual("Hello", actionResult.RouteValues["FirstName"]);
		Assert.AreEqual(StudyRoomBookingCode.Success, actionResult.RouteValues["Code"]);
	}
}
