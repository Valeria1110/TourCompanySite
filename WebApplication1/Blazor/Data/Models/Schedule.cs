

namespace Blazor.Data.Models;

public class Schedule
{
    public int IdS { get; set; }
    public string DateBegin { get; set; }
    public string DateEnd { get; set; }
    public int FreePlaces { get; set; }
    public string Image { get; set; }
    public int Id { get; set; }
    public int[] OrdersIds { get; set; }
}
