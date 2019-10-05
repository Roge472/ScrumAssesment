using BaseModelLibrary.Models;
using BaseModelLibrary.Models.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaseModelLibrary.Lib.Models.UserModels
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Theme { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastEntranceDate { get; set; }
        public virtual ICollection<SocialLogin> SocialLogins { get; set; } 
        public virtual EmailLogin EmailLogin { get; set; }
        public virtual Role Role { get; set; }
        [NotMapped]
        public string ConnectionId { get; set; }
    }
}
