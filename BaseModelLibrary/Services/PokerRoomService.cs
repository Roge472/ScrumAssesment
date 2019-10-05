using BaseModelLibrary.IServices;
using BaseModelLibrary.Lib.Models.UserModels;
using BaseModelLibrary.Models.Pocker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseModelLibrary.Services
{
    public class PokerRoomService:IPokerRoomService
    {
        #region Properties
        public List<ChatRoom> ChatRooms { get; set; }
        #endregion

        public PokerRoomService()
        {
            ChatRooms = new List<ChatRoom>();
        }

        #region IPokerRoomService
        public ChatRoom AddRoom(string roomName)
        {
            ChatRoom chatRoom = new ChatRoom()
            {
                ChatRoomGuid = Guid.NewGuid(),
                Name = roomName,
                Users=new List<User>(),
                Points=new Dictionary<User, Models.Poker.Point>()
            };
            ChatRooms.Add(chatRoom);
            return chatRoom;
        }

        public void AddUser(Guid roomGuid, User user)
        {
            var room = GetRoom(roomGuid);
            if (room == null) throw new ArgumentNullException("There is no such room to add user");
            room.Users.Add(user);
        }

        public void DeleteRoom(Guid roomGuid)
        {
            var room = GetRoom(roomGuid);
            if (room == null) throw new ArgumentNullException("There is no such room to delete");
            ChatRooms.Remove(room);
        }

        public void Refresh(Guid roomGuid)
        {
            var room = GetRoom(roomGuid);
            if (room == null) throw new ArgumentNullException("There is no such room to refresh");
            room.ResetPoints();
        }

        public void RemoveUser(Guid roomGuid, User user)
        {
            var room = GetRoom(roomGuid);
            if (room == null) throw new ArgumentNullException("There is no such room to delete user");
            room.RemoveUser(user);
        }

        public ChatRoom GetRoom(Guid roomGuid)
        {
            return ChatRooms.SingleOrDefault(chatRoom => chatRoom.ChatRoomGuid == roomGuid);
        }

        public void RemvoeUser(string connectionId)
        {
            try
            {
                var room = ChatRooms.SingleOrDefault(r => r.Users.SingleOrDefault(u => u.ConnectionId == connectionId) != null);
                var user = room.Users.SingleOrDefault(u => u.ConnectionId == connectionId);
                room.Users.Remove(user);
                var point = room.Points.SingleOrDefault(p => p.Key.ConnectionId == connectionId);
                room.Points.Remove(point.Key);
            }
            catch { }
        }

        public ChatRoom GetRoom(string connectionId)
        {
            return ChatRooms.SingleOrDefault(r => r.Users.SingleOrDefault(u => u.ConnectionId == connectionId) != null);
        }
        #endregion
    }
}
