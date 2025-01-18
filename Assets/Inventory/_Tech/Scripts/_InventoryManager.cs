using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class _InventoryManager : MonoBehaviour
    {
        [Header("RULES")]

        public bool doubleClickToTransferItem = false;

        [Header("REFERENCES")]

        [SerializeField] private GameObject canvasDisplayerPrefab;

        // Hidden variables
        // ----------------

        public static _InventoryManager instance;
        private List<UI_Displayer> openDisplayers = new List<UI_Displayer>();

        // MONOBEHAVIOR
        // ============

        private void Awake()
        {
            if (!instance)
                instance = this;
        }

        // UTILS
        // =====

        public void _DisplayContainerFullScreen(Container container)
        {
            if (GetDisplayerOf(container))
                return; // Container already open

            InstantiateDisplayer(container, UI_Displayer.DisplayPosition.Fullscreen);
        }

        public void _HideContainer(Container container)
        {
            UI_Displayer containerDisplayer = GetDisplayerOf(container);

            if (!containerDisplayer)
                return; // No container to close

            containerDisplayer._Hide();
        }

        public void _DisplayAndHideContainerFullScreen(Container container)
        {
            if (GetDisplayerOf(container))
                _HideContainer(container);
            else
                _DisplayContainerFullScreen(container);
        }

        public void _DisplayTwoContainers(Container container1, Container container2)
        {
            if (GetDisplayerOf(container1))
                return; // Container already open
            if (GetDisplayerOf(container2))
                return; // Container already open

            InstantiateDisplayer(container1, UI_Displayer.DisplayPosition.HalfLeft);
            InstantiateDisplayer(container2, UI_Displayer.DisplayPosition.HalfRight);
        }

        public void _HideAllContainers() => openDisplayers
                .ToList()
                .ForEach(d => d._Hide());

        // PUBLIC
        // ======

        public void AddToOpenDisplayers(UI_Displayer displayerToAdd)
        {
            if (openDisplayers.Contains(displayerToAdd))
                return; // Already in the list

            openDisplayers.Add(displayerToAdd);
        }

        public void RemoveFromOpenDisplayers(UI_Displayer displayerToRemove) => openDisplayers.Remove(displayerToRemove);

        public Container GetAnotherDisplayedContainer(UI_Displayer currentDisplayer) =>
            openDisplayers
                .Where(d => d != currentDisplayer)
                .FirstOrDefault()
                ?.GetContainer();

        // PRIVATE
        // =======

        private UI_Displayer GetDisplayerOf(Container container) => openDisplayers
                .Where(d => d.GetContainer() == container)
                .FirstOrDefault();

        private UI_Displayer InstantiateDisplayer(Container container, UI_Displayer.DisplayPosition displayPosition)
        {
            UI_Displayer displayer = Instantiate(canvasDisplayerPrefab).GetComponent<UI_Displayer>();
            displayer.Display(container, displayPosition);
            return displayer;
        }
    }
}