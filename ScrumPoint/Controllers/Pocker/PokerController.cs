using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseModelLibrary.IServices;
using BaseModelLibrary.Lib.Models;
using BaseModelLibrary.Lib.Models.UserModels;
using BaseModelLibrary.Models.CloudModels;
using BaseModelLibrary.Models.Pocker;
using BaseModelLibrary.Models.Poker;
using BaseModelLibrary.Models.UserModels;
using InternetShopDBContext.Lib.Contexts;
using InternetShopDBContext.Lib.Repositories.IRepository.ICloudRepositories;
using InternetShopDBContext.Lib.Repositories.IRepository.IUserRepositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using BaseModelLibrary.Profiles;

namespace InternetShop.Controllers
{
    [Route("api/[controller]")]
    public class PokerPointController : Hub
    {
        IPokerRoomService _pokerRoomService;
        IUserRepository _userRepository;
        public PokerPointController(IPokerRoomService pokerRoomService, IUserRepository userRepository)
        {
            _pokerRoomService = pokerRoomService;
            _userRepository = userRepository;
        }

        [HttpPost("[action]")]
        public Task SetPoint(string roomGuid, int points)
        {
            try
            {
                var room = _pokerRoomService.GetRoom(new Guid(roomGuid));
                var point = room.Points.SingleOrDefault(u => u.Key.ConnectionId == Context.ConnectionId);
                room.Points[point.Key] = new Point() { Points = points };
                var connections = room.Users.Select(u => u.ConnectionId).ToList();
                return Clients.Clients(connections).SendAsync("SetPoint", (connection: Context.ConnectionId, points));
            }
            catch
            {

            }
            return null;
        }
        [HttpGet("[action]")]
        public Task GetData(string roomGuid)
        {
            var room = _pokerRoomService.GetRoom(new Guid(roomGuid));
            var data = (name: room.Name, users: room.Users, points: room.Points.Convert());
            return Clients.Clients(Context.ConnectionId).SendAsync("SendData", data);
        }
        public Task ShowPoints(string roomGuid)
        {
            return Clients.Clients(GetUsersConnectedToRoom(roomGuid)).SendAsync("ShowPoints");
        }
        public async Task RefreshPoints(string roomGuid)
        {
            var room = _pokerRoomService.GetRoom(new Guid(roomGuid));
            room.Refresh();
            var data = (name: room.Name, users: room.Users, points: room.Points.Convert());
            await Clients.Clients(GetUsersConnectedToRoom(roomGuid)).SendAsync("HidePoints");
            await Clients.Clients(GetUsersConnectedToRoom(roomGuid)).SendAsync("SendData", data);
        }
        [HttpPost("[action]")]
        public Task Connect(string guid, string userName, bool isObserver)
        {
            try
            {
                var connectionId = Context.ConnectionId;
                var room = _pokerRoomService.GetRoom(new Guid(guid));

                var identity = (ClaimsIdentity)Context.User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                if (claims.Any())
                {
                    try
                    {
                        var userId = Convert.ToInt32(claims.SingleOrDefault(c => c.Type == "Id").Value);
                        var user = _userRepository.Get(userId);
                        user.ConnectionId = connectionId;
                        user.Name = userName;

                        if (!room.Users.Contains(user))
                        {
                            _pokerRoomService.AddUser(room.ChatRoomGuid, user);
                            if (!isObserver)
                            {
                                room.Points.Add(user, null);
                            }
                        }


                        var data = (name: room.Name, users: room.Users, points: room.Points.Convert());
                        return Clients.Clients(GetUsersConnectedToRoom(guid)).SendAsync("SendData", data);

                    }
                    catch
                    {

                    }
                }
                else
                {
                    var user = new User();
                    user.ConnectionId = connectionId;
                    user.Name = userName;

                    if (!room.Users.Contains(user))
                    {
                        _pokerRoomService.AddUser(room.ChatRoomGuid, user);
                        if (!isObserver)
                        {
                            room.Points.Add(user, null);
                        }
                    }


                    var data = (name: room.Name, users: room.Users, points: room.Points.Convert());
                    return Clients.Clients(GetUsersConnectedToRoom(guid)).SendAsync("SendData", data);
                }
            }
            catch
            {

            }
            return null;
        }
        public async Task Disconnect()
        {
            Context.Abort();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string connectionID = Context.ConnectionId;
            var room = _pokerRoomService.GetRoom(connectionID);
            _pokerRoomService.RemvoeUser(connectionID);
            var data = (name: room.Name, users: room.Users, points: room.Points.Convert());
            await Clients.Clients(GetUsersConnectedToRoom(room.ChatRoomGuid.ToString())).SendAsync("SendData", data);
            await base.OnDisconnectedAsync(exception);
        }

        private List<string> GetUsersConnectedToRoom(string roomGuid)
        {
            var room = _pokerRoomService.GetRoom(new Guid(roomGuid));
            var connections = room.Users.Select(u => u.ConnectionId).ToList();
            return connections;
        }
    }
}

