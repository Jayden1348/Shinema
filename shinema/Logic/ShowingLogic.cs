using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
class ShowingsLogic
{
    private List<ShowingModel> _showings;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself

    public ShowingsLogic()
    {
        _showings = ShowingsAccess.LoadAll();
    }


    public void UpdateList(ShowingModel show)
    {
        //Find if there is already an model with the same id
        int index = _showings.FindIndex(s => s.ID == show.ID);

        if (index != -1)
        {
            //update existing model
            _showings[index] = show;
        }
        else
        {
            //add new model
            _showings.Add(show);
        }
        ShowingsAccess.WriteAll(_showings);

    }

    public ShowingModel GetById(int id)
    {
        return _showings.Find(i => i.ID == id);
    }

    public int GetNextId()
    {
        int maxId = _showings.Max(account => account.ID);
        return maxId + 1;
    }




}




