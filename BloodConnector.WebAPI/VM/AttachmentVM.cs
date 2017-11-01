using System;

namespace BloodConnector.WebAPI.VM
{
    public class AttachmentVM
    {
        public long ID { get; set; }
        public string UserId { get; set; }
        public string FileguId { get; set; }
        public int Type { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}