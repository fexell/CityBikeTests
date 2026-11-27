using CityBike.Core;
using Moq;
using Xunit;
 
namespace CityBike.Tests;
 
public class BookingServiceTests
{
  private readonly Mock<IBikeRepository> _bikes = new();
  private readonly Mock<IUnlockClient> _unlockClient = new();
  private readonly BookingService _service;

  public BookingServiceTests()
  {
    _service = new BookingService(_bikes.Object, _unlockClient.Object);
  }

  [Fact]
  public void Book_ValidBike_UnlocksBikeExactlyOnce()
  {
    _bikes.Setup(b => b.GetById(1)).Returns(new Bike(1, "Available", 82));
    _unlockClient.Setup(u => u.Unlock(1)).Returns(true);

    BookingResult result = _service.Book(1);

    Assert.Equal(BookingResult.Unlocked, result);
    _unlockClient.Verify(u => u.Unlock(1), Times.Once());
  }

  [Fact]
  public  void Book_UnknownBike_ReturnsBikeNotFoundAndDoesNotUnlock()
  {
    _bikes.Setup(b => b.GetById(1)).Returns((Bike?)null);

    BookingResult result = _service.Book(1);

    Assert.Equal(BookingResult.BikeNotFound, result);
    _unlockClient.Verify(u => u.Unlock(It.IsAny<int>()), Times.Never());
  }

  [Fact]
  public void Book_BookedBike_ReturnsBikeNotAvailableAndDoesNotUnlock()
  {
    _bikes.Setup(b => b.GetById(1)).Returns(new Bike(1, "Booked", 82));

    BookingResult result = _service.Book(1);

    Assert.Equal(BookingResult.BikeNotAvailable, result);
    _unlockClient.Verify(u => u.Unlock(It.IsAny<int>()), Times.Never());
  }

  [Fact]
  public void Book_LowBattery_ReturnsBatteryTooLowAndDoesNotUnlock()
  {
    _bikes.Setup(b => b.GetById(3)).Returns(new Bike(3, "Available", 9));

    BookingResult result = _service.Book(3);

    Assert.Equal(BookingResult.BatteryTooLow, result);
    _unlockClient.Verify(u => u.Unlock(It.IsAny<int>()), Times.Never());
  }

  [Fact]
  public void Book_BatteryExactlyAtLimit_UnlocksBike()
  {
    _bikes.Setup(b => b.GetById(4)).Returns(new Bike(4, "Available", 10));
    _unlockClient.Setup(u => u.Unlock(4)).Returns(true);

    BookingResult result = _service.Book(4);

    Assert.Equal(BookingResult.Unlocked, result);
    _unlockClient.Verify(u => u.Unlock(4), Times.Once());
  }

  [Fact]
  public void Book_UnlockFails_ReturnsUnlockFailed()
  {
    _bikes.Setup(b => b.GetById(5)).Returns(new Bike(5, "Available", 50));
    _unlockClient.Setup(u => u.Unlock(5)).Returns(false);

    BookingResult result = _service.Book(5);

    Assert.Equal(BookingResult.UnlockFailed, result);
    _unlockClient.Verify(u => u.Unlock(5), Times.Once());
  }
}
