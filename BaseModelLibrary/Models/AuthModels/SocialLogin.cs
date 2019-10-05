using BaseModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseModelLibrary.Lib.Models.UserModels
{
    public class SocialLogin:BaseModel
    {
        public virtual LoginProvider LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string Login { get; set; }
        public virtual User User{ get; set; }
    }
}
