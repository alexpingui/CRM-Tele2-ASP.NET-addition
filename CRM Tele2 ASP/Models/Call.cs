namespace CRM_Tele2_ASP.Models
{
    public class Call()
    {
        public int Id { get; set; }

        public string ClientPhoneNumber { get; set; } = null!; 
        public Client Client { get; set; } 


        public string ClientName { get; set; } = "";
        public string ClientAddress { get; set; } = "";

        public string? Comment { get; set; } = "";
        public DateTime DateOfCall { get; set; }
        public DateTime? DateOfScheduledCall { get; set; }
    }
}
