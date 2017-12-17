using System.Threading.Tasks;

namespace WishListRestService.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
