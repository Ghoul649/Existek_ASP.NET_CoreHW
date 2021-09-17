using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App12.Authentification
{
    public class UserUpdateManager<TKey>
    {
        protected SortedSet<TKey> _toUpdate = new SortedSet<TKey>();
        public virtual void Changed(TKey key)
        {
            _toUpdate.Add(key);
        }

        public virtual bool NeedsToBeUpdated(TKey key)
        {
            return _toUpdate.Contains(key);
        }

        public virtual void Update(TKey key)
        {
            _toUpdate.Remove(key);
        }
    }
}
