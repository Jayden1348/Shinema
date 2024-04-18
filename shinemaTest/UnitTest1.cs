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

    [TestMethod]
    public void TestCheckFullName()
    {
        // Arrange
        List<string> correct_names = new List<string> { "Cristiano", "Hogeschool" };
        List<string> incorrect_names = new List<string> { "Cr1stian0", "Hoge-school" };


        // Act
        foreach (string name in correct_names)
        {
            // Assert
            Assert.IsTrue(AccountsLogic.CheckFullName(name));

        }

        foreach (string name in incorrect_names)
        {
            // Assert
            Assert.IsFalse(AccountsLogic.CheckFullName(name));
        }
    }

    [TestMethod]
    public void TestCheckPassword()
    {
        List<string> correct_passwords = new List<string> { "Cr1stiano", "Hog3sch00l" };
        List<string> incorrect_passwords = new List<string> { "cr1stian0", "Hoge-school" };

        // Act
        foreach (string password in correct_passwords)
        {
            // Assert
            Assert.AreEqual(true, AccountsLogic.CheckPassword(password));

        }

        foreach (string password in incorrect_passwords)
        {
            // Assert
            Assert.AreEqual(false, AccountsLogic.CheckPassword(password));
        }
    }

    [TestMethod]
    public void TestEmail()
    {
        // Instantiate AccountsLogic
        AccountsLogic accountsLogic = new AccountsLogic();

        // Add a new account
        accountsLogic.AddNewAccount(9999, "test@email.com", "Hogeschool1", "TestPersoon", true, true, true);


        // Check if email exists
        bool test = accountsLogic.CheckEmail("test@email.com");

        // Assert the test result
        Assert.IsFalse(test);
    }


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

}