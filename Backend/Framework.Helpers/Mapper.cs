using Framework.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Helpers
{
    public class Mapper
    {
        private static Mapper _instance;

        private Mapper()
        {
        }

        public static Mapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Mapper();
                }

                return _instance;
            }
        }

        public TEntity Map<TEntity>(JObject obj) where TEntity : class
        {
            var entityType = typeof(TEntity);
            var entity = Activator.CreateInstance(entityType) as TEntity;
            if (obj != null)
            {
                var properties = entity.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (obj.TryGetValue(property.Name, out var value))
                    {
                        property.SetValue(this, obj[property.Name], null);
                    }
                }
            }
            return entity;
        }


    }
}
