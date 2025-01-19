using System.Collections;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Item tag", menuName = "Inventory/New item tag", order = 1)]
    public class ItemTag : ScriptableObject
    {
        public string GetName() => name;
    }
}