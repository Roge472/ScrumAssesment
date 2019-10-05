using AutoMapper;
using BaseModelLibrary.Lib.Models.UserModels;
using BaseModelLibrary.Models.Poker;
using BaseModelLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseModelLibrary.Profiles
{
    public class PointProfile:Profile
    {
        PointProfile()
        {

        }
    }
    public static class PointConverter
    {
        public static List<PointViewModel> Convert(this Dictionary<User, Point> dict)
        {
            List<PointViewModel> li = new List<PointViewModel>(dict.Count);
            foreach(var item in dict)
            {
                li.Add(new PointViewModel() { ConnectionId = item.Key.ConnectionId, Points = item.Value?.Points });
            }
            return li;
        }
    }
}
