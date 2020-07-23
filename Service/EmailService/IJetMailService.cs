using System.Threading.Tasks;

namespace Service.EmailService
{
    public interface IJetMailService
    {
        Task<bool> SendEmailWithJetServiceAsync(string email, string subject, string message, string receiverName);
    }
}