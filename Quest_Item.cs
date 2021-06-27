using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Item : MonoBehaviour
{
    [SerializeField]
    Item item;
    [SerializeField]
    int count;

    public Quest_Item(Item item, int count = 1)
    {
        this.item = item;
        this.count = count;
    }

    public Item Item { get => item; set => item = value; }
    public int Count { get => count; set => count = value; }
}
