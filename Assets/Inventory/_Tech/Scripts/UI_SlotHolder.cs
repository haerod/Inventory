using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    // Displays an item list on slots
    // Manages slots creation / destruction
    // Doesnt't take system decisions (asks the displayer)
    public class UI_SlotHolder : MonoBehaviour
    {
        [Header("REFERENCES")]
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private UI_Displayer displayer;

        public void DisplayItemsOnSlots(List<Item> items, int slotCountToDisplay)
        {
            ClearAllSlots();
            CreateItemSlots(items, slotCountToDisplay);
        }

        public void SlotTransferItemRequest(UI_Slot slot, int itemIndex) => displayer.SlotTransferItemRequest(slot, itemIndex);

        private void ClearAllSlots() => GetAllSlots().ForEach(s => Destroy(s.gameObject));
        private List<Transform> GetAllSlots() => transform.Cast<Transform>().ToList();

        private void CreateItemSlots(List<Item> items, int slotCountToDisplay)
        {
            for (int i = 0; i < slotCountToDisplay; i++)
            {
                UI_Slot instaSlot;
                instaSlot = Instantiate(slotPrefab, transform).GetComponent<UI_Slot>();
                instaSlot.SetParamereters(this);

                if (i < items.Count)
                    instaSlot.DisplayItem(items[i]);
                else
                    instaSlot.DisplayItem(null);
            }
        }
    }
}