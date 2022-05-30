
namespace Blazor.Data.Models;

public class Tour
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public int Days { get; set; }
    public int Cost { get; set; }
    public string Level { get; set; }
    public string Desc { get; set; }

    public string Image { get; set; }
    public int[] SchedulesIds { get; set; }
}
    

