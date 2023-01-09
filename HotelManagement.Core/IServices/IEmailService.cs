using HotelManagement.Core.DTOs;

namespace HotelManagement.Core.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
