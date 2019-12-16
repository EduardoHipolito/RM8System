using Stock.Business.Contracts;
using Stock.DataAccess.Commands;
using Framework.Business;
using Framework.Business.Factory;
using Core.Business.Contracts;
using Stock.Domain;
using System.Collections.Generic;
using Framework.Business.Request;
using System.Linq;
using Stock.Domain.Enum;
using Framework.Business.Exceptions;

namespace Stock.Business.Commands
{
    public class ProductionBusiness : BusinessBase<Production, ProductionDataAccess>, IProductionBusiness
    {
        public override bool Add(RequestBase<Production> request)
        {
            ValidateProduction(request.Parameter);

            return base.Add(request);
        }

        public override bool Update(RequestBase<Production> request)
        {
            ValidateProduction(request.Parameter);

            return base.Update(request);

        }

        private void ValidateProduction(Production production)
        {
            if (production.FKFinalProduct.ProductType != ProductType.OwnProduction)
                throw new BusinessException("O produto final deve ser do tipo Produção Própria");

            if (production.ProductionStages == null || production.ProductionStages.Count() == 0)
                throw new BusinessException("A produção deve ter no minimo um estágio");

            if (!production.ProductionStages.SelectMany(s => s.ProductionStageRawMaterials).All(a => a.FKProduct.ProductType == ProductType.RawMaterial))
                throw new BusinessException("Todos os produtos usados como máteria prima devem ser do tipo Matéria Prima");

            if (production.ProductionStages.Select(s => s.FKSupplier).All(a => a.SupplierType == SupplierType.Labor))
                throw new BusinessException("Todos os fornecedores devem ser do tipo Mão de Obra");
        }

        public override bool IsFullUser(int UserId)
        {
            return FactoryComponent.GetInstance<IUserBusiness>().GetById(UserId).ProfileType == Core.Domain.Enum.ProfileType.Developer;
        }

        public override bool ValidatePermition(int UserId, int IdCompany)
        {
            return FactoryComponent.GetInstance<IUserAplicationCompanyBusiness>().ValidatePermition(UserId, IdCompany, AplicationsCodes.ModuleCode, AplicationsCodes.Sale);
        }
    }
}