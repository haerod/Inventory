using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

namespace Inventory
{
    // Displays an item
    // Tells its holder on click on item
    public class UI_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Color colorOnHighlight = Color.black;

        [Header("EVENTS")]
        [SerializeField] private UnityEvent OnCursorEnter;
        [SerializeField] private UnityEvent OnCursorExit;

        [Header("REFERENCES")]
        [SerializeField] private Image iconImage;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private UI_SlotHolder slotHolder;

        private Color baseColor;
        private Item item;
        private float firstClickTime;

        public void OnPointerEnter(PointerEventData eventData)
        {
            ColorBackgroundImage();
            OnCursorEnter.Invoke();
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            ResetBackgroundImage();
            OnCursorExit.Invoke();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_InventoryManager.instance.doubleClickToTransferItem)
            {
                OnDoubleClick();
                return; // Simple click
            }

            if (Time.time - firstClickTime < .5f)
                OnDoubleClick();

            firstClickTime = Time.time;
        }
        private void OnDoubleClick()
        {
            if (!item)
                return; // No item to transfer

            slotHolder.SlotTransferItemRequest(this, transform.GetSiblingIndex());
        }

        public void DisplayItem(Item itemToDisplay)
        {
            RecordBackgroundBaseColor();

            if(!itemToDisplay)
            {
                iconImage.gameObject.SetActive(false);
                return; // No item to display
            }

            SetItem(itemToDisplay);
            SetBackground(itemToDisplay.GetRarity().GetSlotVisuals());
            DisplayIcon();
        }

        public void SetParamereters(UI_SlotHolder holder)
        {
            slotHolder = holder;
        }

        public Item GetItem() => item;

        private void SetItem(Item itemToDisplay) => item = itemToDisplay;
        private void SetBackground(Sprite backgroundSprite) => backgroundImage.sprite = backgroundSprite;
        private void DisplayIcon() => iconImage.sprite = item.GetIcon();
        private void RecordBackgroundBaseColor() => baseColor = backgroundImage.color;

        // BACKGROUND COLORATION
        // =====================

        private void ColorBackgroundImage() => backgroundImage.color = colorOnHighlight;
        private void ResetBackgroundImage() => backgroundImage.color = baseColor;

    }
}