using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Dropper : Game_Character
{
    //public Item item_to_drop;
    public Item item_to_drop;
  
    public virtual void drop_item()
    {
        Instantiate(item_to_drop, transform.position, transform.rotation);
    }
}
