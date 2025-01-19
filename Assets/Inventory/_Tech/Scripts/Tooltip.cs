using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Inventory
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private int tooltipOffset = 50;

        [Header("REFERENCES")]

        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private TextMeshProUGUI contentText;
        [SerializeField] private LayoutElement layoutElement;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private int characterWrapLimit = 80;

        public void SetText(string content, string header = "")
        {
            WriteText(content, header);
            SetSizeFitterWrapLimit();
            SetPositionAndPivot();
        }

        private void WriteText(string content, string header)
        {
            if (string.IsNullOrEmpty(header))
                headerText.gameObject.SetActive(false);
            else
            {
                headerText.gameObject.SetActive(true);
                headerText.text = header;
            }

            contentText.text = content;
        }
        private void SetSizeFitterWrapLimit()
        {
            int headerLength = headerText.text.Length;
            int contentLength = contentText.text.Length;

            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }
        private void SetPositionAndPivot()
        {
            Transform hoveredTransform = TooltipManager.hoveredTransform;
            Vector2 hoveredPosition = hoveredTransform.position;
            Vector2 offset = Vector2.one; 

            if (hoveredPosition.x > Screen.width / 2)
            {
                rectTransform.pivot = new Vector2(1, rectTransform.pivot.y);
                offset.x = -1;
            }
            else
            {
                rectTransform.pivot = new Vector2(0, rectTransform.pivot.y);
                offset.x = 1;
            }

            if (hoveredPosition.y > Screen.height / 2)
            {
                rectTransform.pivot = new Vector2(rectTransform.pivot.x, 1);
                offset.y = -1;
            }
            else
            {
                rectTransform.pivot = new Vector2(rectTransform.pivot.x, 0);
                offset.y = 1;
            }

            transform.position = hoveredPosition + offset * tooltipOffset;
        }
    }
}
