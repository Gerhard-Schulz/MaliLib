using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaliLb.Models
{
    public class Genre
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Название")]
        [Required(ErrorMessage = "Введите название жанра")]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
