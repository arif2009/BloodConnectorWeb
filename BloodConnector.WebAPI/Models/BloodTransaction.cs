using System;

namespace BloodConnector.WebAPI.Models
{
    public class BloodTransaction
    {
        public long ID { get; set; }
        public string DonorId { get; set; }
        public User Donor { get; set; }
        public string ReceiverId { get; set; }
        public User Receiver { get; set; }
        public DateTime Date { get; set; }
    }
}