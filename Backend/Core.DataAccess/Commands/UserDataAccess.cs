using Core.Domain;
using Core.Domain.Enum;
using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DataAccess.Commands
{
    public class UserDataAccess : DataAccessBase<User, CoreContext>
    {
        public User UsrGetIdPerson(int idPerson)
        {
            var Query = Context.Users.Where(x => x.IdPerson == idPerson);

            if (Query.Any())
            {
                return Query.First();
            }
            else
            {
                return null;
            }
        }
        
        private bool LoginAlreadyRegistered(string login)
        {
            return Context.Users.Any(x => x.Login == login);
        }

        public override void Add(User obj)
        {
            if (LoginAlreadyRegistered(obj.Login))
            {
                throw new Exception("Login ja utilizado por outro user");
            }
            base.Add(obj);
        }

        public IQueryable<User> Login(string login)
        {
            return from a in Context.Users.AsNoTracking()
                   from b in Context.Person.AsNoTracking()
                   where a.IdPerson == b.Id
                   where a.Login == login || b.Email == login
                   select a;
        }

        public override IQueryable<User> GetAll(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.FKPerson).Include(i => i.FKPerson.Phones).Include(i => i.FKPerson.Documents).Include(i => i.FKPerson.Addresses);
        }

        public override IQueryable<User> GetAllGrid(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.FKPerson).Include(i => i.FKPerson.Phones).Include(i => i.FKPerson.Documents).Include(i => i.FKPerson.Addresses);
        }
    }
}
