using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnGoing_Quest : OnGoing_Quest_UI
{
    public Text title;
    public Button action_button;
    private void Start()
    {
        action_button.onClick.AddListener(click_action);
    }

    public override void setup(Player _player)
    {
        //base.setup(setupOBJ);
        player = _player;
        Quest add_quest = _player.Active_quest;
        title.text = add_quest.Quest_title;
        instantiate_requested_items();
    }

    void instantiate_requested_items()
    {
        int index = 0;
        foreach (Quest_Item item in player.Active_quest.Quest_item)
        {
            Instantiate(prefab, content.transform);
            GetComponentsInChildren<onGoing_Quest_Item>()[index++].setup(item, player);
        }
    }

    void click_action()
    {
        //GetComponentInParent<Canvas>().enabled = false;
        //GetComponentInParent<OnGoing_Quest_UI>().clear_all();
        //onGoing_Quest_Item quest_item = GetComponentsInChildren<onGoing_Quest_Item>()[0];
        bool flag = true;
        foreach(onGoing_Quest_Item quest_item in GetComponentsInChildren<onGoing_Quest_Item>())
        {
            if (!(quest_item.check_if_complete(quest_item.QuestItem.Item)))
            {
                flag = false;
            }
        }          
        if(flag == true)
        {
            player.finish_quest();
        }
        GetComponentInParent<Canvas>().enabled = false;        
        Debug.Log("finish quest");
        Destroy(this.gameObject);
    }
}
