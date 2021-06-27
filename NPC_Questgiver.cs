using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPC_Questgiver : NPC
{
    [SerializeField]
    List<Quest> questlist = new List<Quest>();
    //public Quest[] questlist = new Quest[10];
    public GameObject NPC_UI_System;
    public GameObject OngoingQuest_UI_Parent;
    public Item item_npc;
    [SerializeField]
    private Player quest_opener;
    [SerializeField]
    int _id;

    public int Id { get => _id; set => _id = value; }

    public void open_quest()
    {
        if (_player.Has_active_quest == true && _player.Active_quest.QuestGiver_id == _id)
        {
            quest_opener = _player;
            open_active_quest_list();
        }
        else
        {
            quest_opener = _player;
            open_npc_quest_list();
        }
    }

    protected override void Start()
    {
        base.Start();
        NPC_UI_System = GameObject.FindGameObjectWithTag("QuestGiver");
        //set questgiverID of quest to this npc
        foreach(Quest quest in questlist)
        {
            quest.QuestGiver_id = _id;
        }
    }

    void open_npc_quest_list()
    {
        QuestListContent available_quest = NPC_UI_System.GetComponentInChildren<QuestListContent>();
        //if player accept quest, QuestListContent.cs can delete the quest from this NPC's queslist
        available_quest.QuestGiver = this;        
        //turn on quest canvas
        Canvas quest_list_canvas = available_quest.GetComponent<Canvas>();
        if(quest_list_canvas.enabled == false)
        {
            quest_list_canvas.enabled = true;
            //add each quest this npc have to the UI system
            initialize_quests();
        }        
    }

    void open_active_quest_list()
    {
        OnGoing_Quest_UI OnGoing_Quest = NPC_UI_System.GetComponentInChildren<OnGoing_Quest_UI>();
        //if player accept quest, QuestListContent.cs can delete the quest from this NPC's queslist
        OnGoing_Quest.Player = quest_opener;
        //turn on quest canvas
        Canvas quest_list_canvas = OnGoing_Quest.GetComponent<Canvas>();
        if(quest_list_canvas.enabled == false)
        {
            quest_list_canvas.enabled = true;
            //add each quest this npc have to the UI system
            NPC_UI_System.GetComponentInChildren<OnGoing_Quest_UI>().setup(quest_opener);
        }        
    }

    void initialize_quests()
    {
        foreach(Quest quest in questlist)
        {
            NPC_UI_System.GetComponentInChildren<QuestListContent>().add_quest(quest);
        }        
    }

    public void delete_quest()
    {

    }

    public void delete_all_quest()
    {

    }
}
