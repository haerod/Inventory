using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class InventoryInputs : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField] private KeyCode displayAndHideInventoryKey = KeyCode.I;
        [SerializeField] private KeyCode hideInventoryKey = KeyCode.Escape;
        [Space]
        [SerializeField] private bool holdKeyToDisplay = true;
        [SerializeField] private KeyCode displayInventoryOnHoldKey = KeyCode.Tab;

        [Header("EVENTS")]
        public UnityEvent OnDisplayAndHideInventoryPressed;
        public UnityEvent OnDisplayAndHideInventoryReleased;
        public UnityEvent OnHideInventoryPressed;

        private void Update()
        {
            if (Input.GetKeyDown(displayAndHideInventoryKey))
                OnDisplayAndHideInventoryPressed.Invoke();            

            if (Input.GetKeyDown(hideInventoryKey))
                OnHideInventoryPressed.Invoke();                       

            if (!holdKeyToDisplay)
                return; // EXIT : don't want hold key to display

            if (Input.GetKeyDown(displayInventoryOnHoldKey))
                OnDisplayAndHideInventoryPressed.Invoke();

            if (Input.GetKeyUp(displayInventoryOnHoldKey))
                OnHideInventoryPressed.Invoke();
        }
    }
}
