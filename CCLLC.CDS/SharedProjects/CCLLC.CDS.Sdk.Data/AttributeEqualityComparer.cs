using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    public class AttributeEqualityComparer : IAttributeEqualityComparer
    {
        public new bool Equals(object x, object y)
        {
            if ((x == null || (x.GetType() == typeof(string) && string.IsNullOrEmpty(x as string))) && (y == null || (y.GetType() == typeof(string) && string.IsNullOrEmpty(y as string))))
                return true;
            else
            {
                if (x == null && y == null) { return true; }
                else if (x == null && y != null) { return false; }
                else if (x != null && y == null) { return false; }
                else if (x.GetType() == y.GetType())
                {
                    if (x.GetType() == typeof(OptionSetValue)) { return ((OptionSetValue)x).Value == ((OptionSetValue)y).Value; }
                    else if (x.GetType() == typeof(BooleanManagedProperty)) { return ((BooleanManagedProperty)x).Value == ((BooleanManagedProperty)y).Value; }
                    else if (x.GetType() == typeof(EntityReference))
                    {
                        if (((EntityReference)x).LogicalName == ((EntityReference)y).LogicalName) { return ((EntityReference)x).Id == ((EntityReference)y).Id; }
                        else { return false; }
                    }
                    else if (x.GetType() == typeof(Money)) { return (((Money)x).Value == ((Money)y).Value); }
                    else if (x.GetType() == typeof(DateTime) || x.GetType() == typeof(DateTime?))
                    {
                        return Math.Abs(((DateTime)x - (DateTime)y).TotalSeconds) < 1;
                    }
                    else { return x.Equals(y); }
                }
                else { return false; }
            }
        }
        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }
}
