using BaseModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaseModelLibrary.Lib.Models.UserModels
{
    public class EmailLogin:BaseModel
    {
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public short IsEmailConfirmed { get; set; }
    }
}
