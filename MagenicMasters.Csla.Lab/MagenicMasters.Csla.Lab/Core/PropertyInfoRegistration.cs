using Csla;
using Csla.Core;
using Csla.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Core
{
    /// <summary>
    /// Defines a helper method to register managed backing fields 
    /// on the correct class.
    /// </summary>
    /// <remarks>
    /// This is based on:
    /// http://dotnetbyexample.blogspot.com/2010/08/fixing-clsa-property-registration.html
    /// </remarks>
    public sealed class PropertyInfoRegistration : BusinessBase
    {
        private PropertyInfoRegistration()
            : base() { }

        public static PropertyInfo<T> Register<TTarget, T>(
            Expression<Func<TTarget, object>> propertyLambdaExpression)
        {
            var property = new PropertyInfo<T>(
                Reflect<TTarget>.GetProperty(propertyLambdaExpression).Name);
            return BusinessBase.RegisterProperty<T>(typeof(TTarget), property);
        }

        public static PropertyInfo<T> Register<TTarget, T>(
            Expression<Func<TTarget, object>> propertyLambdaExpression, RelationshipTypes relationship)
        {
            var property = new PropertyInfo<T>(
                Reflect<TTarget>.GetProperty(propertyLambdaExpression).Name, relationship);
            return BusinessBase.RegisterProperty<T>(typeof(TTarget), property);
        }

        public static PropertyInfo<T> Register<T>(Type targetType, string name)
        {
            var property = new PropertyInfo<T>(name);
            return BusinessBase.RegisterProperty<T>(targetType, property);
        }
    }
}
