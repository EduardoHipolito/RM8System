using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Framework.Business.Factory
{
    public class FactoryComponent
    {
        private static ConcurrentDictionary<string, Type> typeList = new ConcurrentDictionary<string, Type>();

        public static T GetInstance<T>() where T : class
        {
            var contractType = typeof(T);
            var contractName = contractType.FullName;

            Type currentType;

            if (typeList.ContainsKey(contractName) &&
                typeList.TryGetValue(contractName, out currentType))
            {
                return Activator.CreateInstance(currentType) as T;
            }

            var attribute = contractType.GetCustomAttribute<FactoryReferenceAttribute>();

            if (attribute == null)
            {
                throw new TypeLoadException();
            }

            var attributeTypeName = attribute.TypeName;
            currentType = Type.GetType(attributeTypeName);

            if (currentType == null)
            {
                throw new TypeLoadException();
            }

            object instance = Activator.CreateInstance(currentType);

            if (!typeList.ContainsKey(contractName))
            {
                typeList.TryAdd(contractName, currentType);
            }

            return instance as T;
        }

    }
}
