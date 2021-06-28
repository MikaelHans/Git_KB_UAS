using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Quest_System : Base_UI
{
    List<Quest> _questlist = new List<Quest>();

    public void init(List<Quest> questlist)
    {
        _questlist = questlist;
        Setup_UI();
    }
    public void init(Quest quest)
    {
        _questlist.Add(quest);
        Setup_UI();
    }
    public override void Setup_UI()//setup quests
    {
        foreach(Quest quest in _questlist)
        {
            GameObject elo = Instantiate(_prefab, _content.transform);
            elo.GetComponent<UI_Quest>().Setup_UI(quest.gameObject);
        }
    }

    public void closecanvas()
    {
        GetComponent<Canvas>().enabled = false;
    }
}
