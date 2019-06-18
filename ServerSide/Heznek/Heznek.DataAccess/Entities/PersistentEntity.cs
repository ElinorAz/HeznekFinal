using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class PersistentEntity<T>
    {
        public T Id { get; set; }
    }
}
