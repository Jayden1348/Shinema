namespace shinemaTest;

using System.Text.Json;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestBlurredPassword_ReturnsCorrectLength()
    {
        // Arrange
        string password = "password123";
        AccountModel user = new AccountModel(1, "test@example.com", password, "John Doe");

        // Act
        string blurredPassword = AccountsLogic.BlurredPassword(user);

        // Assert
        Assert.AreEqual(password.Length, blurredPassword.Length);
    }

    [TestMethod]
    public void TestBlurredPassword_ReturnsAllStars()
    {
        // Arrange
        string password = "password123";
        AccountModel user = new AccountModel(2, "test2@example.com", password, "John Doe");

        // Act
        string blurredPassword = AccountsLogic.BlurredPassword(user);

        // Assert
        Assert.AreEqual(new string('*', password.Length), blurredPassword);
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
    public void TestCinemaInfoSave()
    {
        // Create Cinema Information Model
        CinemaInformationModel cinema = new CinemaInformationModel(
            "Amsterdam",
            "Johan Cruijff Boulevard 600, 1101 DS",
            "06:00",
            "23:00",
            "06987654321",
            "Cinema@newEmail.com");


        //create expected json
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(cinema, options);
        string testPath = @"./TestCinemaInformation.json";
        File.WriteAllText(testPath, json);

        //Use the function Cinema to write to json
        CinemaInformationAccess.WriteInfoCinema(cinema);

        //Read json file 
        string json1 = File.ReadAllText(@"./TestCinemaInformation.json");
        string json2 = File.ReadAllText(@"././DataSources/cinemainformation.json");

        CinemaInformationModel cinemaTest = JsonSerializer.Deserialize<CinemaInformationModel>(json1);
        CinemaInformationModel cinemaObjectToTest = JsonSerializer.Deserialize<CinemaInformationModel>(json2);
        Assert.IsTrue(cinemaObjectToTest.City == cinemaTest.City
                      && cinemaObjectToTest.Address == cinemaTest.Address
                      && cinemaObjectToTest.OpeningTime == cinemaTest.OpeningTime
                      && cinemaObjectToTest.ClosingTime == cinemaTest.ClosingTime
                      && cinemaObjectToTest.PhoneNumber == cinemaTest.PhoneNumber
                      && cinemaObjectToTest.Email == cinemaTest.Email);
    }

    [TestMethod]
    public void TestCinemaInfoLoad()
    {
        // Create Cinema Information Model
        CinemaInformationModel cinemaTest = new CinemaInformationModel(
            "Test City",
            "Test Address",
            "Test Opening Time",
            "Test Closing Time",
            "Test PhoneNumber",
            "Test Email");

        //Write cinemaTest to json

        CinemaInformationAccess.WriteInfoCinema(cinemaTest);


        CinemaInformationModel cinemaInfoToTest = CinemaInformationAccess.LoadInfo();
        // Test if all fields are the same
        Assert.IsTrue(cinemaTest.City == cinemaInfoToTest.City
                      && cinemaTest.Address == cinemaInfoToTest.Address
                      && cinemaTest.OpeningTime == cinemaInfoToTest.OpeningTime
                      && cinemaTest.ClosingTime == cinemaInfoToTest.ClosingTime
                      && cinemaTest.PhoneNumber == cinemaInfoToTest.PhoneNumber
                      && cinemaTest.Email == cinemaInfoToTest.Email);
    }


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
        ReservationModel reservation = new ReservationModel(999, 500, 300, new List<string> { "A1", "A2" }, 20, "123456");
        ReservationModel reservation2 = new ReservationModel(1000, 501, 300, new List<string> { "A1", "A2" }, 20, "123456");
        ReservationModel reservation3 = new ReservationModel(1001, 502, 300, new List<string> { "A1", "A2" }, 20, "123456");

        // Add the reservations to the list
        reservationsLogic.AddNewReservation(reservation.Id, reservation.Showing_ID, reservation.Account_ID, reservation.Seats, reservation.Price, reservation.Unique_code);
        reservationsLogic.AddNewReservation(reservation2.Id, reservation2.Showing_ID, reservation2.Account_ID, reservation2.Seats, reservation2.Price, reservation2.Unique_code);
        reservationsLogic.AddNewReservation(reservation3.Id, reservation3.Showing_ID, reservation3.Account_ID, reservation3.Seats, reservation3.Price, reservation3.Unique_code);

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
        Assert.IsFalse(FoodLogic.AddFood("Snickers", 250, default))
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

        Assert.AreEqual(10 , openSeatsTest2);

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
}