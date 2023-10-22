using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu()]
    public class Inventory_Consumable : Inventory_Item
    {
        public bool Use()
        {
            //do something

            return true;
        }
    }
}