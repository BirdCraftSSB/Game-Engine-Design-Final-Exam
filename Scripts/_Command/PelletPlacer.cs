using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletPlacer : MonoBehaviour
{
    static List<Transform> items;

    public static void PlacePellet(Transform item)
    {
        Transform newitem = item;
        if (items == null)
        {
            items = new List<Transform>();
        }
        items.Add(newitem);
    }

    public static void RemovePellet(Vector3 position)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].position == position)
            {
                GameObject.Destroy(items[i].gameObject);
                items.RemoveAt(i);
                break;
            }
        }
    }
}
