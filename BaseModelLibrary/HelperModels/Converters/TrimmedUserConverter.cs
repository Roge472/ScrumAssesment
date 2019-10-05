using BaseModelLibrary.HelperModels.TrimmedModles.UserModels;
using BaseModelLibrary.Lib.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseModelLibrary.HelperModels.Converters
{
    public static class TrimmedUserConverter
    {
        public static User FromTrimmedUserToUser(TrimmedEmailUser trimmedEmailUser)
        {
            User user = new User();
            user.Name = trimmedEmailUser.FirstName;
            user.LastName = trimmedEmailUser.LastName;
            return user;
        }
    }
}
