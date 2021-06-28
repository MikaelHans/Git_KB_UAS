using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Item_UI : MonoBehaviour
{
    //sg ini buat inventory
    public Image item_sprite;
    public Text UI_item_name, UI_item_count;
    protected Player player;
    protected string itemname;
    [SerializeField]
    protected Quest_Item questItem;

    public Quest_Item QuestItem { get => questItem; set => questItem = value; }

    public virtual void setup(Quest_Item quest_item)
    {
        item_sprite.sprite = quest_item.Item.GetComponent<SpriteRenderer>().sprite;
        UI_item_name.text = quest_item.Item.item_name;
        UI_item_count.text = "x" + quest_item.Count.ToString();
    }
}
