using System.Collections;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Item rarity", menuName = "Inventory/New item rarity", order = 2)]
    public class ItemRarity : ScriptableObject
    {
        [SerializeField] private Sprite raritySlotVisuals;
        [SerializeField] private int levelOfRarity;

        public string GetName() => name;
        public int GetLevelOfRarity() => levelOfRarity;
        public Sprite GetSlotVisuals() => raritySlotVisuals;
    }
}