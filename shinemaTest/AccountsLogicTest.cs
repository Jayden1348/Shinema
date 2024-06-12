namespace shinemaTest;

[TestClass]
public class AccountLogicTest
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


    [TestMethod]
    public void TestCheckFullName()
    {
        // Arrange
        List<string> correct_names = new List<string> { "Cristiano", "Hogeschool", "Hoge-school", "Test Persoon" };
        List<string> incorrect_names = new List<string> { "Cr1stian0", " Rotterdam", "12345678", "Hogeschool1", "Hogeschool  Test" };


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
        List<string> correct_passwords = new List<string> { "Cr1stiano", "Hog3sch00l", "Test1234", "Hogeschool1" };
        List<string> incorrect_passwords = new List<string> { "cr1stian0", "Hoge-school", "Test", "12345678", "Hogeschool" };

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