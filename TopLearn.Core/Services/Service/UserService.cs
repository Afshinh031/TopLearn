using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.User;
using TopLearn.Utility.Hasher;
using TopLearn.ViewModel.UserViewModels;

namespace TopLearn.Core.Services.Service
{
    public class UserService : IUserService
    {
        private TopLearnContext _context;

        public UserService(TopLearnContext context)
        {
            _context = context;
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Single(u => u.UserID == userId);
        }

        public bool EmialIsExist(string email)
        {
            return _context.Users.Any(u => u.UserEmail == email);
        }

        public void DisposeUser()
        {
            _context.Dispose();
        }

        public bool LoginUser(LoginViewModel login)
        {
            return _context.Users.Any(u => u.UserEmail == login.UserEmail && u.UserPassword==login.UserPassword.ToEncodePasswordMd5() && u.UserIsActive==true);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.UserEmail == email);
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u => u.UserEmailConfigurationCode == activeCode);
        }

        public string GetUserFristNameById(int userId)
        {
           return _context.Users.Single(u => u.UserID == userId).UserFristName;
        }

        public string GetUserLastNameById(int userId)
        {
            return _context.Users.Single(u => u.UserID == userId).UserLastName;
        }

        public string GetUserImageById(int userId)
        {
            return _context.Users.Single(u => u.UserID == userId).UserImage;
        }
    }


    public class UserRepository : IUserRepository
    {
        private TopLearnContext _context;

        public UserRepository(TopLearnContext context)
        {
            _context = context;
        }
        public bool InsertUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool UpdateUser(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteUser(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Deleted;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void SaveUser()
        {
            _context.SaveChanges();
        }

        public void DisposeUser()
        {
            _context.Dispose();
        }
    }
}
