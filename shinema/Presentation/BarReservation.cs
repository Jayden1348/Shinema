using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using Microsoft.Win32.SafeHandles;

public static class BarReservation
{
    public static void ReserveBarSeatsInteraction(DateTime date, string reservationCode, int userID, int reservedShowingSeats)
    {
        BarReservationLogic b = new BarReservationLogic();
        int availableNumberOfSeats = BarReservationLogic.CheckBarAvailability(date);
        if (availableNumberOfSeats == 0)
        {
            Console.Clear();
            Console.WriteLine("Unfortunately there are no available seats left in the bar.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

        }
        else
        {
            int amountOfSeatsToReserve = 0;

            List<string> yesNo = new List<string> { "Yes", "No" };
            string reserveChoice = NavigationMenu.DisplayMenu(yesNo, "Would you like to reserve seats at our bar");
            Console.Clear();
            if (reserveChoice == "2")
            {
                return;
            }
            bool isValidInput = false;
            while (!isValidInput)
            {
                Console.WriteLine($"There are {availableNumberOfSeats} available seats.");
                int maxSeats = reservedShowingSeats;

                if (reservedShowingSeats > availableNumberOfSeats)
                {
                    maxSeats = availableNumberOfSeats;
                }
                Console.WriteLine($"How many seats would you like to reserve (max {maxSeats} seats)");
                Console.WriteLine($"\nEnter 0 to quit");
                string stringInputSeats = Console.ReadLine();
                Console.Clear();
                bool isValidInt = int.TryParse(stringInputSeats, out amountOfSeatsToReserve);
                if (!isValidInt)
                {
                    Console.WriteLine("Invalid input, input is not a number");
                }

                else if (amountOfSeatsToReserve < 0)
                {
                    Console.WriteLine("You are not allowed to book negative seats");
                }

                else if (amountOfSeatsToReserve > maxSeats)
                {
                    Console.WriteLine($"You are not allowed to book more than {maxSeats} seats");
                }

                else
                {
                    isValidInput = true;
                }
            }
            Console.Clear();
            if (amountOfSeatsToReserve > 0)
            {
                b.ReserveBarSeats(userID, reservationCode, date, amountOfSeatsToReserve);
            }
            if (amountOfSeatsToReserve == 0)
            {
                Console.WriteLine("Canceled bar reservation");
            }
            else
            {
                Console.WriteLine($"Reserved {amountOfSeatsToReserve} bar seats");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

    }
    public static void RemoveBarReservationInteraction(int userID)
    {
        BarReservationLogic b = new BarReservationLogic();
        List<BarReservationModel> userBarReservations = b.FindBarReservationUsingID(userID);
        if (!userBarReservations.Any())
        {
            Console.WriteLine("You don't have any reservations at out bar");
            return;
        }

        List<string> barReservationChoices = new List<string>();
        foreach (BarReservationModel barReservation in userBarReservations)
        {
            barReservationChoices.Add($"Reservation at {barReservation.Date} for {barReservation.Number_of_seats} seats");
        }
        string navigationOutput = NavigationMenu.DisplayMenu(barReservationChoices, "Which bar reservation would you like to cancel");
        Console.Clear();
        int navigationOutputInt = Convert.ToInt16(navigationOutput);
        string chosenToDelete = barReservationChoices.ElementAt(navigationOutputInt - 1);
        BarReservationModel reservationModel = userBarReservations.ElementAt(navigationOutputInt - 1);
        string yesNo = NavigationMenu.DisplayMenu(new List<string> { "Yes", "No" }, $"Would you like to cancel {chosenToDelete}");
        if (yesNo == "1")
        {
            Console.Clear();
            BarReservationLogic.RemoveBarSeatReservation(reservationModel.Unique_code);
            Console.WriteLine("Bar Reservation canceled");
        }
    }
}