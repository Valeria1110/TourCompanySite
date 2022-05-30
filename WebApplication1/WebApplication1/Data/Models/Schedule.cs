using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Data.Models;
public class Schedule
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Column(Order = 1)]

            public int IdS { get; set; }
            public string DateBegin { get; set; }
            public string DateEnd { get; set; }
            public int FreePlaces { get; set; }
            public Tour Tour { get; set; }
            public string Image { get; set; }
            public IEnumerable<Order> Orders { get; set; }

    }
    

