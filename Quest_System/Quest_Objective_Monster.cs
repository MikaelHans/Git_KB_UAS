using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Objective_Monster : Quest_Objective
{    
    [SerializeField]
    int _slayed_count;

    public Quest_Objective_Monster(Enemy enemy, int count = 1)
    {
        this.count = count;
        _slayed_count = 0;
    }

    private void Awake()
    {
        Event_Manager.Slayed_Monster.AddListener(add_kill_count);
    }

    private void OnDestroy()
    {
        Event_Manager.Slayed_Monster.RemoveListener(add_kill_count);
    }

    void add_kill_count()
    {
        _slayed_count++;
    }
    public override int completed()
    {
        return _slayed_count;
    }
}
