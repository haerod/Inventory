using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Inventory
{
    // Displays / hide container's items or a part of it
    // Manages UI decisions (ex: ask for transfer an item on click)
    public class UI_Displayer : MonoBehaviour
    {
        [SerializeField] private bool hiddenByDefault = true;

        [Header("EVENTS")]
        [SerializeField] private UnityEvent OnDisplayInventory;
        [SerializeField] private UnityEvent OnHideInventory;
        [SerializeField] private UnityEvent OnFilterInventoryByTag;

        [Header("REFERENCES")]
        [SerializeField] private GameObject panelInventory;
        [SerializeField] private CanvasScaler canvasScaler;
        [SerializeField] private UI_SlotHolder slotHolder;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button sortingButton;
        [SerializeField] private Button transferButton;

        // Hidden varibles
        // ---------------

        private Container containerToDisplay;
        public enum DisplayPosition { Fullscreen, HalfRight, HalfLeft, }

        // Util variables
        // --------------

        private Container otherOpenContainer => _InventoryManager.instance.GetAnotherDisplayedContainer(this);

        // MONOBEHAVIOR
        // ============

        private void Start()
        {
            if (panelInventory.activeInHierarchy && hiddenByDefault)
                _Hide();
        }

        // GET
        // ===

        public Container GetContainer() => containerToDisplay;

        // PUBLIC
        // ======

        public void DisplayAndHide(Container container, DisplayPosition displayPosition = DisplayPosition.Fullscreen)
        {
            if (panelInventory.activeInHierarchy)
                _Hide();
            else
                Display(container, displayPosition);
        }

        public void Display(Container container, DisplayPosition displayPosition = DisplayPosition.Fullscreen)
        {
            SetContainer(container);
            SetDisplayerSize(displayPosition);
            SetTitleText(container);
            _InventoryManager.instance.AddToOpenDisplayers(this);

            if (displayPosition == DisplayPosition.Fullscreen)
                transferButton.gameObject.SetActive(false);

            else if (displayPosition == DisplayPosition.HalfLeft)
                exitButton.gameObject.SetActive(false);

            else if (displayPosition == DisplayPosition.HalfRight)
            {
                exitButton.onClick.RemoveAllListeners();
                exitButton.onClick.AddListener(_InventoryManager.instance._HideAllContainers);
                FlipImageHorizontaly(transferButton.GetComponent<RectTransform>());
            }

            panelInventory.SetActive(true);
            RefreshItemSlots();

            OnDisplayInventory.Invoke();
        }

        public void _Hide()
        {
            OnHideInventory.Invoke();
            _InventoryManager.instance.RemoveFromOpenDisplayers(this);
            //panelInventory.SetActive(false);
            Destroy(gameObject);
        }

        public void _SortItems() => containerToDisplay.OrderItemsByNameDescending();

        public void _TranferAllItems() =>
            containerToDisplay.TransferEveryItemTo(otherOpenContainer);

        public void SlotTransferItemRequest(UI_Slot slot, int itemIndex)
        {
            Container otherContainer = _InventoryManager.instance.GetAnotherDisplayedContainer(this);

            if (!otherContainer)
                return; // Nowhere to transfer item

            containerToDisplay.TransferItemTo(itemIndex, otherContainer);
        }

        public void FilterInventoryByTag(ItemTag tag)
        {
            List<Item> itemsToDisplay = containerToDisplay.GetItemsWithTag(tag);
            slotHolder.DisplayItemsOnSlots(itemsToDisplay, itemsToDisplay.Count);
            OnFilterInventoryByTag.Invoke();
        }

        // PRIVATE 
        // =======

        private void SetContainer(Container container)
        {
            containerToDisplay = container;
            container.OnContentModified.RemoveAllListeners();
            container.OnContentModified.AddListener(OnContainerModified);
        }

        private void SetDisplayerSize(DisplayPosition displayPosition)
        {
            RectTransform rt = panelInventory.GetComponent<RectTransform>();

            if (displayPosition == DisplayPosition.Fullscreen)
            {
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;
            }
            else if (displayPosition == DisplayPosition.HalfRight)
            {
                rt.offsetMin = new Vector2(
                    canvasScaler.referenceResolution.x / 2,
                    0);
                rt.offsetMax = Vector2.zero;
            }
            else if (displayPosition == DisplayPosition.HalfLeft)
            {
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = new Vector2(
                    -canvasScaler.referenceResolution.x / 2,
                    0);
            }
        }

        private void SetTitleText(Container container)
        {
            if (!titleText)
                return; // No title text on it

            titleText.text = container.GetName();
        }

        private void OnContainerModified()
        {
            RefreshItemSlots();
        }

        private void RefreshItemSlots()
        {
            slotHolder.DisplayItemsOnSlots(
                containerToDisplay.GetContainerItems(), 
                containerToDisplay.GetContainerCapacity());
        }

        private void FlipImageHorizontaly(RectTransform rectTransform) => rectTransform.localScale = new Vector3(-1, 1, 1);
    }
}