using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/New item", order = 1)]
    public class Item : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite icon;
        [SerializeField] private List<ItemTag> tags;
        [TextArea] [SerializeField] private string description;

        public string GetName() => itemName;
        public Sprite GetIcon() => icon;
        public string GetDescription() => description;
        public List<ItemTag> GetTags() => tags;
        public bool HasTag(ItemTag tag) => tags.Contains(tag);
    }
}