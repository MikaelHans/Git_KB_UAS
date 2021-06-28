using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Quest_System : Base_UI
{
    List<Quest> _questlist = new List<Quest>();
    bool _isactive;
    public void init(List<Quest> questlist)//questlist
    {
        _questlist.AddRange(questlist);
        Setup_UI();
        _isactive = false;
    }
    public void init(Quest quest)//active quest 
    {
        _questlist.Add(quest);
        _isactive = true;
        Setup_UI();        
    }
    public void init()//from click button(acticve quest)
    {
        _isactive = true;
        if (GetComponent<Canvas>().enabled == false)
        {
            GetComponent<Canvas>().enabled = true;
            Quest quest = FindObjectOfType<Player>().Active_quest;
            if (quest != null)
            {
                _questlist.Add(quest);
                Setup_UI();
            }
            else
            {

            }
        }        
    }

    public override void Setup_UI()//setup quests
    {
        foreach(Quest quest in _questlist)
        {
            //instantiate and add object to content as child
            GameObject elo = Instantiate(_prefab, _content.transform);
            //isactive is to tell quest objective item wether or not to add live check
            elo.GetComponent<UI_Quest>().Setup_UI(quest.gameObject, _isactive);
        }
    }

    public void closecanvas()
    {
        //clear all displayed item cekno gak dobel
        GetComponent<Canvas>().enabled = false;
        foreach (UI_Quest uiq in _content.GetComponentsInChildren<UI_Quest>())
        {
            Destroy(uiq.gameObject);
        }
        _questlist.Clear();
    }
}
