using BaseModelLibrary.Lib.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseModelLibrary.Models.UserModels
{
    public class Role : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
    public enum Rols
    {
        user,
        moderator
    }
}
