using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Quest : Base_UI
{
    List<UI_Objective_Quest> _quest_objective = new List<UI_Objective_Quest>();
    public Text _quest_name, _quest_desc;
    [SerializeField]
    Player_Action action;
    public override void Setup_UI()
    {
        
    }
    public override void Setup_UI(GameObject setup)
    {
        Quest quest = setup.GetComponent<Quest>();
        _quest_name.text = quest.name;
        _quest_desc.text = quest.Quest_description;
        action._quest = quest;
        foreach(Quest_Objective questobj in quest.Quest_objective)
        {
            GameObject elo = Instantiate(_prefab, _content.transform);
            elo.GetComponent<UI_Objective_Quest>().Setup_UI(questobj.gameObject);
        }
    }

    
}
