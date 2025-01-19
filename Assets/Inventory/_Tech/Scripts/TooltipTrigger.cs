using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string header;

        [TextArea] 
        [SerializeField] private string content;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!TooltipManager.instance)
                return;

            UI_Slot slot = GetComponent<UI_Slot>();

            if (slot)
            {
                Item item = slot.GetItem();

                if (!item)
                    return; // No item to show

                TooltipManager.hoveredTransform = transform;
                TooltipManager.Show(item.GetDescription(), item.GetName());
            }
            else
            {
                TooltipManager.hoveredTransform = transform;
                TooltipManager.Show(content, header);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!TooltipManager.instance)
                return;

            TooltipManager.Hide();
        }
    }
}