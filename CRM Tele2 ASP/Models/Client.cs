using System.ComponentModel.DataAnnotations;

namespace CRM_Tele2_ASP.Models
{
    public class Client
    {
        
        public int Id { get; set; }
        [Key]
        [Required(ErrorMessage ="Номер телефона - обязательное поле")]
        [StringLength(12), MinLength(11, ErrorMessage = "Номер телефона должен быть не короче 11 символов")]
        [Phone]
        [RegularExpression(@"^\+77\d{9}",ErrorMessage ="Введите телефон в формате +77...")]
        public string PhoneNumber { get; set; } = "Все семёрки";

        [StringLength(100, ErrorMessage ="Слишком длинное имя")]
        public string? Name { get; set; } = "Безымянный";

        [StringLength(100, ErrorMessage = "Слишком длинный адрес")]
        public string? Address { get; set; } = "Без определённого места жительства";
    }
}
