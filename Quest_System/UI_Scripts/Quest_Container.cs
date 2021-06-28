using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Container : MonoBehaviour
{
    //list of available quest
    public Text title_text;
    public GameObject quest_item_prefab, scrollView_Content;
    public Button accept_button;
    Quest quest;
    // Start is called before the first frame update
    public void Setup_Quest(Quest added_quest)
    {
        title_text.text = added_quest.Quest_title;
        int index = 0;
        foreach(Quest_Item item in added_quest.Quest_item)
        {
            Instantiate(quest_item_prefab, scrollView_Content.transform);
            GetComponentsInChildren<Quest_Item_UI>()[index++].setup(item);
        }        
        quest = added_quest;
        accept_button.onClick.AddListener(player_accept_quest);
    }

    public void Setup_Quests(Quest added_quest)
    {
        title_text.text = added_quest.Quest_title;        
    }

    void player_accept_quest()
    {
        if (GetComponentInParent<QuestListContent>().Player.accept_quest(quest))
        {
            Debug.Log("SUCCESS");
            GetComponentInParent<Canvas>().enabled = false;
        }
        else
        {
            Debug.Log("Already has quest");
        }        
    }

}
