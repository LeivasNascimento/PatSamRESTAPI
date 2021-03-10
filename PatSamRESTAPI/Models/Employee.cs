using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatSamRESTAPI.Models
{
    [Table("Employee")]
    public class Employee
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [RegularExpression(@"^(100|100{1-9][0-9]?)$")]
        public int Age { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime DOB { get; set; }
        public bool Manager { get; set; }
    }
}
