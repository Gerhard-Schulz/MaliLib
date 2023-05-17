using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaliLb.Models
{
    public class Author
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("ФИО")]
        [Required(ErrorMessage = "Введите ФИО автора")]
        public string Name { get; set; }
    }
}
