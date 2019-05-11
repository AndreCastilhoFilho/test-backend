using System.Collections.Generic;

namespace PrBanco.API.Services
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            Success = true;
            Notifications = new List<string>();
        }

        public ServiceResult(bool success, List<string> notifications)
        {
            Success = success;
            Notifications = notifications;
        }

        public bool Success { get; set; }
        public List<string> Notifications { get; private set; }

    }
}
