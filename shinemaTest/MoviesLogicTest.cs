namespace shinemaTest;

[TestClass]
public class MovieLogicTest
{
    [TestMethod]

    public void TestDeleteMovie()
    {
        // Create a new movie
        MovieModel movie = new MovieModel(1, "Test Movie", 120, "16", "Test Description", new List<string> { "Action" }, "2021-01-01");

        // Add the movie to the list
        MoviesLogic.AddMovie(movie.ID, movie.Title, movie.Length, movie.Age, movie.Description, movie.Genre, movie.Release_Date);

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

    public void TestCheckIfMovieExist()
    {
        string movieDiscription = "The Godfather \"Don\" Vito Corleone is the head of the Corleone mafia family in New York. He is at the event of his daughter's wedding. Michael, Vito's youngest son and a decorated WWII Marine is also present at the wedding. Michael seems to be uninterested in being a part of the family business. Vito is a powerful man, and is kind to all those who give him respect but is ruthless against those who do not. But when a powerful and treacherous rival wants to sell drugs and needs the Don's influence for the same, Vito refuses to do it. What follows is a clash between Vito's fading old values and the new ways which may cause Michael to do the thing he was most reluctant in doing and wage a mob war against all the other mafia families which could tear the Corleone family apart. ";
        MovieModel newMovie = new MovieModel(1, "The Godfather", 185, "14", movieDiscription, new List<string> { "Crime", "Drama" }, "1972");
        MoviesLogic.UpdateMovieList(newMovie);
        Assert.AreEqual(MoviesLogic.CheckIfMovieExist(newMovie.ID), newMovie);
    }

    [TestMethod]

    public void TestCheckIfMovieNotExist()
    {
        string movieDiscription = "The Godfather \"Don\" Vito Corleone is the head of the Corleone mafia family in New York. He is at the event of his daughter's wedding. Michael, Vito's youngest son and a decorated WWII Marine is also present at the wedding. Michael seems to be uninterested in being a part of the family business. Vito is a powerful man, and is kind to all those who give him respect but is ruthless against those who do not. But when a powerful and treacherous rival wants to sell drugs and needs the Don's influence for the same, Vito refuses to do it. What follows is a clash between Vito's fading old values and the new ways which may cause Michael to do the thing he was most reluctant in doing and wage a mob war against all the other mafia families which could tear the Corleone family apart. ";
        MovieModel newMovie = new MovieModel(1, "The Godfather", 185, "14", movieDiscription, new List<string> { "Crime", "Drama" }, "1972");
        MoviesLogic.UpdateMovieList(newMovie);
        Assert.AreEqual(MoviesLogic.CheckIfMovieExist(-1123), null);
    }



    [TestMethod]
    public void TestUpdateMovieList()
    {
        string movieDescription = "Inception is a 2010 science fiction action film written and directed by Christopher Nolan, who also produced the film with Emma Thomas. The film stars Leonardo DiCaprio as a professional thief who steals information by infiltrating the subconscious of his targets. He is offered a chance to have his criminal history erased as payment for the implantation of another person's idea into a target's subconscious.";
        MovieModel newMovie = new MovieModel(1, "Inception", 148, "13", movieDescription, new List<string> { "Action", "Sci-Fi", "Thriller" }, "2010");

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

}