using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Objective_Monster : Quest_Objective
{    
    [SerializeField]
    int _slayed_count;
    Event_Manager kill_listener;
    [SerializeField]
    bool _listener_active;

    public Quest_Objective_Monster(Enemy enemy, int count = 1)
    {
        this.count = count;
        _slayed_count = 0;
    }
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

    private void OnDestroy()
    {
        kill_listener.Slayed_Monster.RemoveListener(add_kill_count);
    }

    public void add_kill_count(Enemy killed_enemy)
    {
        if(Objective.GetComponent<Enemy>().GetType() == killed_enemy.GetType())
        {
            _slayed_count++;
        }
    }
    public override int completed()
    {
        return _slayed_count;
    }
}
