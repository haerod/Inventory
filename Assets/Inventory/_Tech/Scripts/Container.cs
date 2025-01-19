using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class Container : MonoBehaviour
    {
        [SerializeField] private string containerName = "Inventory";
        [SerializeField] private bool getNameOfTheObject = false;
        [SerializeField] private int capacity = 5;
        [SerializeField] private List<Item> items;

        public UnityEvent OnContentModified;

        // MONOBEHAVIOR
        // ============

        private void Start()
        {
            RemoveExcedentItems();
        }

        // GET / SET
        // =========

        public List<Item> GetContainerItems() => items;
        public int GetContainerCapacity() => capacity;
        public string GetName() => getNameOfTheObject ? name : containerName;

        // ADD AND REMOVE ITEMS
        // ====================

        // Public
        // ------

        public void TransferItemTo(int itemIndex, Container destinationContainer)
        {
            Item item = items[itemIndex];

            if (destinationContainer.TryAddItem(item))
                RemoveItem(itemIndex);
        }

        public void TransferEveryItemTo(Container destinationContainer)
        {
            List<Item> toTransfer = items.ToList();
            Item itemToTransfer;

            for (int i = 0; i < toTransfer.Count; i++)
            {
                itemToTransfer = items.LastOrDefault();

                if (destinationContainer.TryAddItem(itemToTransfer))
                    RemoveLastItem();
                else
                    break;
            }
        }

        public void RemoveAllItemsLike(Item item)
        {
            if (!items.Contains(item))
                return; // No item like this to remove;

            items = items
                .Where(i => i != item)
                .ToList();

            OnContentModified.Invoke();
        }

        public void RemoveEverything()
        {
            items.Clear();
            OnContentModified.Invoke();
        }

        // Private
        // -------

        private bool TryAddItem(Item item)
        {
            if (!item)
                return false; // Item is null
            if (items.Count >= capacity)
                return false; // Maximum capacity reached

            items.Add(item);
            OnContentModified.Invoke();
            return true;
        }

        private void RemoveItem(int itemIndex)
        {
            items.RemoveAt(itemIndex);
            OnContentModified.Invoke();
        }

        private void RemoveLastItem() => RemoveItem(items.Count - 1);

        // ORDER / SORT ITEMS
        // ==================

        public void OrderItemsByName()
        {
            items = items
                .OrderBy(i => i.GetName())
                .ToList();

            OnContentModified.Invoke();
        }
        public void OrderItemsByNameDescending()
        {
            items = items
                .OrderByDescending(i => i.GetName())
                .ToList();

            OnContentModified.Invoke();
        }
        public void OrderItemByTag()
        {
            items = items
                .OrderBy(i => i.GetTags().FirstOrDefault()?.GetName())
                .ToList();

            OnContentModified.Invoke();
        }        
        public void OrderItemByTagDescending()
        {
            items = items
                .OrderByDescending(i => i.GetTags().FirstOrDefault()? .GetName())
                .ToList();

            OnContentModified.Invoke();
        }
        public void OrderItemByRarity()
        {
            items = items
                .OrderBy(i => i.GetRarity().GetLevelOfRarity())
                .ThenByDescending(i => i.GetName())
                .ToList();

            OnContentModified.Invoke();
        }
        // ITEM TAGS
        // =========

        public List<Item> GetItemsWithTag(ItemTag tag) => items.Where(i => i.GetTags().Contains(tag)).ToList();

        public List<ItemTag> GetAllItemTagsInContainer() =>
            items
                .SelectMany(i => i.GetTags())
                .Distinct()
                .ToList();

        // CAPACITY
        // ========

        private void RemoveExcedentItems()
        {
            if (items.Count > capacity)
                items = items
                    .Take(capacity)
                    .ToList();
        }
    }
}