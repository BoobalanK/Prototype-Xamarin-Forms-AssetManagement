using AsssetManagement.Models;
using AsssetManagement.Repositories;

using System;
using System.Collections.Generic;
using System.Text;

namespace AsssetManagement.Services
{
    public interface IUserService
    {
        User GetUser(Guid id);
        bool SaveUser(User user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User GetUser(Guid id)
        {
            return _userRepository.Get(id.ToString());
        }
        public bool SaveUser(User user)
        {
            return _userRepository.Save(user);
        }
    }
}
