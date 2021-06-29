using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Objective_Monster : Quest_Objective
{    
    [SerializeField]
    int _slayed_count;
    Event_Manager kill_listener;

    bool _listener_active;

    public override void init()
    {        
        if (!_listener_active)
        {            
            Debug.Log("IN");
            kill_listener = FindObjectOfType<Event_Manager>();
            kill_listener.Slayed_Monster.AddListener(add_kill_count);
            _listener_active = true;
        }
    }    

    public void add_kill_count(System.Type enemytype)
    {
        Debug.Log("Cok");
        if (Objective.GetComponent<Enemy>().GetType() == enemytype)
        {
            Debug.Log("Cok");
            _slayed_count++;
        }
    }
    public override int completed()
    {
        return _slayed_count;
    }
}
