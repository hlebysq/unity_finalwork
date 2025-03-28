using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using ObjectScripts;
using UI;

namespace PlayerScripts
{
    public class PlayerInventory : NetworkBehaviour
    {
        [SerializeField] private int inventorySize = 3;
        [SerializeField] private InventoryUI inventoryUI;

        private List<IGrabbable> _inventory = new();
        private List<GameObject> _inventoryObjects = new();
        private List<Sprite> _inventoryIcons = new();
        private int _selectedItemIndex = -1;
        private void Start()
        {
            inventoryUI.UpdateInventory(_inventoryIcons, _selectedItemIndex);
        }
        public bool AddItem(IGrabbable item)
        {
            if (_inventory.Count >= inventorySize)
            {
                Debug.Log("Inventory full");
                return false;
            }

            _inventory.Add(item);
            _inventoryObjects.Add(((MonoBehaviour)item).gameObject);
            _inventoryIcons.Add(item.Icon);
            ((MonoBehaviour)item).gameObject.SetActive(false);

            if (_selectedItemIndex == -1)
            {
                _selectedItemIndex = 0;
            }

            inventoryUI.UpdateInventory(_inventoryIcons, _selectedItemIndex);
            return true;
        }

        public void DropSelectedItem()
        {
            if (_selectedItemIndex == -1 || _inventory.Count == 0) return;

            GameObject objToDrop = _inventoryObjects[_selectedItemIndex];

            _inventory.RemoveAt(_selectedItemIndex);
            _inventoryObjects.RemoveAt(_selectedItemIndex);
            _inventoryIcons.RemoveAt(_selectedItemIndex);

            objToDrop.SetActive(true);
            objToDrop.transform.position = transform.position + transform.forward;
            objToDrop.GetComponent<IGrabbable>().Drop();

            _selectedItemIndex = _inventory.Count > 0 ? Mathf.Clamp(_selectedItemIndex, 0, _inventory.Count - 1) : -1;
            inventoryUI.UpdateInventory(_inventoryIcons, _selectedItemIndex);
        }

        public void SwitchItem()
        {
            if (_inventory.Count == 0) return;
            _selectedItemIndex = (_selectedItemIndex + 1) % _inventory.Count;
            inventoryUI.UpdateInventory(_inventoryIcons, _selectedItemIndex);
        }

        public IGrabbable GetSelectedItem()
        {
            if (_selectedItemIndex == -1 || _inventory.Count == 0) return null;
            return _inventory[_selectedItemIndex];
        }
    }
}
