namespace shinemaTest;

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
            Assert.AreEqual(true, AccountsLogic.CheckFullName(name));

        }

        foreach (string name in incorrect_names)
        {
            // Assert
            Assert.AreEqual(false, AccountsLogic.CheckFullName(name));
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
}