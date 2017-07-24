using System;

namespace BloodConnector.WebAPI.Models
{
    public class BloodTransaction
    {
        public long ID { get; set; }
        public string DonorId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime Date { get; set; }
    }
}