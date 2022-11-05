
using Microsoft.Extensions.Logging;
using NLog;
using ScheduleControl.Business.Abstract;
using ScheduleControl.Business.Abstract.Auth;
using ScheduleControl.Entities.Dtos.Account;
using ScheduleControl.Entities.Models;
using System;

namespace ScheduleControl.Business.Concrete.Managers.Auth
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        // private Logger _loogger = LogManager.GetCurrentClassLogger();
        private readonly ILogger<AuthManager> _logger;

        public AuthManager(ILogger<AuthManager> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;

        }

        public User Login(UserForLoginDto userForLoginDto)
        {
            try
            {
                var userToCheck = _userService.Logining(userForLoginDto.Email, userForLoginDto.Password);
                if (userToCheck == null)
                {
                    _logger.LogError("Giriş başarısız.");
                    //throw new Exception("hata metni");

                }
                return userToCheck;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
                //throw new Exception("hata metni");
            }
        }

        public User Register(UserForRegisterDto userForRegisterDto)
        {
            try
            {
                var userToCheck = UserExists(userForRegisterDto.Email);
                if (userToCheck)
                {
                    var user = new User
                    {
                        Email = userForRegisterDto.Email,
                        FirstName = userForRegisterDto.FirstName,
                        LastName = userForRegisterDto.LastName,
                        Password = userForRegisterDto.Password,
                        Status = false,
                        UserGuid = Guid.NewGuid(),
                        IsActivatedMailSend = false
                    };
                    _userService.Add(user);
                    return user;
                }

                else
                {
                    _logger.LogError("Bu mail adresi ile kullanıcı kayıtlı  !!!");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
                //throw new Exception("hata metni");
            }

            //throw new Exception("Bu mail adresi ile kullanıcı kayıtlı  !!!");
            //logger.Error("Bu mail adresi ile kullanıcı kayıtlı  !!!");
        }

        public bool UserActivatedRegister(string userMailUrl)
        {
            try
            {
                Guid userUniqNumber = Guid.Parse(userMailUrl.Split("*")[1]);
                var userInfo = _userService.UserGetByUniqNumber(userUniqNumber);
                if (userInfo != null)
                {
                    userInfo.IsActivatedMailSend = true;
                    userInfo.Status = true;
                    _userService.Update(userInfo);
                    return true;
                }
                else
                {
                    _logger.LogError("Hata mesajı");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
                //throw new Exception("hata metni");
            }
        }

        public bool UserExists(string email)
        {
            try
            {
                if (_userService.GetByMail(email) != null)
                {
                    _logger.LogError("Mail gönderilemedi.");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
                //throw new Exception("hata metni");
            }
        }
    }
}