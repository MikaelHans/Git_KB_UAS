using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_UI : MonoBehaviour
{
    public Text _npc_name_UI, _dialogue_box_UI;
    public Image _npc_sprite_UI;
    GameObject _npc;
    public void display_UI(GameObject npc)
    {
        _npc = npc;
        GetComponent<Canvas>().enabled = true;
        _npc_name_UI.text = npc.name;
        _dialogue_box_UI.text = npc.GetComponent<NPC>().conversation_dialogues[0];
        _npc_sprite_UI.sprite = npc.GetComponent<SpriteRenderer>().sprite;
    }

    public void on_UI_close()
    {
        GetComponent<Canvas>().enabled = false;
        if(_npc.GetComponent<NPC_Questgiver>() != null)
        {
            _npc.GetComponent<NPC_Questgiver>().open_quest();
        }
    }
}
