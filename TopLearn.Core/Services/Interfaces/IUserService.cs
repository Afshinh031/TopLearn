using System;
using System.Collections.Generic;
using System.Text;
using TopLearn.DataLayer.Entities.User;
using TopLearn.ViewModel.UserViewModels;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
        List<UserModel> GetAllUser(int skip, int take,int userOnline);
        List<UserInactiveViewModel> GetUsersInactive(int skip, int take);
        User GetUserById(int userId);
        int UserCount(bool isActive);
        bool UserEmialIsExist(string email);
        string GetUserFristNameById(int userId);
        string GetUserLastNameById(int userId);
        string GetUserImageById(int userId);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        bool LoginUser(LoginViewModel login);
        bool UserNameIsExist(string userName);
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
