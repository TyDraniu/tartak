using System;
using System.Collections.Generic;
using System.Linq;

namespace Tartak1
{
    public static class ExtensionClass
    {
        public static List<T> CopyList<T>(this List<T> lst) where T : ICloneable
        {
            List<T> lstCopy = new List<T>();

            lstCopy.AddRange(lst.Select(i => (T)i.Clone()));

            return lstCopy;
        }
    }
}
