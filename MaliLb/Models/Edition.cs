using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaliLb.Models
{
    public class Edition
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Название")]
        [Required(ErrorMessage = "Введите название издания")]
        public string Name { get; set; }
    }
}
