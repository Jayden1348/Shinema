namespace shinemaTest;

[TestClass]
public class ReservationLogicTest
{

    [TestMethod]

    public void TestDeleteReservation()
    {
        // make instance of ReservationsLogic
        ReservationLogic reservationsLogic = new ReservationLogic();

        // Create new reservations
        ReservationModel reservation1 = new ReservationModel(999, 500, 300, new List<string> { "A1", "A2" }, 20, "123456", null, null);
        ReservationModel reservation2 = new ReservationModel(1000, 501, 300, new List<string> { "A1", "A2" }, 20, "123456", null, null);
        ReservationModel reservation3 = new ReservationModel(1001, 502, 300, new List<string> { "A1", "A2" }, 20, "123456", null, null);

        // Add the reservations to the list
        reservationsLogic.AddNewReservation(reservation1.Id, reservation1.Showing_ID, reservation1.Account_ID, reservation1.Seats, reservation1.Price, reservation1.Unique_code, null, null);
        reservationsLogic.AddNewReservation(reservation2.Id, reservation2.Showing_ID, reservation2.Account_ID, reservation2.Seats, reservation2.Price, reservation2.Unique_code, null, null);
        reservationsLogic.AddNewReservation(reservation3.Id, reservation3.Showing_ID, reservation3.Account_ID, reservation3.Seats, reservation3.Price, reservation3.Unique_code, null, null);


        // Check if the reservations have been added
        Assert.IsTrue(reservationsLogic.GetAllReservations().Any(r => r.Id == reservation1.Id));
        Assert.IsTrue(reservationsLogic.GetAllReservations().Any(r => r.Id == reservation2.Id));
        Assert.IsTrue(reservationsLogic.GetAllReservations().Any(r => r.Id == reservation3.Id));

        // delete the reservation
        List<int> showingIds = new List<int> { reservation1.Showing_ID, reservation2.Showing_ID, reservation3.Showing_ID };
        reservationsLogic.DeleteReservation(showingIds);

        // Check if the reservation is deleted
        Assert.IsFalse(reservationsLogic.GetAllReservations().Any(r => r.Id == reservation1.Id));
        Assert.IsFalse(reservationsLogic.GetAllReservations().Any(r => r.Id == reservation2.Id));
        Assert.IsFalse(reservationsLogic.GetAllReservations().Any(r => r.Id == reservation3.Id));

    }

}