using System.Xml.Serialization;

public static class CinemaInfo
{
    public static void EditLoop()
    {

        // default json
        // {
        //     "city": "Rotterdam",
        //     "address": "Wijnhaven 107, 3011 WN",
        //     "openingtime": "10:00",
        //     "closingtime": "22:00",
        //     "phonenumber": "06123456789",
        //     "email": "Chinema@email.com"
        // }
        bool cinemaInfoRedo = true;
        CinemaInformationModel newCinemaInformation = CinemaInfoLogic.GetCinemaInfoObject();
        while (cinemaInfoRedo)
        {
            List<string> options = new List<string> { "Change City", "Change Address", "Change Opening Time", "Change Closing Time", "Change Phone Number", "Change Email" };
            //choice Menu
            string choiceEditCinemaInfo = NavigationMenu.DisplayMenu(options);
            Console.WriteLine("Enter New Info:\n");

            if (choiceEditCinemaInfo == "1")
            {
                Console.WriteLine("What is the city where the cinema is located: (Example: \"Rotterdam\")");
                newCinemaInformation.City = Console.ReadLine();
                Thread.Sleep(1000);
            }
            else if (choiceEditCinemaInfo == "2")
            {
                Console.WriteLine("What is the Address: (Example: \"Wijnhaven 107, 3011 WN\")");
                newCinemaInformation.Address = Console.ReadLine();
                Thread.Sleep(1000);
            }
            

            

            else if (choiceEditCinemaInfo == "3")
            {
                Console.WriteLine("At what time (24h format) does the cinema open: (Example: \"09:00\")");
                newCinemaInformation.OpeningTime = Console.ReadLine();
                Thread.Sleep(1000);
            }
            
            
            else if (choiceEditCinemaInfo == "4")
            {
                Console.WriteLine("At what time (24h format) does the cinema close: (Example: \"22:00\")");
                newCinemaInformation.ClosingTime = Console.ReadLine();
                Thread.Sleep(1000);
            }
            

            else if (choiceEditCinemaInfo == "5")
            {
                Console.WriteLine("What is the phone number of the cinema: ");
                newCinemaInformation.PhoneNumber = Console.ReadLine();
                Thread.Sleep(1000);
            }
            

            else if (choiceEditCinemaInfo == "6")
            {
                Console.WriteLine("What is the e-mail of the cinema: ");
                newCinemaInformation.Email = Console.ReadLine();
                Thread.Sleep(1000);
            }
            
            Console.Clear();

            Console.WriteLine("This is what it will look like:\n");
            Console.WriteLine(CinemaInfoLogic.GetCinemaInfo(newCinemaInformation));

            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

            bool cinemaInfoChoosing = true;
            while (cinemaInfoChoosing)
            {

                List<string> optionEditCinemaInfo = new List<string> { "Save Cinema Info", "Continue Changing", "Cancel" };
                string choiceCinemaInfo = NavigationMenu.DisplayMenu(optionEditCinemaInfo);

                if (choiceCinemaInfo == "1")
                {
                    cinemaInfoChoosing = false;
                    cinemaInfoRedo = false;
                    CinemaInfoLogic.SaveCinemaInfo(newCinemaInformation);
                    Thread.Sleep(1000);
                    Console.WriteLine("Info Saved");
                }

                else if (choiceCinemaInfo == "2")
                {
                    cinemaInfoChoosing = false;
                }

                else if (choiceCinemaInfo == "3")
                {
                    cinemaInfoChoosing = false;
                    cinemaInfoRedo = false;

                }

            }
            Console.Clear();
        }
    }
}