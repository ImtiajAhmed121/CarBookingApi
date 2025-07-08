namespace CarBookingApi.Dtos
{
    public class CreateUpdateBookingDto
    {
        public int CarId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string RepeatOption { get; set; } = "DoesNotRepeat"; // daily or weekly
    }
}
