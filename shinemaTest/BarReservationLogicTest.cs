namespace shinemaTest;

[TestClass]
public class BarReservationLogicTest
{
    [TestMethod]
    public void TestBarReservationAvailabilty()
    {
        BarReservationLogic barReservationLogic = new BarReservationLogic();
        //First Test
        DateTime newReservationDateTime = DateTime.Parse("20-5-2024 15:01");
        DateTime ReservationDateTime1 = DateTime.Parse("20-5-2024 12:00");
        DateTime ReservationDateTime2 = DateTime.Parse("20-5-2024 12:00");
        List<BarReservationModel> reservationList = new List<BarReservationModel>{ new BarReservationModel(1, "A", ReservationDateTime1, 10),
                                                                                   new BarReservationModel(2, "B", ReservationDateTime2, 20)
        };
        int openSeatsTest1 = barReservationLogic.CheckBarAvailability(newReservationDateTime, reservationList);

        Assert.AreEqual(40, openSeatsTest1);

        //Second Test
        DateTime newReservationDateTimeTest2 = DateTime.Parse("20-5-2024 15:00");
        DateTime ReservationDateTime1Test2 = DateTime.Parse("20-5-2024 12:01");
        DateTime ReservationDateTime2Test2 = DateTime.Parse("20-5-2024 12:01");
        List<BarReservationModel> reservationListTest2 = new List<BarReservationModel>{ new BarReservationModel(1, "A", ReservationDateTime1Test2, 10),
                                                                                        new BarReservationModel(2, "B", ReservationDateTime2Test2, 20)
        };
        int openSeatsTest2 = barReservationLogic.CheckBarAvailability(newReservationDateTimeTest2, reservationListTest2);

        Assert.AreEqual(10, openSeatsTest2);

        //Third Test
        DateTime newReservationDateTimeTest3 = DateTime.Parse("20-5-2024 12:00");
        DateTime ReservationDateTime1Test3 = DateTime.Parse("20-5-2024 12:00");
        DateTime ReservationDateTime2Test3 = DateTime.Parse("20-5-2024 15:01");
        List<BarReservationModel> reservationListTest3 = new List<BarReservationModel>{ new BarReservationModel(1, "A", ReservationDateTime1Test3, 10),
                                                                                        new BarReservationModel(2, "B", ReservationDateTime2Test3, 20)
        };
        int openSeatsTest3 = barReservationLogic.CheckBarAvailability(newReservationDateTimeTest3, reservationListTest3);

        Assert.AreEqual(30, openSeatsTest3);

    }



    [TestMethod]

    public void TestDeletebarReservation()
    {
        // make instance of ReservationsLogic
        BarReservationLogic barReservationsLogic = new BarReservationLogic();

        // Create new reservations
        BarReservationModel barReservation = new BarReservationModel(999, "123456", DateTime.Now, 4);
        BarReservationModel barReservation2 = new BarReservationModel(1000, "654321", DateTime.Now, 4);
        BarReservationModel barReservation3 = new BarReservationModel(1001, "369852", DateTime.Now, 4);

        // Add the reservations to the list
        barReservationsLogic.AddOneItem(barReservation);
        barReservationsLogic.AddOneItem(barReservation2);
        barReservationsLogic.AddOneItem(barReservation3);

        // Check if the reservations have been added
        Assert.IsTrue(barReservationsLogic.FindBarReservationUsingCode("123456") == barReservation);
        Assert.IsTrue(barReservationsLogic.FindBarReservationUsingCode("654321") == barReservation2);
        Assert.IsTrue(barReservationsLogic.FindBarReservationUsingCode("369852") == barReservation3);

        // delete the reservation
        List<string> reservationCodes = new List<string> { barReservation.Unique_code, barReservation2.Unique_code, barReservation3.Unique_code };
        barReservationsLogic.RemoveBarSeatReservation(reservationCodes);

        // Check if the reservation is deleted
        Assert.IsFalse(barReservationsLogic.FindBarReservationUsingCode("123456") == barReservation);
        Assert.IsFalse(barReservationsLogic.FindBarReservationUsingCode("654321") == barReservation2);
        Assert.IsFalse(barReservationsLogic.FindBarReservationUsingCode("369852") == barReservation3);
    }


}