using System;
using System.Collections.Generic;
using System.Linq;

namespace Tartak1
{
    public static class ExtensionClass
    {
        public static List<T> CopyList<T>(this List<T> lst)
        {
            List<T> lstCopy = new List<T>();
            foreach (ICloneable item in lst.OfType<ICloneable>())
            {
                lstCopy.Add((T)item.Clone());
            }

            return lstCopy;
        }
    }
}
