using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager instance;

        public static Transform hoveredTransform;

        [SerializeField] private Tooltip tooltip;

        private void Awake()
        {
            if (!instance)
                instance = this;
        }

        public static void Show(string content, string header = "")
        {
            instance.tooltip.SetText(content, header);
            instance.tooltip.gameObject.SetActive(true);
        }

        public static void Hide() => instance.tooltip.gameObject.SetActive(false);
    }
}
