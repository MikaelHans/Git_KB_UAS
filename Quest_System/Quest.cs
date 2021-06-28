using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField]
    string quest_description, quest_title;
    [SerializeField]
    float time_limit;
    [SerializeField]
    List<Quest_Objective> _quest_objective = new List<Quest_Objective>();
    [SerializeField]
    int _questGiver_id;

    public Quest(int questgiver_id, Quest_Objective_Item quest_item, string quest_description= "Not Set", string quest_title="Not Set", float time_limit = 0f)
    {
        this.quest_description = quest_description;
        this.quest_title = quest_title;
        this.time_limit = time_limit;
        _questGiver_id = questgiver_id;
    }

    public Quest()
    {
        quest_description = "NOT SET";
        quest_title = "NOT SET";
        time_limit = 0;
        _questGiver_id = 0;
    }

    private void Awake()
    {
        _quest_objective.AddRange(GetComponentsInChildren<Quest_Objective>());
        Debug.Log(_quest_objective);
    }

    public string Quest_description { get => quest_description; set => quest_description = value; }
    public string Quest_title { get => quest_title; set => quest_title = value; }
    public float Time_limit { get => time_limit; set => time_limit = value; }
    public int QuestGiver_id { get => _questGiver_id; set => _questGiver_id = value; }
    public List<Quest_Objective> Quest_objective { get => _quest_objective; set => _quest_objective = value; }
    public virtual void complete_quest() { }
    public virtual void init() { }
}
