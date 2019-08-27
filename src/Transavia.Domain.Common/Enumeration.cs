using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Transavia.Domain.Common
{
    public abstract class Enumeration<TEnumeration, TId> : ValueObject, IComparable 
        where TEnumeration : Enumeration<TEnumeration, TId>
        where TId : IComparable
    {
        protected Enumeration(TId id, string name)
        {
            Id = id;
            Name = name;
        }

        public TId Id { get; private set; }

        public string Name { get; private set; }

        public override string ToString() => Name;

        public static IEnumerable<TEnumeration> GetAll()
        {
            var fields = typeof(TEnumeration).GetRuntimeFields();

            return fields.Select(f => f.GetValue(null)).Cast<TEnumeration>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration<TEnumeration, TId>;

            if (otherValue == null)
                return false;

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static TEnumeration FromId(TId value) 
        {
            var matchingItem = Parse<TId>(value, "value", item => item.Id.Equals(value));
            return matchingItem;
        }

        public static TEnumeration FromName(string name) 
        {
            var matchingItem = Parse<string>(name, "display name", item => item.Name == name);
            return matchingItem;
        }

        private static TEnumeration Parse<T>(T value, string description, Func<TEnumeration, bool> predicate)
        {
            var matchingItem = GetAll().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration<TEnumeration, TId>)other).Id);
    }
}