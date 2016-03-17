using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MySQL
{
    internal abstract class WhereRelationDictAbstract<TKey>
    {
        private static Dictionary<TKey, string> _dict;

        public WhereRelationDictAbstract()
        {
            _dict = InitDict();
        }


        protected abstract Dictionary<TKey, string> InitDict();


        public string this[TKey index]
        {
            get
            {
                if (_dict == null || _dict.ContainsKey(index) == false)
                {
                    throw new Exception("不支持的筛选条件！" + index.ToString());
                }
                return _dict[index];
            }
        }


    }
}
