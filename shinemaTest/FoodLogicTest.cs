namespace shinemaTest;

[TestClass]
public class FoodLogicTest
{

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

    public void TestBuyFood()
    {

    }

}