using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_UI : MonoBehaviour
{
    public Text _npc_name_UI, _dialogue_box_UI;
    public Image _npc_sprite_UI;
    NPC _npc;
    [SerializeField]
    bool _close_without_opening_quest;
    public void display_UI(NPC npc)
    {
        _npc = npc;
        GetComponent<Canvas>().enabled = true;
        _npc_name_UI.text = npc.name;
        _dialogue_box_UI.text = npc.conversation_dialogues[0];
        _npc_sprite_UI.sprite = npc.GetComponent<SpriteRenderer>().sprite;
    }
    public void display_UI(NPC npc, int dialogue)
    {
        _npc = npc;
        GetComponent<Canvas>().enabled = true;
        _npc_name_UI.text = npc.name;
        _dialogue_box_UI.text = npc.conversation_dialogues[dialogue];
        _npc_sprite_UI.sprite = npc.GetComponent<SpriteRenderer>().sprite;
    }


    public void on_UI_close()
    {
        GetComponent<Canvas>().enabled = false;
        NPC_Questgiver questgiver = _npc.GetComponent<NPC_Questgiver>();
        if(!_close_without_opening_quest)
        {
            if (questgiver != null)
            {
                if (questgiver.can_open())
                {
                    questgiver.open_quest();
                }
                else if (!questgiver.can_open())
                {
                    display_UI(_npc, 2);
                    _close_without_opening_quest = true;
                }
            }
        }
        else
        {
            _close_without_opening_quest = false;
        }
    }
}
