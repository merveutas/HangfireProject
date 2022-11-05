using ScheduleControl.Entities.Dtos.Mail;
using ScheduleControl.Entities.Models;
using System.Threading.Tasks;

namespace ScheduleControl.Business.Abstract.Mail
{
    public interface IMailService
    {
        Task SendMailAsync(MailMessageDto mailMessageDto);

        Task SendUserRegisterMailAsync(int userId);

        Task SendDevAppUserMailAsync(int userId, DevApp devApp);
    }
}