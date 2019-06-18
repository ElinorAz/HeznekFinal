using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class KeyValue<TKey,TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}
