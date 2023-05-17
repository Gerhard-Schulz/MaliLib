using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaliLb.Models
{
    public class Work
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Название")]
        [Required(ErrorMessage = "Введите название произведения")]
        public string Name { get; set; }

        [DisplayName("Дата написания")]
        [Required(ErrorMessage = "Введите дату написания")]
        public DateTime DateOfWriting { get; set; }

        [DisplayName("Автор")]
        [Required(ErrorMessage = "Укажите автора")]
        public int AuthorID { get; set; }
        [ForeignKey("AuthorID")]
        public Author Author { get; set; }

        [DisplayName("Жанр")]
        [Required(ErrorMessage = "Укажите жанр")]
        public int GenreID { get; set; }
        [ForeignKey("GenreID")]
        public Genre Genre { get; set; }
    }
}
