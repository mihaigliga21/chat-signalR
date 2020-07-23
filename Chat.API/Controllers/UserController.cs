using AutoMapper;
using Chat.API.Model;
using Chat.API.Model.Request;
using Chat.Domain.Contracts;
using Chat.Domain.Service.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Chat.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IUserService userService
            ) : base(unitOfWork, mapper)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("auhtentificate")]
        public IActionResult Auhtentificate([FromBody] AuthentificateRequest request)
        {
            var response = _userService.AuthentificateUser(request.Email, request.Password);

            var userModel = _mapper.Map<UserModel>(response.user);
            var tokens = _mapper.Map<UserWithTokenModel>(response.tokens);
            
            var result = tokens;
            result.User = userModel;

            return base.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest model)
        {
            var user = _mapper.Map<Domain.Model.User>(model);

            var response = _userService.Register(user);

            //var result = _mapper.Map<UserModel>(response);

            return base.Created(response);
        }

        [HttpPost("search-user")]
        public IActionResult SearchUserByEmail([FromBody] SearchUserRequest request)
        {
            var response = _userService.SearchUserByEmail(request.Email, this.User?.Identity?.Name);

            var result = _mapper.Map<IList<Model.UserModel>>(response);

            return base.Ok(result);
        }
    }
}
