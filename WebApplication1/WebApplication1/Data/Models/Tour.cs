using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Data.Models;
    public class Tour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Days { get; set; }
        public int Cost { get; set; }
        public string Level { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }
    }