namespace shinemaTest;
using System.Globalization;

[TestClass]
public class ShowingLogicTest
{
    [TestMethod]

    public void TestDeleteShowing()
    {
        // Create a new showing
        DateTime datetime = new DateTime(2025, 1, 1, 12, 0, 0);
        DateTime secondDatetime = new DateTime(2025, 1, 1, 14, 0, 0);
        DateTime thirdDatetime = new DateTime(2025, 1, 1, 16, 0, 0);
        DateTime fourthDatetime = new DateTime(2025, 1, 1, 18, 0, 0);


        ShowingModel showing1 = new ShowingModel(4, 1, 1, datetime);
        ShowingModel showing2 = new ShowingModel(5, 3, 1, datetime);
        ShowingModel showing3 = new ShowingModel(6, 3, 1, secondDatetime);
        ShowingModel showing4 = new ShowingModel(7, 3, 1, thirdDatetime);
        ShowingModel showing5 = new ShowingModel(8, 3, 1, fourthDatetime);
        ShowingsLogic showingsLogic = new ShowingsLogic();


        // Add the showing to the list
        showingsLogic.AddNewShowing(showing1.ID, showing1.RoomID, showing1.MovieID, showing1.Datetime, 1);
        showingsLogic.AddNewShowing(showing2.ID, showing1.RoomID, showing1.MovieID, showing2.Datetime, 1);
        showingsLogic.AddNewShowing(showing3.ID, showing1.RoomID, showing1.MovieID, showing3.Datetime, 1);
        showingsLogic.AddNewShowing(showing4.ID, showing1.RoomID, showing1.MovieID, showing4.Datetime, 1);
        showingsLogic.AddNewShowing(showing5.ID, showing1.RoomID, showing1.MovieID, showing5.Datetime, 1);

        // Check if the showings have been added
        Assert.IsTrue(showingsLogic.GetAllShowings().Any(s => s.ID == showing1.ID));
        Assert.IsTrue(showingsLogic.GetAllShowings().Any(s => s.ID == showing2.ID));
        Assert.IsTrue(showingsLogic.GetAllShowings().Any(s => s.ID == showing3.ID));
        Assert.IsTrue(showingsLogic.GetAllShowings().Any(s => s.ID == showing4.ID));
        Assert.IsTrue(showingsLogic.GetAllShowings().Any(s => s.ID == showing5.ID));

        // Delete the showing
        showingsLogic.DeleteShowing(showing1.ID);

        // Check if the showing is deleted
        Assert.IsFalse(showingsLogic.GetAllShowings().Any(s => s.ID == showing1.ID));

        // get all showings of movie 3
        List<int> showings = showingsLogic.GetShowingID(3);

        // delete all showings
        showingsLogic.DeleteShowing(showings);

        // check if all showings are deleted
        Assert.IsFalse(showingsLogic.GetAllShowings().Any(s => s.MovieID == 3));

    }




    [TestMethod]
    public void TestAddShowing()
    {
        ShowingsLogic showings = new ShowingsLogic();
        int future = (DateTime.Now.AddYears(100)).Year;
        MovieModel m = new MovieModel(1, "TestMovie", 60, null, null, null, null);
        MoviesLogic.UpdateMovieList(m);
        int movielength = m.Length;


        // Date in the past (yesterday)
        ShowingModel s1 = new ShowingModel(2, 1, 1, DateTime.Now.AddDays(-1));


        // Date after opening hours
        CinemaInformationModel info = CinemaInfoLogic.GetCinemaInfoObject();
        string[] split_time = info.OpeningTime.Split(':');
        DateTime opening = new DateTime(future, 1, 1, Convert.ToInt32(split_time[0]), Convert.ToInt32(split_time[1]), 00);
        split_time = info.ClosingTime.Split(':');
        DateTime closing = new DateTime(future, 1, 1, Convert.ToInt32(split_time[0]), Convert.ToInt32(split_time[1]), 00);

        ShowingModel s21 = new ShowingModel(2, 1, 1, opening.AddMinutes(-1));                   // Before opening time
        ShowingModel s22 = new ShowingModel(2, 1, 1, opening);                                  // Exactly opening time
        ShowingModel s23 = new ShowingModel(2, 1, 1, opening.AddMinutes(1));                    // After opening time

        ShowingModel s31 = new ShowingModel(2, 1, 1, closing.AddMinutes(-movielength - 1));     // Before closing time
        ShowingModel s32 = new ShowingModel(2, 1, 1, closing.AddMinutes(-movielength));         // Exactly closing time
        ShowingModel s33 = new ShowingModel(2, 1, 1, closing.AddMinutes(-movielength + 1));     // After closing time


        // Date when another showing is already planned
        ShowingModel existing_showing = new ShowingModel(1, 1, 1, new DateTime(future, 1, 1, 12, 00, 00));
        showings.UpdateShowings(existing_showing);

        ShowingModel s41 = new ShowingModel(2, 1, 1, existing_showing.Datetime.AddMinutes(-movielength - 30 - 1));  // Before another showing (31 min)
        ShowingModel s42 = new ShowingModel(2, 1, 1, existing_showing.Datetime.AddMinutes(-movielength - 30));      // Before another showing (30 min)
        ShowingModel s43 = new ShowingModel(2, 1, 1, existing_showing.Datetime.AddMinutes(-movielength - 30 + 1));  // Before another showing (29 min)
        ShowingModel s44 = new ShowingModel(2, 2, 1, existing_showing.Datetime.AddMinutes(-movielength - 30 + 1));  // Before another showing (29 min) in a different hall

        ShowingModel s51 = new ShowingModel(2, 1, 1, existing_showing.Datetime.AddMinutes(movielength + 30 - 1));   // After another showing (29 min)
        ShowingModel s52 = new ShowingModel(2, 1, 1, existing_showing.Datetime.AddMinutes(movielength + 30));       // After another showing (30 min)
        ShowingModel s53 = new ShowingModel(2, 1, 1, existing_showing.Datetime.AddMinutes(movielength + 30 + 1));   // After another showing (31 min)
        ShowingModel s54 = new ShowingModel(2, 2, 1, existing_showing.Datetime.AddMinutes(-movielength - 30 - 1));  // After another showing (29 min) in a different hall


        // Check all showing-datetimes

        Assert.AreEqual(1, showings.ValidateDate(existing_showing));        // Check normal datetime

        Assert.AreEqual(2, showings.ValidateDate(s1));      // Check date in the past

        Assert.AreEqual(3, showings.ValidateDate(s21));     // Check before opening time
        Assert.AreEqual(1, showings.ValidateDate(s22));     // Check exactly opening time
        Assert.AreEqual(1, showings.ValidateDate(s23));     // Check after opening time

        Assert.AreEqual(1, showings.ValidateDate(s31));     // Check before closing time
        Assert.AreEqual(1, showings.ValidateDate(s32));     // Check exactly closing time
        Assert.AreEqual(3, showings.ValidateDate(s33));     // Check after closing time

        Assert.AreEqual(1, showings.ValidateDate(s41));     // Check before another showing + 31 min
        Assert.AreEqual(1, showings.ValidateDate(s42));     // Check before another showing + 30 min
        Assert.AreEqual(4, showings.ValidateDate(s43));     // Check before another showing + 29 min
        Assert.AreEqual(1, showings.ValidateDate(s44));     // Check before another showing + 29 min in another hall

        Assert.AreEqual(4, showings.ValidateDate(s51));     // Check after another showing + 29 min
        Assert.AreEqual(1, showings.ValidateDate(s52));     // Check after another showing + 30 min
        Assert.AreEqual(1, showings.ValidateDate(s53));     // Check after another showing + 31 min
        Assert.AreEqual(1, showings.ValidateDate(s54));     // Check after another showing + 29 min in another hall

    }


    [DataTestMethod]
    [DataRow("12-12-2012", "12:12", "12-12-2012 12:12:00")]           // Regular date
    [DataRow("01-01-0001", "00:00", "01-01-0001 00:00:00")]           // All numbers min
    [DataRow("31-12-9999", "23:59", "31-12-9999 23:59:00")]           // All numbers max

    [DataRow("2-1-01-2-00-12", "23:59", "01-01-0001 00:00:00")]       // Too many -
    [DataRow("01-01-2000", "2:34:9:234", "01-01-0001 00:00:00")]      // Too many :
    [DataRow("*n67-a$e4-A12-34", "23:59", "01-01-0001 00:00:00")]     // Monkey typing on keyboard
    [DataRow("01-01-2000", "as#23:28%v", "01-01-0001 00:00:00")]      // Monkey typing on keyboard again
    [DataRow("-3-4-2025", "-23:59", "01-01-0001 00:00:00")]           // Negative date or time
    // Note: 01-01-0001 00:00:00 is the ToString() value of an empty DateTime, so every wrong DateTime shoud give that date

    public void TestSetToDateTime(string date, string time, string expected_datetime)
    {
        CultureInfo.CurrentCulture = new CultureInfo("nl-NL");
        DateTime new_datetime = ShowingsLogic.SetToDatetime(date, time);
        Assert.AreEqual(expected_datetime, new_datetime.ToString("dd-MM-yyyy HH:mm:ss"));
    }

}