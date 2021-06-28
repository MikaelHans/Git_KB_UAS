using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onGoing_Quest_Item : Quest_Item_UI
{
    public Text item_complete_text;
    
   
    public void setup(Quest_Item _quest_item, Player _player)
    {
        questItem = _quest_item;
        player = _player;
        itemname = questItem.Item.item_name;
       
        item_sprite.sprite = questItem.Item.GetComponent<SpriteRenderer>().sprite;
        UI_item_name.text = questItem.Item.item_name;
        UI_item_count.text = player.get_inventory().search_item(questItem.Item).ToString() + "/" + questItem.Count.ToString();

        if (_player.get_inventory().search_item(questItem.Item) >= questItem.Count)
        {
            item_complete_text.enabled = true;
        }
    }

    public bool check_if_complete(Item quest_item)
    {
        if (player.get_inventory().search_item(quest_item) >= questItem.Count)
        {
            return true;
        }
        return false;
    }
}
