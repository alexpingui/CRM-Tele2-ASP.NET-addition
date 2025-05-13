using System.ComponentModel.DataAnnotations;

namespace CRM_Tele2_ASP.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Key]
        public string PhoneNumber { get; set; } = "Все семёрки";
        public string Name { get; set; } = "Безымянный";
        public string Address { get; set; } = "Без определённого места жительства";
       

        // Навигационное свойство — связь один-ко-многим
        public List<Call> Calls { get; set; } = new();
    }
}
