using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Test : MonoBehaviour
    {
        public Container container1, container2;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                _InventoryManager.instance._DisplayTwoContainers(container1,container2);
        }
    } 
}
