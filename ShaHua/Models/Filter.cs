using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ShaHua.Models
{
    public class Filter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Start { get; set; }
        public int PageLimit { get; set; }

        public string GetFilter()
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = GetType().GetProperties();
            foreach (var p in properties)
            {
                if (p.GetValue(this) != null && !p.Name.Equals("Start") && !p.Name.Equals("PageLimit"))
                    sb.Append(p.Name + "=" + p.GetValue(this));
            }
            return sb.ToString();
        }
    }
}