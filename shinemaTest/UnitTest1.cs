namespace shinemaTest;

using System.Text.Json;

[TestClass]
public class UnitTest1
{
    [DataTestMethod]
    [DataRow("Hogeschool123", "30b307b395e4570a85e5851a5b3846a18b8a61e39012ad3b809224a7a76a3c5e")]
    [DataRow("f", "252f10c83610ebca1a059c0bae8255eba2f95be4d1d7bcfa89d7248a82d9f111")]
    [DataRow("pav7q8q644g", "533f86c690e2f7d115ca6eb54d6a82948262539cef80fd9a153c3c5c6ad6c223")]
    [DataRow("kEVIn123", "f6a1387fb3f60e26f142b96bc04a6da6945118aa6736185e7fd26b82ccd02e1e")]

    public void TestHashedPasswords(string password, string expected_string)
    {
        string hashed_password = AccountsLogic.GetHashString(password);
        Assert.AreEqual(hashed_password, expected_string);
    }

    // [TestMethod]
    // public void TestCheckFullName()
    // {
    //     // Arrange
    //     List<string> correct_names = new List<string> { "Cristiano", "Hogeschool" };
    //     List<string> incorrect_names = new List<string> { "Cr1stian0", "Hoge-school" };


    //     // Act
    //     foreach (string name in correct_names)
    //     {
    //         // Assert
    //         Assert.IsTrue(AccountsLogic.CheckFullName(name));

    //     }

    //     foreach (string name in incorrect_names)
    //     {
    //         // Assert
    //         Assert.IsFalse(AccountsLogic.CheckFullName(name));
    //     }
    // }

    // [TestMethod]
    // public void TestCheckPassword()
    // {
    //     List<string> correct_passwords = new List<string> { "Cr1stiano", "Hog3sch00l" };
    //     List<string> incorrect_passwords = new List<string> { "cr1stian0", "Hoge-school" };

    //     // Act
    //     foreach (string password in correct_passwords)
    //     {
    //         // Assert
    //         Assert.AreEqual(true, AccountsLogic.CheckPassword(password));

    //     }

    //     foreach (string password in incorrect_passwords)
    //     {
    //         // Assert
    //         Assert.AreEqual(false, AccountsLogic.CheckPassword(password));
    //     }
    // }

    // [TestMethod]
    // public void TestEmail()
    // {
    //     // Instantiate AccountsLogic
    //     AccountsLogic accountsLogic = new AccountsLogic();

    //     // Add a new account
    //     accountsLogic.AddNewAccount(9999, "test@email.com", "Hogeschool1", "TestPersoon", true, true, true);


    //     // Check if email exists
    //     bool test = accountsLogic.CheckEmail("test@email.com");

    //     // Assert the test result
    //     Assert.IsFalse(test);
    // }


    // Unit Test Cinema Information

    [TestMethod]

    public void TestDeleteMovie()
    {
        // Create a new movie
        MovieModel movie = new MovieModel(1, "Test Movie", 120, "16", "Test Description", 1, new List<string> { "Action" }, "2021-01-01");

        // Add the movie to the list
        MoviesLogic.AddMovie(movie.ID, movie.Title, movie.Length, movie.Age, movie.Description, movie.ShowingID, movie.Genre, movie.Release_Date);

        // Delete the movie
        bool boolCheck = MoviesLogic.DeleteMovie(movie.ID);

        // Check if the movie is deleted
        Assert.IsTrue(boolCheck);
        Assert.IsFalse(MoviesLogic.GetAllMovies().Any(m => m.ID == movie.ID));

        // Check if a movie that doesnt exist is deleted
        boolCheck = MoviesLogic.DeleteMovie(9999);
        Assert.IsFalse(boolCheck);
    }

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
        showingsLogic.AddNewShowing(showing1.ID, showing1.MovieID, showing1.RoomID, showing1.Datetime, true);
        showingsLogic.AddNewShowing(showing2.ID, showing2.MovieID, showing2.RoomID, showing2.Datetime, true);
        showingsLogic.AddNewShowing(showing3.ID, showing3.MovieID, showing3.RoomID, showing3.Datetime, true);
        showingsLogic.AddNewShowing(showing4.ID, showing4.MovieID, showing4.RoomID, showing4.Datetime, true);
        showingsLogic.AddNewShowing(showing5.ID, showing5.MovieID, showing5.RoomID, showing5.Datetime, true);

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

    public void TestDeleteReservation()
    {
        // make instance of ReservationsLogic
        ReservationLogic reservationsLogic = new ReservationLogic();

        // Create new reservations
        ReservationModel reservation = new ReservationModel(999, 500, 300, new List<string> { "A1", "A2" }, 20, "123456", null);
        ReservationModel reservation2 = new ReservationModel(1000, 501, 300, new List<string> { "A1", "A2" }, 20, "123456", null);
        ReservationModel reservation3 = new ReservationModel(1001, 502, 300, new List<string> { "A1", "A2" }, 20, "123456", null);

        // Add the reservations to the list
        reservationsLogic.AddNewReservation(reservation.Id, reservation.Showing_ID, reservation.Account_ID, reservation.Seats, reservation.Price, reservation.Unique_code, null);
        reservationsLogic.AddNewReservation(reservation2.Id, reservation2.Showing_ID, reservation2.Account_ID, reservation2.Seats, reservation2.Price, reservation2.Unique_code, null);
        reservationsLogic.AddNewReservation(reservation3.Id, reservation3.Showing_ID, reservation3.Account_ID, reservation3.Seats, reservation3.Price, reservation3.Unique_code, null);


        // Check if the reservations have been added
        Assert.IsTrue(reservationsLogic.GetAllReservations().Any(r => r.Id == reservation.Id));
        Assert.IsTrue(reservationsLogic.GetAllReservations().Any(r => r.Id == reservation2.Id));
        Assert.IsTrue(reservationsLogic.GetAllReservations().Any(r => r.Id == reservation3.Id));

        // delete the reservation
        List<int> showingIds = new List<int> { reservation.Showing_ID, reservation2.Showing_ID, reservation3.Showing_ID };
        reservationsLogic.DeleteReservation(showingIds);

        // Check if the reservation is deleted
        Assert.IsFalse(reservationsLogic.GetAllReservations().Any(r => r.Id == reservation.Id));
        Assert.IsFalse(reservationsLogic.GetAllReservations().Any(r => r.Id == reservation2.Id));
        Assert.IsFalse(reservationsLogic.GetAllReservations().Any(r => r.Id == reservation3.Id));

    }

    [TestMethod]
    public void TestCinemaInfoTimeValidity()
    {
        // return value description

        // returns -4 if ":" is not in time string
        // returns -3 if hours is not an integer
        // returns -2 if minutes is not an integer
        // returns -1 if hours is out of range (higher than 24 or lower than 0)
        // returns 0 if minutes is out of range (higher than 59 or lower than 0)
        // returns 1 if time string is correct


        // inputs to test that match expected output 
        string[] testInputs =  { "23:59", "23:60", "24:00",
                                "00:00", "00:01", "-1:00",
                                "00:-1", "2359", "2360",
                                "-159", "aa:59", "11:aa",
                                "aa:aa", "aaaa", "aa:-59" };


        // expected output
        int[] testOuputs = { 1, 0, -1,
                            1, 1, -1,
                            0, -4, -4,
                            -4, -3, -2,
                            -3, -4, -3 };

        int functionOutput;
        for (int i = 0; i < testInputs.Length; i++)
        {
            Console.WriteLine(testInputs[i]);
            functionOutput = CinemaInfoLogic.CheckTimeValidity(testInputs[i]);
            Assert.AreEqual(testOuputs[i], functionOutput);
        }
        // int j = 4;
        // functionOutput = CinemaInfoLogic.CheckTimeValidity(testInputs[j]);
        //     Assert.AreEqual(functionOutput, testOuputs[j]);
    }

    [TestMethod]

    public void TestAddFood()
    {
        //Test if it returns true with correct input
        Assert.IsTrue(FoodLogic.AddFood("Snickers", 250, 2.50));

        // Test if it returns false with incorrect inputs
        Assert.IsFalse(FoodLogic.AddFood(null, 250, 2.50));
        Assert.IsFalse(FoodLogic.AddFood("Snickers", default, 2.50));
        Assert.IsFalse(FoodLogic.AddFood("Snickers", 250, default));
    }

    [TestMethod]

    public void TestAddFoodData()
    {
        string title1 = "Snickers";
        int amount1 = 200;
        double price1 = 1.20;
        //Add correctly filled in data to json to later check if it is written to json correctly
        FoodLogic.AddFood(title1, amount1, price1);

        //Check if food is added to the json

        //Check title
        Assert.IsTrue(FoodLogic.GetAllFood().Any(i => i.Title == title1));

        //Check amount
        Assert.IsTrue(FoodLogic.GetAllFood().Any(i => i.Amount == amount1));

        //Check price
        Assert.IsTrue(FoodLogic.GetAllFood().Any(i => i.Price == price1));

    }

    [TestMethod]

    public void TestCheckIfMovieExist()
    {
        string movieDiscription = "The Godfather \"Don\" Vito Corleone is the head of the Corleone mafia family in New York. He is at the event of his daughter's wedding. Michael, Vito's youngest son and a decorated WWII Marine is also present at the wedding. Michael seems to be uninterested in being a part of the family business. Vito is a powerful man, and is kind to all those who give him respect but is ruthless against those who do not. But when a powerful and treacherous rival wants to sell drugs and needs the Don's influence for the same, Vito refuses to do it. What follows is a clash between Vito's fading old values and the new ways which may cause Michael to do the thing he was most reluctant in doing and wage a mob war against all the other mafia families which could tear the Corleone family apart. ";
        MovieModel newMovie = new MovieModel(1, "The Godfather", 185, "14", movieDiscription, 1, new List<string> { "Crime", "Drama" }, "1972");
        MoviesLogic.UpdateMovieList(newMovie);
        Assert.AreEqual(MoviesLogic.CheckIfMovieExist(newMovie.ID), newMovie);
    }

    [TestMethod]

    public void TestCheckIfMovieNotExist()
    {
        string movieDiscription = "The Godfather \"Don\" Vito Corleone is the head of the Corleone mafia family in New York. He is at the event of his daughter's wedding. Michael, Vito's youngest son and a decorated WWII Marine is also present at the wedding. Michael seems to be uninterested in being a part of the family business. Vito is a powerful man, and is kind to all those who give him respect but is ruthless against those who do not. But when a powerful and treacherous rival wants to sell drugs and needs the Don's influence for the same, Vito refuses to do it. What follows is a clash between Vito's fading old values and the new ways which may cause Michael to do the thing he was most reluctant in doing and wage a mob war against all the other mafia families which could tear the Corleone family apart. ";
        MovieModel newMovie = new MovieModel(1, "The Godfather", 185, "14", movieDiscription, 1, new List<string> { "Crime", "Drama" }, "1972");
        MoviesLogic.UpdateMovieList(newMovie);
        Assert.AreEqual(MoviesLogic.CheckIfMovieExist(-1123), null);
    }


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


    // sales unittest
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

        for(int i = 0; i < seats.Count; i++)
        {
            Assert.AreEqual(expectedRank[i], SalesLogic.GetSeatRank(seats[i], hallNumbers[i]));
        }
    }

    [TestMethod]
    public void TestGetReservationsListBasedOnDate()
    {
        DateTime date = DateTime.Parse("01-01-2010");
        ShowingsLogic s = new ShowingsLogic();
        s.AddNewShowing(1, 1, 1, date, true);

        List<string> seatList = new List<string> { "Test Seat" };
        ReservationLogic r = new ReservationLogic();
        Dictionary<int, int> snacks = new Dictionary<int, int> {};
        r.UpdateReservation(new ReservationModel(1, 1, 1, seatList, 10.0, "uniquecode", snacks));


        DateTime testStartDate = DateTime.Parse("01-01-2009");
        DateTime testEndDate = DateTime.Parse("01-01-2011");

        
        ReservationModel actualReservationModel = SalesLogic.GetReservationsListBasedOnDate(testStartDate, testEndDate)[0];

        ReservationModel expectedReservationModel = new ReservationModel(1, 1, 1, seatList, 10.0, "uniquecode", snacks);

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


    [TestMethod]
    public void TestUpdateMovieList()
    {
        string movieDescription = "Inception is a 2010 science fiction action film written and directed by Christopher Nolan, who also produced the film with Emma Thomas. The film stars Leonardo DiCaprio as a professional thief who steals information by infiltrating the subconscious of his targets. He is offered a chance to have his criminal history erased as payment for the implantation of another person's idea into a target's subconscious.";
        MovieModel newMovie = new MovieModel(1, "Inception", 148, "13", movieDescription, 1, new List<string> { "Action", "Sci-Fi", "Thriller" }, "2010");

        // Update the movie properties
        newMovie.Title = "The Godfather";
        newMovie.Length = 185;
        newMovie.Age = "14";
        newMovie.Description = "The Godfather \"Don\" Vito Corleone is the head of the Corleone mafia family in New York. He is at the event of his daughter's wedding. Michael, Vito's youngest son and a decorated WWII Marine is also present at the wedding. Michael seems to be uninterested in being a part of the family business. Vito is a powerful man, and is kind to all those who give him respect but is ruthless against those who do not. But when a powerful and treacherous rival wants to sell drugs and needs the Don's influence for the same, Vito refuses to do it. What follows is a clash between Vito's fading old values and the new ways which may cause Michael to do the thing he was most reluctant in doing and wage a mob war against all the other mafia families which could tear the Corleone family apart.";
        newMovie.Genre = new List<string> { "Crime", "Drama" };
        newMovie.Release_Date = "1972";

        // Update the movie list
        MoviesLogic.UpdateMovieList(newMovie);

        // Assert the updated values
        Assert.AreEqual("The Godfather", newMovie.Title);
        Assert.AreEqual(185, newMovie.Length);
        Assert.AreEqual("14", newMovie.Age);
        Assert.AreEqual("The Godfather \"Don\" Vito Corleone is the head of the Corleone mafia family in New York. He is at the event of his daughter's wedding. Michael, Vito's youngest son and a decorated WWII Marine is also present at the wedding. Michael seems to be uninterested in being a part of the family business. Vito is a powerful man, and is kind to all those who give him respect but is ruthless against those who do not. But when a powerful and treacherous rival wants to sell drugs and needs the Don's influence for the same, Vito refuses to do it. What follows is a clash between Vito's fading old values and the new ways which may cause Michael to do the thing he was most reluctant in doing and wage a mob war against all the other mafia families which could tear the Corleone family apart.", newMovie.Description);
        CollectionAssert.AreEqual(new List<string> { "Crime", "Drama" }, newMovie.Genre);
        Assert.AreEqual("1972", newMovie.Release_Date);
    }

    [TestMethod]

    public void TestBuyFood()
    {   

    }

}