using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Design.Core
{
    public interface IFactory<TKey, TProvider>
    {
        TProvider GetProvider(TKey key);
    }
}
