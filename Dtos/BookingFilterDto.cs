namespace CarBookingApi.Dtos
{
    public class BookingFilterDto
    {
        public int? CarId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;
        }
    }
}
