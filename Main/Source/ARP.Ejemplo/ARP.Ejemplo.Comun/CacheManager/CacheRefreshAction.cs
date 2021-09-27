using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace ARP.Ejemplo.Comun.CacheManager
{
    internal class CacheRefreshAction : ICacheItemRefreshAction
    {
        #region Methods (1)

        // Public Methods (1) 

        public void Refresh(string pKey, object pExpiredValue, CacheItemRemovedReason pRemovalReason)
        {
            // Item has been removed from cache. Perform desired actions here, based upon
            // the removal reason (e.g. refresh the cache with the item).
        }

        #endregion Methods
    }
}