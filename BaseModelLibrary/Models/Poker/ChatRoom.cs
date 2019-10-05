using BaseModelLibrary.Lib.Models.UserModels;
using BaseModelLibrary.Models.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseModelLibrary.Models.Pocker
{
    public class ChatRoom
    {
        public Guid ChatRoomGuid { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Dictionary<User, Point> Points { get; set; }
    }

    public static class CharRoomHelper
    {
        public static void ResetPoints(this ChatRoom room)
        {
            var points = room.Points;
            foreach(var point in points)
            {
                points[point.Key] = null;
            }
        }
        public static void RemoveUser(this ChatRoom room, User user)
        {
            var points = room.Points;
            room.Users.Remove(user);
            points.Remove(user);
        }
        public static void Refresh(this ChatRoom room)
        {
            var points=room.Points;
            var newPoints = new Dictionary<User, Point>(points.Count);
            foreach(var point in points)
            {
                newPoints[point.Key] = null;
            }
            room.Points = newPoints;

        }
    }

}
