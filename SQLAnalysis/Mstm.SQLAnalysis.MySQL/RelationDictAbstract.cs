using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MySQL
{
    internal abstract class RelationDictAbstract<TKey>
    {
        private static Dictionary<TKey, string> _dict;

        public RelationDictAbstract()
        {
            _dict = InitDict();
        }

        /// <summary>
        /// 初始化字典数据
        /// </summary>
        /// <returns></returns>
        protected abstract Dictionary<TKey, string> InitDict();


        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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
