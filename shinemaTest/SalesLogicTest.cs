namespace shinemaTest;

[TestClass]
public class SalesLogicTest
{
    [TestMethod]

    public void TestGetPriceForSeat()
    {

        List<string> seats = new List<string> { "F3", "E6", "G7",
                                                "A9", "E7", "J8",
                                                "I5", "M11", "J16"};

        List<int> hallNumbers = new List<int> { 1, 1, 1,
                                                2, 2, 2,
                                                3, 3, 3 };

        List<int> expectedRank = new List<int> { 3, 2, 1,
                                                 3, 2, 1,
                                                 3, 2, 1 };

        for (int i = 0; i < seats.Count; i++)
        {
            Assert.AreEqual(expectedRank[i], SalesLogic.GetSeatRank(seats[i], hallNumbers[i]));
        }
    }

    [TestMethod]
    public void TestGetReservationsListBasedOnDate()
    {
        DateTime date = DateTime.Parse("01-01-2010");
        ShowingsLogic s = new ShowingsLogic();
        s.AddNewShowing(1, 1, 1, date, 1);

        List<string> seatList = new List<string> { "Test Seat" };
        ReservationLogic r = new ReservationLogic();

        Dictionary<int, int> snacks = new Dictionary<int, int> { };
        Dictionary<int, int> drinks = new Dictionary<int, int> { };
        r.UpdateReservation(new ReservationModel(1, 1, 1, seatList, 10.0, "uniquecode", snacks, drinks));




        DateTime testStartDate = DateTime.Parse("01-01-2009");
        DateTime testEndDate = DateTime.Parse("01-01-2011");


        ReservationModel actualReservationModel = SalesLogic.GetReservationsListBasedOnDate(testStartDate, testEndDate)[0];

        ReservationModel expectedReservationModel = new ReservationModel(1, 1, 1, seatList, 10.0, "uniquecode", snacks, drinks);


        //assert that both objects have the same values
        Assert.IsTrue(actualReservationModel.Id == expectedReservationModel.Id &&
                      actualReservationModel.Showing_ID == expectedReservationModel.Showing_ID &&
                      actualReservationModel.Account_ID == expectedReservationModel.Account_ID &&
                      actualReservationModel.Seats[0] == expectedReservationModel.Seats[0] &&
                      actualReservationModel.Price == expectedReservationModel.Price &&
                      actualReservationModel.Unique_code == expectedReservationModel.Unique_code);


        //test case that is not within time span
        DateTime testStartDate2 = DateTime.Parse("01-01-2000");
        DateTime testEndDate2 = DateTime.Parse("01-01-2001");

        List<ReservationModel> actualReservationModel2 = SalesLogic.GetReservationsListBasedOnDate(testStartDate2, testEndDate2);

        //assert if list is empty
        Assert.IsFalse(actualReservationModel2.Any());

    }


}