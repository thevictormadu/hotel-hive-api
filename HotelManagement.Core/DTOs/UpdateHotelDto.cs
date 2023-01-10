using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.DTOs
{
    public class UpdateHotelDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string? BankCode { get; set; }
        public string Location { get; set; }
        public string Rating { get; set; }
    }
}
