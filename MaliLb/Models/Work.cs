using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        [DisplayName("Описание")]
        public string? Description { get; set; }

        [DisplayName("Дата написания")]
        [Required(ErrorMessage = "Введите дату написания")]
        public DateTime DateOfWriting { get; set; }

        [DisplayName("Автор")]
        [Required(ErrorMessage = "Укажите автора")]
        [ForeignKey("Author")]
        public int AuthorID { get; set; }
        [ValidateNever]
        public Author Author { get; set; }

        [DisplayName("Жанр")]
        [Required(ErrorMessage = "Укажите жанр")]
        [ForeignKey("Genre")]
        public int GenreID { get; set; }
        [ValidateNever]
        public Genre Genre { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Author}) - {Genre}";
        }
    }
}
