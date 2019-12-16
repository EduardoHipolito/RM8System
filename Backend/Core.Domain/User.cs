using Core.Domain.Enum;
using Framework.Business.Exceptions;
using Framework.Domain;
using Framework.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain
{
    public class User: EntityBase
    {
        [Required(ErrorMessage = "O login é obrigatório")]
        [MaxLength(30)]
        [MinLength(6)]
        public string Login { get; set; }

        [MaxLength(64)]
        [MinLength(6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "O perfil é obrigatório")]
        [MaxLength(2)]
        public ProfileType ProfileType { get; set; }

        public Guid TokenAlteracaoDeSenha { get; set; }
        
        [Required(ErrorMessage = "O identificador da pessoa e obrigatorio")]
        [ForeignKey(nameof(FKPerson))]
        public int IdPerson { get; set; }
        public PhysicalPerson FKPerson { get; set; }

        public ICollection<UserAplicationCompany> Permitions { get; set; }

        public void ChangePassword(string pass, string newPass, string confirmationPass)
        {
            Guard.ForNullOrEmptyDefaultMessage(pass, "Senha");
            Guard.ForNullOrEmptyDefaultMessage(newPass, "Nova senha");
            Guard.ForNullOrEmptyDefaultMessage(confirmationPass, "Confirmação de senha");
            Guard.AreEqual(newPass, confirmationPass, "As senhas não conferem!");
            ValidatePassword(pass);
            Password = newPass;
        }

        public void ChangePassword(Guid token, string newPass, string confirmacaoDeSenha)
        {
            if (!TokenAlteracaoDeSenha.Equals(token))
            {
                throw new Exception("token para alteração de senha inválido!");
            }
            Guard.ForNullOrEmptyDefaultMessage(newPass, "Nova senha");
            Guard.ForNullOrEmptyDefaultMessage(confirmacaoDeSenha, "Confirmação de senha");
            Guard.AreEqual(newPass, confirmacaoDeSenha, "As senhas não conferem!");
            Password = newPass;
            GenerateToken();
        }

        public void ValidatePassword(string senha)
        {
            Guard.ForNullOrEmptyDefaultMessage(senha, "Senha");
            var senhaCriptografada = Crypt.Encrypt(senha);
            if (!Password.Equals(senhaCriptografada))
            {
                throw new BusinessException("Senha inválida!");
            }
        }

        public Guid GenerateToken()
        {
            TokenAlteracaoDeSenha = Guid.NewGuid();
            return TokenAlteracaoDeSenha;
        }

    }
}
