using Framework.Business.Factory;
using Framework.Business.Interfaces;
using Framework.Business.Request;
using Framework.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Helpers
{


    public class EntityMapHelper<TTarget> : IDisposable where TTarget : EntityBase
    {
        public RequestBase requestBase;

        public List<TTarget> targetEntitiestList = new List<TTarget>();

        public List<Dictionary<int, object>> sourceEntitiestDictionary = new List<Dictionary<int, object>>();

        public List<string> targetKeyNames = new List<string>();
        public List<string> sourceKeyNames = new List<string>();
        public List<string> targetPropertyNames = new List<string>();

        public EntityMapHelper(RequestBase requestBase, IEnumerable<TTarget> targetEntities)
        {
            targetEntities = targetEntities.Where(w => w != null).ToList();
            if (targetEntities != null)
            {
                this.targetEntitiestList = (targetEntities is List<TTarget>) ? targetEntities as List<TTarget> : targetEntities.ToList();
                this.requestBase = requestBase;
            }
        }

        public EntityMapHelper(RequestBase requestBase, TTarget targetEntity)
        {
            if (targetEntity != null)
            {
                this.targetEntitiestList = new List<TTarget>() { targetEntity };
                this.requestBase = requestBase;
            }
        }

        public void Add<TEntity, TComponent>(
                            Expression<Func<TTarget, int?>> targetKeySelector,
                            Expression<Func<TTarget, TEntity>> targetMemberSelector,
                            Expression<Func<TEntity, int>> sourceKeySelectorExpression = null
                            ) where TEntity : EntityBase where TComponent : class, IBusinessBase<TEntity>
        {
            if (targetEntitiestList != null && targetEntitiestList.Any())
            {

                var targetKeyName = (targetKeySelector.Body as MemberExpression ?? ((UnaryExpression)targetKeySelector.Body).Operand as MemberExpression).Member.Name;
                string sourceKeyName;

                if (sourceKeySelectorExpression == null)
                {
                    sourceKeyName = typeof(TEntity).GetProperties().Where(x => x.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>() != null).FirstOrDefault().Name;

                    ParameterExpression sourceKeyParameterExpression = Expression.Parameter(typeof(TEntity));
                    var sourceKeyPropertyExpression = Expression.Property(sourceKeyParameterExpression, sourceKeyName);
                    sourceKeySelectorExpression = Expression.Lambda<Func<TEntity, int>>(sourceKeyPropertyExpression, sourceKeyParameterExpression);
                }
                else
                {
                    sourceKeyName = (sourceKeySelectorExpression.Body as MemberExpression).Member.Name;
                }

                Func<TEntity, int> sourceKeySelector = sourceKeySelectorExpression.Compile();
                var idList = targetEntitiestList.AsQueryable().Select(targetKeySelector).Distinct().Select(x => x.GetValueOrDefault(0)).ToList();

                var entityComponent = FactoryComponent.GetInstance<TComponent>();
                var request = CreateRequest(this.requestBase);
                request.IdList = idList;
                var response = entityComponent.GetAllByIdList(request);

                if (response != null && response.Any())
                {
                    if (targetMemberSelector.Body is MemberExpression targetExpressionMember)
                    {
                        var targetProperty = targetExpressionMember.Member as PropertyInfo;
                        var dictionary = response.ToDictionary(sourceKeySelector, x => (object)x);

                        this.sourceEntitiestDictionary.Add(dictionary);
                        this.targetKeyNames.Add(targetKeyName);
                        this.sourceKeyNames.Add(sourceKeyName);
                        this.targetPropertyNames.Add(targetExpressionMember.Member.Name);
                    }
                }


            }
        }

        private RequestByIdList CreateRequest(RequestBase request)
        {
            return new RequestByIdList() { UserId = request.UserId, IdCompany = request.IdCompany };
        }

        public List<TTarget> Map()
        {

            var i = 0;
            while (i < targetPropertyNames.Count && targetEntitiestList.Any())
            {
                var targetProperty = targetEntitiestList.FirstOrDefault().GetType().GetProperty(this.targetPropertyNames[i]);

                if (targetProperty != null)
                {
                    foreach (var item in targetEntitiestList)
                    {
                        var key = item.GetType().GetProperty(this.targetKeyNames[i]).GetValue(item);
                        if (key != null)
                        {
                            this.sourceEntitiestDictionary[i].TryGetValue((int)key, out object value);

                            targetProperty.SetValue(item, value, null);
                        }
                    }
                }

                i++;
            }
            return targetEntitiestList;

        }

        public void Dispose()
        {
            this.targetKeyNames.Clear();
            this.sourceKeyNames.Clear();
            this.targetPropertyNames.Clear();
            this.sourceEntitiestDictionary.Clear();

            this.targetKeyNames = null;
            this.sourceKeyNames = null;
            this.targetPropertyNames = null;
            this.sourceEntitiestDictionary = null;

            this.targetEntitiestList = null;
            this.requestBase = null;
        }
    }

    public class EntityMapHelper
    {
        public static List<TTarget> Map<TTarget, TEntity, TComponent>(
                            RequestBase requestBase,
                            IEnumerable<TTarget> targetEntities,
                            Expression<Func<TTarget, int?>> targetKeySelector,
                            Expression<Func<TTarget, TEntity>> targetMemberSelector,
                            Expression<Func<TEntity, int>> sourceKeySelectorExpression = null) where TTarget : TEntity where TEntity : EntityBase where TComponent : class, IBusinessBase<TEntity>
        {

            using (var mapHelper = new EntityMapHelper<TTarget>(requestBase, targetEntities))
            {
                mapHelper.Add<TEntity, TComponent>(targetKeySelector, targetMemberSelector, sourceKeySelectorExpression);
                return mapHelper.Map();
            }
        }

        public static TTarget Map<TTarget, TEntity, TComponent>(
                            IBusinessBase<TTarget> mainComponent,
                            RequestBase requestBase,
                           TTarget targetEntity,
                           Expression<Func<TTarget, int?>> targetKeySelector,
                           Expression<Func<TTarget, TEntity>> targetMemberSelector,
                           Expression<Func<TEntity, int>> sourceKeySelectorExpression = null) where TTarget : TEntity where TEntity : EntityBase where TComponent : class, IBusinessBase<TEntity>
        {

            using (var mapHelper = new EntityMapHelper<TTarget>(requestBase, targetEntity))
            {
                mapHelper.Add<TEntity, TComponent>(targetKeySelector, targetMemberSelector, sourceKeySelectorExpression);
                return mapHelper.Map().FirstOrDefault();
            }
        }
    }
}
