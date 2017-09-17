using System.ComponentModel.DataAnnotations.Schema;
using BloodConnector.WebAPI.Interface;

namespace BloodConnector.WebAPI.Models
{
    public class BaseEntity : IObjectWithState
    {
        /*[Required]
        public string CreatedBy { get; set; }

        [Required]
        public string UpdatedBy { get; set; }*/

        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}