using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPC_Questgiver : NPC
{
    [SerializeField]
    List<Quest> questlist = new List<Quest>();
    //public Quest[] questlist = new Quest[10];
    public GameObject _ongoing_quest_UI, _available_quests_UI;
    public Item item_npc;
    [SerializeField]
    private Player quest_opener;
    [SerializeField]
    int _id;

    public int Id { get => _id; set => _id = value; }

    private void Awake()
    {
        int index = 0;
        foreach(Quest quest in questlist)
        {
            Quest elo = Instantiate(quest, transform);
            elo.QuestGiver_id = _id;
        }
        questlist.Clear();
        //yitback
        foreach (Quest quest in GetComponentsInChildren<Quest>())
        {
            questlist.Add(quest);
        }
    }

    

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
        _available_quests_UI = GameObject.FindGameObjectsWithTag("QuestGiver")[1];
        //set questgiverID of quest to this npc
        _ongoing_quest_UI = GameObject.FindGameObjectsWithTag("QuestGiver")[2];
        foreach (Quest quest in questlist)
        {
            quest.QuestGiver_id = _id;
        }
        //Player player = FindObjectOfType<Player>();
        //if (player.Has_active_quest)
        //{
        //    if(player.Active_quest.QuestGiver_id == _id)
        //    {
        //        quest_opener = player;
        //    }
        //}
    }

    void open_npc_quest_list()
    {        
        _available_quests_UI.GetComponent<Canvas>().enabled = true;
        _available_quests_UI.GetComponent<UI_Quest_System>().init(questlist);
    }

    void open_active_quest_list()
    {
        _ongoing_quest_UI.GetComponent<Canvas>().enabled = true;
        _ongoing_quest_UI.GetComponent<UI_Quest_System>().init(quest_opener.Active_quest);
    }

    void initialize_quests()
    {
        foreach(Quest quest in questlist)
        {
            quest.init();
        }        
    }

    public void delete_quest()
    {

    }

    public void delete_all_quest()
    {

    }
}
