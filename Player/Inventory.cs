using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    Slot[] inventory_slots;
    [SerializeField]
    int item_count;
    // Start is called before the first frame update
    void Start()
    {
        inventory_slots = GetComponentsInChildren<Slot>();
        item_count = 0;
    }

    public void add_item(Item new_item)
    {
        bool flag = false;
        int idx = 0;
        foreach (Slot slot in inventory_slots)
        {
            try
            {
                //klo ada isinya
                if (slot.is_empty == false)
                {
                    if (slot.GetItem().GetType() == new_item.GetType())
                    {
                        flag = true;
                        slot.add_existing_item();
                    }
                }
            }
            catch (System.NullReferenceException)
            {
                break;
            }
        }
        if (flag == false)
        {
            foreach (Slot slot in inventory_slots)
            {
                try
                {
                    //klo ada isinya
                    if (slot.is_empty == true)
                    {
                        inventory_slots[idx].add_new_item(new_item);
                        break;
                    }
                }
                catch (System.NullReferenceException)
                {
                    break;
                }
                idx++;
            }
        }

        //if (item_count == 0)
        //{
        //    inventory_slots[item_count].add_new_item(new_item);
        //    item_count++;
        //}
        //else
        //{
        //    inventory_slots[item_count - 1].add_existing_item();
        //}
    }

    //public void subtract_item(Quest_Item item)
    //{
    //    foreach (Slot slot in inventory_slots)
    //    {
    //        try
    //        {
    //            if (slot.is_empty == false)
    //            {
    //                if (slot.GetItem().GetType() == item.Objective.GetType())
    //                {
    //                    slot.substract_item(item.Count);
    //                }
    //            }
    //            //else
    //            //{
    //            //    break;
    //            //}
    //        }
    //        catch (System.NullReferenceException)
    //        {
    //            break;
    //        }
    //    }        
    //}

    public int search_item(Item item)
    {
        foreach(Slot slot in inventory_slots)
        {
            if(slot.is_empty == false)
            {
                if (item.GetType() == slot.GetItem().GetType())
                {
                    return slot.Item_count;
                }
            }
        }
        return 0;
    }
}

