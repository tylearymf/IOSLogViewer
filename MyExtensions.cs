using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSLogViewer
{
    static class MyExtensions
    {
        /// <summary>
        /// 以指定字符切割字符串，并去重 且转小写
        /// </summary>
        static public List<string> Distinct(this string pStr, char pChar)
        {
            if (string.IsNullOrEmpty(pStr)) return null;
            var tHashSet = new HashSet<string>();
            var tSplits = pStr.Split(new char[] { pChar }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in tSplits)
            {
                tHashSet.Add(item.ToLower());
            }
            return tHashSet.ToList();
        }
    }
}
