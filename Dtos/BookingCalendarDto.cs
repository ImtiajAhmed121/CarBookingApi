namespace CarBookingApi.Dtos
{
    public class BookingCalendarDto
    {
        public int CarId {  get; set; }
        public string CarName { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
