using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_Tele2_ASP.Models
{
    public class Call()
    {
        public int Id { get; set; }

        public string ClientPhoneNumber { get; set; } = null!; 
        public string? ClientName { get; set; }
        public string? ClientAddress { get; set; }

        public string? Comment { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateOfCall { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfScheduledCall { get; set; }

        [ForeignKey(nameof(ClientPhoneNumber))]
        public Client? Client { get; set; }
    }
}
