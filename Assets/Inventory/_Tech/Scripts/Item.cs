using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/New item", order = 0)]
    public class Item : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite icon;
        [SerializeField] private ItemRarity rarity;
        [SerializeField] private List<ItemTag> tags;
        [TextArea] [SerializeField] private string description;

        public string GetName() => itemName;
        public Sprite GetIcon() => icon;
        public string GetDescription() => description;
        public List<ItemTag> GetTags() => tags;
        public ItemRarity GetRarity() => rarity;
        public bool HasTag(ItemTag tag) => tags.Contains(tag);
    }
}