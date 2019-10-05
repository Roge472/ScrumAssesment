using BaseModelLibrary.Lib.Models.UserModels;
using BaseModelLibrary.Models.Pocker;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseModelLibrary.IServices
{
    public interface IPokerRoomService
    {
        ChatRoom AddRoom(string roomName);
        void DeleteRoom(Guid guid);
        void AddUser(Guid roomGuid, User user);
        void RemoveUser(Guid roomGuid, User user);
        void RemvoeUser(string connectionId);
        void Refresh(Guid roomGuid);
        ChatRoom GetRoom(Guid roomGuid);
        ChatRoom GetRoom(string connecionId);
    }
}
