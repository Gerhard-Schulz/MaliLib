using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaliLb.Models
{
    public class Reader
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Номер читательского билета")]
        [Required(ErrorMessage = "Введите номер билета")]
        public int СardNumber { get; set; }

        [DisplayName("ФИО")]
        [Required(ErrorMessage = "Введите ФИО читателя")]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{СardNumber} - {Name}";
        }
    }
}
