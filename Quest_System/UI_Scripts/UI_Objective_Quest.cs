﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Objective_Quest : Base_UI
{
    public Text _objective_name, _objective_count;
    public Image _objective_sprite;
    public Quest_Objective _quest_objective;
    public override void Setup_UI()
    {
        throw new System.NotImplementedException();
    }
    public override void Setup_UI(GameObject setup, bool isactive)
    {
        Quest_Objective quest_obj = setup.GetComponent<Quest_Objective>();
        _quest_objective = quest_obj;
        _objective_sprite.sprite = quest_obj.Objective.GetComponent<SpriteRenderer>().sprite;
        _objective_name.text = quest_obj.name;        
        if (isactive)
        {
            _objective_count.text = quest_obj.Count.ToString() + '/' + quest_obj.completed();
        }
        else
        {
            _objective_count.text = quest_obj.Count.ToString();
        }
    }
}
