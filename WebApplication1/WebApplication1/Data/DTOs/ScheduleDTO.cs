namespace WebApplication1.Data.DTOs
{
    public class ScheduleDTO
    {
        public int IdS { get; set; }
        public string DateBegin { get; set; }
        public string DateEnd { get; set; }
        public int FreePlaces { get; set; }
        public string Image { get; set; }
        public int[] OrdersIds { get; set; }
        public int Id { get; set; }
    }
}
