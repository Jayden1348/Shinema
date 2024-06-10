using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class ShowingsLogic
{
    private List<ShowingModel> _showings;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself

    public ShowingsLogic()
    {
        _showings = GenericAccess<ShowingModel>.LoadAll();
    }

    public void UpdateShowings(ShowingModel show)
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
        GenericAccess<ShowingModel>.WriteAll(_showings);

    }

    public ShowingModel GetById(int id)
    {
        return _showings.Find(i => i.ID == id);
    }

    public int GetNextId()
    {
        if (_showings.Count == 0) return 1;
        int maxId = _showings.Max(account => account.ID);
        return maxId + 1;
    }

    public int AddNewShowing(int id, int roomid, int movieid, DateTime datetime, int test1)
    {
        if (test1 == 1)
        {
            ShowingModel newShowing = new ShowingModel(id, roomid, movieid, datetime);
            UpdateShowings(newShowing);
        }
        return test1;

    }

    public List<ShowingModel> FilterByMovie(MovieModel chosen_movie)
    {
        List<ShowingModel> filtered_showings = new();
        foreach (ShowingModel movie in _showings)
        {
            if (movie.MovieID == chosen_movie.ID)
            {
                if (ValidateDate(movie) == 1)
                {
                    filtered_showings.Add(movie);
                }
            }
        }
        return filtered_showings;
    }

    public int ValidateDate(ShowingModel ns)
    {
        // Check if date is in the future
        DateTime start_ns = ns.Datetime;
        DateTime end_ns = ns.Datetime.AddMinutes(MoviesLogic.GetById(ns.MovieID).Length);
        if (DateTime.Compare(start_ns, DateTime.Now) < 0)
        {
            return 2;
        }

        // Check if date after opening and before closing
        CinemaInformationModel info = CinemaInfoLogic.GetCinemaInfoObject();
        string[] split_time = info.OpeningTime.Split(':');
        TimeOnly opening = new TimeOnly(Convert.ToInt32(split_time[0]), Convert.ToInt32(split_time[1]));

        split_time = info.ClosingTime.Split(':');
        TimeOnly closing = new TimeOnly(Convert.ToInt32(split_time[0]), Convert.ToInt32(split_time[1]));

        if ((opening > TimeOnly.FromDateTime(start_ns)) || (closing < TimeOnly.FromDateTime(end_ns)))
        {
            return 3;
        }

        else
        {
            // Check for existing showings at that time
            foreach (ShowingModel s in _showings)
            {
                if (s.RoomID == ns.RoomID)
                {
                    DateTime start_s = s.Datetime;
                    DateTime end_s = s.Datetime.AddMinutes(MoviesLogic.GetById(s.MovieID).Length);
                    if ((start_s < start_ns && end_s.AddMinutes(30) > start_ns) || (start_s > start_ns && end_ns.AddMinutes(30) > start_s))
                    {
                        return 4;
                    }
                }
            }
        }
        return 1;
    }

    public static DateTime SetToDatetime(string date, string time)
    {
        try
        {
            List<string> datelist = date.Split("-").ToList();
            if (datelist.Count != 3) { return new DateTime(); }
            int day = Convert.ToInt32(datelist[0]);
            int month = Convert.ToInt32(datelist[1]);
            int year = Convert.ToInt32(datelist[2]);

            List<string> timelist = time.Split(":").ToList();
            if (timelist.Count != 2) { return new DateTime(); }
            int hour = Convert.ToInt32(timelist[0]);
            int minutes = Convert.ToInt32(timelist[1]);
            return new DateTime(year, month, day, hour, minutes, 0);
        }
        catch
        {
            return new DateTime();
        }
    }

    public List<int> GetShowingID(int movieid)
    {
        List<int> showings = new();
        foreach (ShowingModel show in _showings)
        {
            if (show.MovieID == movieid)
            {
                showings.Add(show.ID);
            }
        }
        return showings;
    }

    public void DeleteShowing(int id)
    {
        _showings.RemoveAll(s => s.ID == id);
        GenericAccess<ShowingModel>.WriteAll(_showings);
    }

    public void DeleteShowing(List<int> ids)
    {
        foreach (int id in ids)
        {
            _showings.RemoveAll(s => s.ID == id);
        }
        GenericAccess<ShowingModel>.WriteAll(_showings);
    }

    public List<ShowingModel> GetAllShowings()
    {
        return _showings;
    }
}




