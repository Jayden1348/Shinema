//Console.WriteLine("Welcome to this amazing program");
//Thread.Sleep(2000);
//Menu.Start();
AccountModel user = new AccountModel(1071427, "1071427@hr.nl", "password", "Jayden");
ShowingModel show = new ShowingModel(1, 3, 1, new DateTime(2015, 12, 25), new DateTime(2015, 12, 25));
SeatReservation.StartReservation(user, show);