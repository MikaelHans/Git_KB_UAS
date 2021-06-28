using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Objective_Item : Quest_Objective
{
    //[SerializeField]
    //Item item;


    //public Quest_Item(Item item, int count = 1)
    //{
    //    this.item = item;
    //    this.count = count;
    //}

    //public Item Item { get => item; set => item = value; }
    //

    public override int completed()
    {
        Player player = FindObjectOfType<Player>();
        return player.get_inventory().search_item(_objective.GetComponent<Item>());
    }
}

