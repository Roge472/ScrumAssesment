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

namespace InternetShop.Controllers
{
    [Route("api/[controller]")]
    public class MainController : Controller
    {
        IPokerRoomService _pokerRoomService;
        IUserRepository _userRepository;
        public MainController(IPokerRoomService pokerRoomService, IUserRepository userRepository)
        {
            _pokerRoomService = pokerRoomService;
            _userRepository = userRepository;
        }


        [HttpPost("[action]")]
        [Authorize(Roles = "User,Admin,Moderator")]
        public async Task<Guid?> AddChatRoom(string roomName)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            try
            {
                var id = Convert.ToInt32(claims.SingleOrDefault(c => c.Type == "Id").Value);
                var user = _userRepository.Get(id);
                var room=_pokerRoomService.AddRoom(roomName);
                return room.ChatRoomGuid;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        [HttpGet("[action]")]
        public async Task<string> UserName()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            try
            {
                var id = Convert.ToInt32(claims.SingleOrDefault(c => c.Type == "Id").Value);
                var user = _userRepository.Get(id);
                var name = user.Name;
                return name;
            }
            catch (Exception e)
            {
                var name = "Anonymous";
                return name;
            }
        }

    }
}
