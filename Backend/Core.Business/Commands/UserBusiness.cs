using Core.Business.Contracts;
using Core.Business.Entities;
using Core.DataAccess;
using Core.DataAccess.Commands;
using Core.Domain;
using Core.Domain.Enum;
using Framework.Business;
using Framework.Business.Exceptions;
using Framework.Business.Factory;
using Framework.Business.Request;
using Framework.DataAccess;
using Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Business.Commands
{
    public class UserBusiness : BusinessBase<User, UserDataAccess>, IUserBusiness
    {
        public override bool IsFullUser(int UserId)
        {
            return this.GetById(UserId).ProfileType == Domain.Enum.ProfileType.Developer;
        }

        public override bool ValidatePermition(int UserId, int IdCompany)
        {
            return FactoryComponent.GetInstance<IUserAplicationCompanyBusiness>().ValidatePermition(UserId, IdCompany, AplicationsCodes.ModuleCode, AplicationsCodes.User);
        }

        public bool ChangePassword(RequestBase<UserChangePassword> request)
        {
            var users = _dataAccess.Login(request.Parameter.Login);
            User user = users.First();
            if (user != null)
            {
                user.ChangePassword(request.Parameter.Password, request.Parameter.NewPassword, request.Parameter.ConfirmationPassword);
                _dataAccess.Update(user);
                _dataAccess.Commit();
                return true;
            }
            else
            {
                throw new BusinessException("Login não cadastrado.");
            }
        }

        public bool ChangePasswordByToken(RequestBase<UserChangePassword> request)
        {
            var users = _dataAccess.Login(request.Parameter.Login);
            User user = users.First();
            if (user != null)
            {
                user.ChangePassword(request.Parameter.Token, request.Parameter.NewPassword, request.Parameter.ConfirmationPassword);
                _dataAccess.Update(user);
                _dataAccess.Commit();
                return true;
            }
            else
            {
                throw new BusinessException("Login não cadastrado.");
            }

        }

        public User GetById(int id)
        {
            var user = _dataAccess.Get(id);
            if (user == null)
            {
                throw new BusinessException("Não existe usuario com esse login");
            }
            return user;
        }

        public User Get(RequestBase<string> request)
        {
            var users = _dataAccess.Login(request.Parameter);
            User user = users.First();
            if (user == null)
            {
                throw new BusinessException("Não existe usuario com esse login");
            }
            return user;
        }

        public User GetByIdPerson(RequestBase<int> request)
        {
            var user = _dataAccess.UsrGetIdPerson(request.Parameter);
            if (user == null)
            {
                throw new BusinessException("Usuario não encontrado");
            }
            return user;
        }

        public Guid GetNewPassHash(RequestBase<string> request)
        {
            var users = _dataAccess.Login(request.Parameter);
            Guid Token;
            string email;
            if (users.Any())
            {
                User user = users.First();
                Token = user.GenerateToken();
                email = user.FKPerson.Email;
                _dataAccess.Update(user);
                _dataAccess.Commit();
            }
            else
            {
                throw new BusinessException("Login não cadastrado.");
            }
            var MyMail = new SendEmail
            {
                Assunto = "Token de alteração de senha.",
                Corpo = new NewPassEmailTemplate().Template.Replace("ReplacePerToken", Token.ToString()).Replace("ReplacePerEmail", request.Parameter),
                Destinatarios = new List<string>()
            };
            MyMail.Destinatarios.Add(email);
            MyMail.Envia();
            return Token;
        }

        public ProfileType GetProfile(RequestBase<int> request)
        {
            return _dataAccess.Find(x => x.Id == request.Parameter).FirstOrDefault().ProfileType;
        }

        public bool Login(ref int? IdUser, string login, string pass)
        {
            var users = _dataAccess.Login(login);

            if (users.Any())
            {
                User user = users.FirstOrDefault();
                user.ValidatePassword(pass);
                IdUser = user.Id;
                return true;
            }
            else
            {
                throw new BusinessException("Login não cadastrado.");
            }
        }

        public void LoginAlreadyRegistered(RequestBase<string> request)
        {
            if (_dataAccess.GetAll().Any(x => x.Login == request.Parameter))
            {
                throw new BusinessException("Login ja cadastrado");
            }
        }

        public User GetByLogin(RequestBase<string> request)
        {
            var user = this._dataAccess.GetAll(IsFullUser(request.UserId ) ? 0 : request.IdCompany ).FirstOrDefault(w => w.Login == request.Parameter);
            if (user == null)
            {
                throw new BusinessException("Usuario não encontrado");
            }
            return user;
        }

        public override bool Add(RequestBase<User> request)
        {
            request.Parameter.Password = Crypt.Encrypt(request.Parameter.Password);
            return base.Add(request);
        }

    }
}