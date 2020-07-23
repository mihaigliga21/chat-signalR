using Chat.Domain.Contracts.Repository;
using Chat.Domain.Contracts.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Chat.Domain.Service.User
{
    public interface IUserService
    {
        (Model.User user, Model.UserTokens tokens) AuthentificateUser(string username, string password);
        Guid Register(Model.User user);
        bool IsUserValid(Guid userId);
        IList<Model.User> SearchUserByEmail(string email, string exceptRequester = "");
        Model.User GetUser(Guid userId);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISecurityService _securityService;

        public UserService(IUserRepository userRepository,
            ISecurityService securityService)
        {
            _userRepository = userRepository;
            _securityService = securityService;
        }

        public (Model.User user, Model.UserTokens tokens) AuthentificateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ValidationException("Invalid fields request");

            var user = _userRepository.GetByCondition(x => x.Email == username);
            if(user == null)
                throw new ValidationException("Unknow username");

            if(password != user.Password)
                throw new ValidationException("Invalid password");

            var tokens = _securityService.GenerateUserTokens(user);

            return (user, tokens);
        }

        public Model.User GetUser(Guid userId)
        {
            return _userRepository.GetByCondition(x => x.Id == userId);
        }

        public bool IsUserValid(Guid userId)
        {
            var user = _userRepository.GetByCondition(x => x.Id == userId);

            return user == null ? false : true;
        }

        public Guid Register(Model.User user)
        {
            _userRepository.Add(user);

            return user.Id;
        }

        public IList<Model.User> SearchUserByEmail(string email, string exceptRequester = "")
        {
            if (string.IsNullOrEmpty(email))
                return new List<Model.User>();

            IList<Model.User> users;

            if (!string.IsNullOrEmpty(exceptRequester))
            {
                users = _userRepository.Where(x => x.Email.Contains(email) && !x.Email.Contains(exceptRequester)).ToList();
            }
            else
            {
                users = _userRepository.Where(x => x.Email.Contains(email)).ToList();
            }
            

            return users;
        }
    }
}
