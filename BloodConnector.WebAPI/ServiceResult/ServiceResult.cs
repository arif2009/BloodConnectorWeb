using System.Collections.Generic;

namespace BloodConnector.WebAPI.ServiceResult
{
    public class ServiceResult<TViewModel>
    {
        public ServiceResult()
        {
            ErrorMessages = new List<KeyValuePair<string, string>>();
        }

        public bool Success
        {
            get { return ErrorMessages.Count == 0; }
        }

        public List<KeyValuePair<string, string>> ErrorMessages { get; set; }
        public TViewModel Model { get; set; }
    }
}