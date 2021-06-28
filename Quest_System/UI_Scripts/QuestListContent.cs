using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListContent : MonoBehaviour
{
    public GameObject quest_container_prefab;
    [SerializeField]
    Player player;
    [SerializeField]
    NPC_Questgiver questGiver;
    public GameObject scrollviewContent;

    public Player Player { get => player; set => player = value; }
    public NPC_Questgiver QuestGiver { get => questGiver; set => questGiver = value; }

    // Start is called before the first frame update


    private void Update()
    {
        if (GetComponentInParent<Canvas>().enabled == false)
        {
            clear_all_quest();
        }
    }

    public void add_quest(Quest added_quest)
    {
        //quest_container_prefab
        Instantiate(quest_container_prefab, scrollviewContent.transform);        
        Quest_Container [] container_array = GetComponentsInChildren<Quest_Container>();
        container_array[container_array.Length - 1].Setup_Quest(added_quest);
    }

    //void add_quests(List<Quest> list_of_quest)
    //{

    //}

    public void clear_all_quest()
    {
        GetComponentInParent<Canvas>().enabled = false;
        foreach(Quest_Container quest in GetComponentsInChildren<Quest_Container>())
        {
            Destroy(quest.gameObject);
        }
    }
}
