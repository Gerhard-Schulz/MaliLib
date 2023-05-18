using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaliLb.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Описание")]
        public string? Description { get; set; }


        [DisplayName("Обложка")]
        [Required(ErrorMessage = "Введите ссылку на фотографию")]
        public string Img { get; set; }

        [DisplayName("Год издания")]
        [Required(ErrorMessage = "Введите год издания")]
        public DateTime YearOfPublishing { get; set; }

        [DisplayName("Произведение")]
        [Required(ErrorMessage = "Введите название произведения")]
        [ForeignKey("Work")]
        public int WorkID { get; set; }
        [ValidateNever]
        public Work Work { get; set; }

        [DisplayName("Издание")]
        [Required(ErrorMessage = "Введите название издания")]
        [ForeignKey("Edition")]
        public int EditionID { get; set; }
        [ValidateNever]
        public Edition Edition { get; set; }

        [DisplayName("Читатель, взявший книгу")]
        [ForeignKey("Reader")]
        public int? VisitorID { get; set; }
        [ValidateNever]
        public Visitor? Visitor { get; set; }

        public override string ToString()
        {
            return $"{Work} [{Edition}]";
        }
    }
}
