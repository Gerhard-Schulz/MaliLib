using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaliLb.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Год издания")]
        [Required(ErrorMessage = "Введите год издания")]
        public DateTime YearOfPublishing { get; set; }

        [DisplayName("Произведение")]
        [Required(ErrorMessage = "Введите название произведения")]
        public int WorkID { get; set; }
        [ForeignKey("WorkID")]
        public Work Work { get; set; }

        [DisplayName("Издание")]
        [Required(ErrorMessage = "Введите название издания")]
        public int EditionID { get; set; }
        [ForeignKey("EditionID")]
        public Edition Edition { get; set; }

        [DisplayName("Читатель, взявший книгу")]
        public int? ReaderID { get; set; }
        [ForeignKey("ReaderID")]
        public Reader Reader { get; set; }
    }
}
