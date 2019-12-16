using Framework.Domain.Attributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Framework.Helpers
{
    public class FilterObjects<TEntity> where TEntity : class
    {
        private IQueryable<TEntity> myQyery;


        public FilterObjects()
        {
        }

        public IQueryable<TEntity> Filter(IQueryable<TEntity> query, JObject objT)
        {
            myQyery = query;
            each(typeof(TEntity), objT);
            return myQyery;
        }


        private void each(Type typeT, JObject objT)
        {
            var NomeCompleto = "";
            var ContadorDeChamadas = 0;
            var prop = typeT.GetProperties().Where(w => !Attribute.IsDefined(w, typeof(FilterAttribute)) || (Attribute.IsDefined(w, typeof(FilterAttribute)) && !w.GetCustomAttribute<FilterAttribute>().Ignore)).ToList();
            foreach (PropertyInfo property in prop)
            {
                string NameSpace = null;
                if (property.PropertyType.Name.ToLower().Contains("null"))
                {
                    NameSpace = Nullable.GetUnderlyingType(property.PropertyType).Namespace;
                }
                else
                {
                    NameSpace = property.PropertyType.Namespace;
                }
                if (NameSpace.Contains("Domain"))
                {
                    var name = property.Name;
                    if (name.ToLower().Contains("uf"))
                    {

                    }
                    string[] props = NomeCompleto.Split('.');
                    NomeCompleto = "";
                    foreach (var item in props.Take(ContadorDeChamadas))
                    {
                        if (!string.IsNullOrEmpty(item.Trim()))
                        {
                            if (string.IsNullOrEmpty(NomeCompleto.Trim()))
                            {
                                NomeCompleto = item;
                            }
                            else
                            {
                                NomeCompleto += "." + item;
                            }
                        }

                    }

                    if (ContadorDeChamadas == 0)
                    {
                        NomeCompleto = "";
                    }
                    int Count = ContadorDeChamadas;
                    ContadorDeChamadas++;

                    if (string.IsNullOrEmpty(NomeCompleto.Trim()))
                    {
                        NomeCompleto = name;
                    }
                    else
                    {
                        NomeCompleto += "." + name;
                    }

                    var type = property.PropertyType;
                    try
                    {
                        string value;
                        if (Attribute.IsDefined(property, typeof(FilterAttribute)))
                        {
                            value = objT.GetValue(property.GetCustomAttribute<FilterAttribute>().By).ToString();
                        }
                        else
                        {
                            value = objT.GetValue(name).ToString();
                        }
                        JObject ob = JObject.Parse(value);
                        each(type, ob);
                    }
                    catch (Exception ex)
                    {
                        NomeCompleto = "";
                        Log.Instance.ErrorLog(ex);
                    }
                    ContadorDeChamadas = Count;
                }
                else
                {

                    string type = property.PropertyType.Name;
                    if (type.ToLower().Contains("null"))
                    {
                        type = Nullable.GetUnderlyingType(property.PropertyType).Name;
                    }
                    //Pego o nome da propriedade
                    string name = property.Name;

                    //Pego o valor da propriedade
                    object value = null;
                    try
                    {
                        value = getpropertValue(objT, type, name);
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.ErrorLog(ex);
                        value = null;
                    }

                    if (value != null)
                    {
                        Expression<Func<TEntity, bool>> exp;
                        ParameterExpression parameterExp = Expression.Parameter(typeof(TEntity), "x");
                        Expression propertyExp;
                        ConstantExpression someValue;
                        if (!string.IsNullOrEmpty(NomeCompleto.Trim()))
                        {
                            NomeCompleto += "." + name;
                            string[] props = NomeCompleto.Split('.');

                            propertyExp = parameterExp;
                            Type typeR = typeof(TEntity);
                            foreach (string var in props)
                            {
                                if (!string.IsNullOrEmpty(var.Trim()))
                                {
                                    PropertyInfo pi = typeR.GetProperty(var);
                                    if (pi != null)
                                    {
                                        propertyExp = Expression.Property(propertyExp, pi);
                                        typeR = pi.PropertyType;
                                    }
                                }
                            }
                            someValue = Expression.Constant(value, property.PropertyType);
                        }
                        else
                        {
                            if (Attribute.IsDefined(property, typeof(FilterAttribute)))
                            {
                                var propertiesNames = property.GetCustomAttribute<FilterAttribute>().By.Split('.');
                                propertyExp = parameterExp;
                                Type typeR = typeof(TEntity);
                                foreach (string var in propertiesNames)
                                {
                                    if (!string.IsNullOrEmpty(var.Trim()))
                                    {
                                        PropertyInfo pi = typeR.GetProperty(var);
                                        if (pi != null)
                                        {
                                            propertyExp = Expression.Property(propertyExp, pi);
                                            typeR = pi.PropertyType;
                                        }
                                    }
                                }
                                //propertyExp = Expression.PropertyOrField(parameterExp, propertyName);
                            }
                            else
                            {
                                propertyExp = Expression.PropertyOrField(parameterExp, name);
                            }
                            someValue = Expression.Constant(value, property.PropertyType);

                        }

                        if (property.PropertyType.Name.ToLower().Equals("string"))
                        {
                            if (!string.IsNullOrEmpty(someValue.Value.ToString()))
                            {
                                PropertyInfo propertyToUse;
                                if (Attribute.IsDefined(property, typeof(FilterAttribute)))
                                {
                                    string propertyName = property.GetCustomAttribute<FilterAttribute>().By;
                                    propertyToUse = GetPropertyByFilterTtribute(typeT, propertyName);
                                }
                                else
                                {
                                    propertyToUse = property;
                                }
                                MethodInfo method = propertyToUse.PropertyType.GetMethod("Contains", new[] { property.PropertyType });
                                MethodCallExpression expression = Expression.Call(propertyExp, method, someValue);
                                exp = Expression.Lambda<Func<TEntity, bool>>(expression, parameterExp);
                            }
                            else
                            {
                                exp = null;
                            }
                        }
                        else
                        {
                            if (property.PropertyType.Name.ToLower().Equals("datetime"))
                            {
                                PropertyInfo pi = typeof(DateTime).GetProperty("Date");
                                propertyExp = Expression.Property(propertyExp, pi);
                            }
                            Expression expression = Expression.Equal(propertyExp, someValue);
                            exp = Expression.Lambda<Func<TEntity, bool>>(expression, parameterExp);
                        }
                        if (exp != null)
                        {
                            myQyery = myQyery.Where(exp);
                        }
                    }
                }

            }
        }

        private object getpropertValue(JObject objT, string type, string name)
        {
            object value;
            switch (type.ToLower())
            {
                case "string":
                    {
                        if (objT.TryGetValue(name, out var ret))
                        {
                            value = ret.ToObject<string>();
                            return value;
                        }
                        return null;
                    }
                case "int":
                    {
                        if (objT.TryGetValue(name, out var ret))
                        {
                            value = ret.ToObject<int>();
                            return value;
                        }
                        return null;
                    }
                case "int16":
                    {
                        if (objT.TryGetValue(name, out var ret))
                        {
                            value = ret.ToObject<Int16>();
                            return value;
                        }
                        return null;

                    }
                case "int32":
                    {
                        if (objT.TryGetValue(name, out var ret))
                        {
                            value = ret.ToObject<Int32>();
                            return value;
                        }
                        return null;

                    }
                case "int64":
                    {
                        if (objT.TryGetValue(name, out var ret))
                        {
                            value = ret.ToObject<Int64>();
                            return value;
                        }
                        return null;
                    }
                case "datetime":
                    {
                        if (objT.TryGetValue(name, out var ret))
                        {
                            value = DateTime.Parse(ret.ToObject<string>());
                            return value;
                        }
                        return null;
                    }
                case "decimal":
                    {
                        if (objT.TryGetValue(name, out var ret))
                        {
                            value = ret.ToObject<decimal>();
                            return value;
                        }
                        return null;
                    }
                case "double":
                    {
                        if (objT.TryGetValue(name, out var ret))
                        {
                            value = ret.ToObject<double>();
                            return value;
                        }
                        return null;
                    }
                default:
                    {
                        var erro = new StringBuilder();
                        erro.AppendLine("Propriedade com erro no filtro");
                        erro.AppendLine("Nome:" + name);
                        erro.AppendLine("Tipo:" + type);
                        Log.Instance.Function(erro);
                        return null;
                    }
            }
        }

        private PropertyInfo GetPropertyByFilterTtribute(Type typeT, string by)
        {
            PropertyInfo ret = null;
            var properties = by.Split('.');
            foreach (var property in properties)
            {
                if (ret == null)
                {
                    ret = typeT.GetProperties().FirstOrDefault(f => f.Name.Contains(property));
                }
                else
                {
                    ret = ret.GetType().GetProperties().FirstOrDefault(f => f.Name.Contains(property));
                }

            }
            return ret;
        }
    }
}
