using Read_Planet.Models;

namespace Read_Planet.Services
{
    public interface IMessengerService
    {
        Task<bool> Send(Message message, string attachment = "");
    }
}
