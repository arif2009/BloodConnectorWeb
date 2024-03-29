﻿using System;

namespace BloodConnector.WebAPI.Models
{
    public class Attachment
    {
        public long ID { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string FileguId { get; set; }
        public int Type { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}