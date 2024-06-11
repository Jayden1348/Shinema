namespace shinemaTest;

[TestClass]
public class CinemaInfoLogicTest
{

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

}