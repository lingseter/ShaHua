using System;
using System.Reflection;

namespace Utility
{
    public class Common
    {
        public static D Mapper<D, S>(S s)
        {
            D d = Activator.CreateInstance<D>();
            try
            {
                var sType = s.GetType();
                var dType = typeof(D);
                foreach (PropertyInfo sP in sType.GetProperties())
                {
                    foreach (PropertyInfo dP in dType.GetProperties())
                    {
                        if (dP.Name == sP.Name)
                        {
                            dP.SetValue(d, sP.GetValue(s));
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("mapper object fail .", ex);
            }
            return d;
        }
    }
}
