using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Active_Quest : MonoBehaviour
{
    //player
    public Text title;
    public GameObject quest_items_scrollview;
    public Quest_Item_UI quest_item_prefab;
    public Button closebutton;
    private Player player;

    public Player Player { get => player; set => player = value; }

    public void setup_UI()
    {
        GetComponentInChildren<Canvas>().enabled = true;
        if (player.Has_active_quest)
        {
            Quest active_quest = player.Active_quest;
            title.text = active_quest.Quest_title;
            setup_items(active_quest);
        }
        else
        {
            title.text = "No Active Quest";
        }
    }

    void setup_items(Quest quest)
    {
        //foreach(Item questitem in quest.Quest_item)
        GetComponentInChildren<Canvas>().enabled = true;
        int index = 0;
        foreach(Quest_Item item in quest.Quest_item)
        {
            Instantiate(quest_item_prefab, quest_items_scrollview.transform);
            GetComponentsInChildren<onGoing_Quest_Item>()[index].setup(item, player);
            GetComponentsInChildren<onGoing_Quest_Item>()[index++].gameObject.GetComponent<RectTransform>().localScale = new Vector3(5, 5, 1);
        }       
    }

    public void clear_content()
    {
        GetComponentInChildren<Canvas>().enabled = false;
        foreach(Quest_Item_UI quest in quest_items_scrollview.GetComponentsInChildren<Quest_Item_UI>())
        {
            Destroy(quest.gameObject);
        }
        title.text = "No Active Quest";
    }
}
