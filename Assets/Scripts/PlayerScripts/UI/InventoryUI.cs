using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Image[] itemSlots; 
        [SerializeField] private Color selectedColor = Color.yellow;
        [SerializeField] private Color defaultColor = Color.white;

        public void UpdateInventory(List<Sprite> icons, int selectedIndex)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (i < icons.Count)
                {
                    itemSlots[i].sprite = icons[i];
                    itemSlots[i].color = i == selectedIndex ? selectedColor : defaultColor;
                }
                else
                {
                    itemSlots[i].sprite = null;
                    itemSlots[i].color = new Color(1, 1, 1, 0);
                }
            }
        }
    }
}
