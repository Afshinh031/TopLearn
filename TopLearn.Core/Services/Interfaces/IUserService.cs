using System;
using System.Collections.Generic;
using System.Text;
using TopLearn.DataLayer.Entities.User;
using TopLearn.ViewModel.UserViewModels;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int userId);
        bool EmialIsExist(string email);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        bool LoginUser(LoginViewModel login);
        void DisposeUser();
    }

    public interface IUserRepository
    {
        bool InsertUser(User user);

        bool UpdateUser(User user);

        bool DeleteUser(User user);


        void SaveUser();

        void DisposeUser();

    }
}
