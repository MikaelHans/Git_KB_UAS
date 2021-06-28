using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Accept_Quest : Player_Action
{
    
    Player _player;
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }
    public override void action()
    {
        _player.accept_quest(_quest);
        GetComponentInParent<UI_Quest_System>().closecanvas();
    }
}
