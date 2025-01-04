using System;
using System.Security;

namespace SmartLib
{
    /// <summary>
    /// DeepClonerExtensions类
    /// </summary>
    public static class DeepClonerExtensions
    {
        /// <summary>
        ///     Performs deep (full) copy of object and related graph
        /// </summary>
        public static T DeepClone<T>(this T obj)
        {
            return DeepClonerGenerator.CloneObject(obj);
        }

        /// <summary>
        ///     Performs deep (full) copy of object and related graph to existing object
        /// </summary>
        /// <returns>existing filled object</returns>
        /// <remarks>Method is valid only for classes, classes should be descendants in reality, not in declaration</remarks>
        public static TTo DeepCloneTo<TFrom, TTo>(this TFrom objFrom, TTo objTo) where TTo : TFrom
        {
            return (TTo)DeepClonerGenerator.CloneObjectTo(objFrom, objTo, true);
        }

        /// <summary>
        ///     Performs shallow copy of object to existing object
        /// </summary>
        /// <returns>existing filled object</returns>
        /// <remarks>Method is valid only for classes, classes should be descendants in reality, not in declaration</remarks>
        public static TTo ShallowCloneTo<TFrom, TTo>(this TFrom objFrom, TTo objTo) where TTo : class, TFrom
        {
            return (TTo)DeepClonerGenerator.CloneObjectTo(objFrom, objTo, false);
        }

        /// <summary>
        ///     Performs shallow (only new object returned, without cloning of dependencies) copy of object
        /// </summary>
        public static T ShallowClone<T>(this T obj)
        {
            return ShallowClonerGenerator.CloneObject(obj);
        }

        static DeepClonerExtensions()
        {
            if (!PermissionCheck())
                throw new SecurityException(
                    "DeepCloner should have enough permissions to run. Grant FullTrust or Reflection permission.");
        }

        private static bool PermissionCheck()
        {
            // best way to check required permission: execute something and receive exception
            // .net security policy is weird for normal usage
            try
            {
                new object().ShallowClone();
            }
            catch (VerificationException)
            {
                return false;
            }
            catch (MemberAccessException)
            {
                return false;
            }

            return true;
        }
    }

    internal static class ShallowClonerGenerator
    {
        public static T CloneObject<T>(T obj)
        {
            // this is faster than typeof(T).IsValueType
            if (obj is ValueType)
            {
                if (typeof(T) == obj.GetType())
                    return obj;

                // we're here so, we clone value type obj as object type T
                // so, we need to copy it, bcs we have a reference, not real object.
                return (T)ShallowObjectCloner.CloneObject(obj);
            }

            if (ReferenceEquals(obj, null))
                return (T)(object)null;

            if (DeepClonerSafeTypes.CanReturnSameObject(obj.GetType()))
                return obj;

            return (T)ShallowObjectCloner.CloneObject(obj);
        }
    }
}