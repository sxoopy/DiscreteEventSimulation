using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    // EventHookList 是一種自創的資料結構，因為一般的 list 不足夠使用
    public class EventHookList<T> : List<T>
    {
        public event EventHandler ItemAdded;
        public new void Add(T item)
        {
            base.Add(item);
            if (ItemAdded != null)
                ItemAdded(item, null);
        }
    }
}
