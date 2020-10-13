using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public class GInventory
    {
        public List<GameObject> items = new List<GameObject>();

        public void AddItem(GameObject item)
        {
            items.Add(item);
        }

        public GameObject FindItemWithTag(string tag)
        {
            foreach (GameObject item in items)
            {
                if (item.CompareTag(tag))
                {
                    return item;
                }
            }

            return null;
        }

        public void RemoveItem(GameObject item)
        {
            int indexToRemove = -1;
            foreach (GameObject g in items)
            {
                indexToRemove++;
                if (g == item)
                {
                    break;
                }
            }

            if (indexToRemove > -1)
            {
                items.RemoveAt(indexToRemove);
            }
        }
    }
}
