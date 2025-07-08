namespace CarBookingApi.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string RepeatOption { get; set; } = "DoesNotRepeat"; // or repeat daily or weekly

        public Car Car { get; set; }

    }
}
