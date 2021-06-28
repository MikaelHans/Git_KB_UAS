using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Objective_Quest : Base_UI
{
    public Text _objective_name, _objective_count;
    public Image _objective_sprite;
    public override void Setup_UI()
    {
        throw new System.NotImplementedException();
    }
    public override void Setup_UI(GameObject setup)
    {
        Quest_Objective quest_obj = setup.GetComponent<Quest_Objective>();
        _objective_sprite.sprite = quest_obj.Objective.GetComponent<SpriteRenderer>().sprite;
        _objective_name.text = quest_obj.name;
        _objective_count.text = quest_obj.Count.ToString();
    }
}
