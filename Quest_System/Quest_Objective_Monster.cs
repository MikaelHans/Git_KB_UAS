using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Objective_Monster : Quest_Objective
{
    [SerializeField]
    Enemy _enemy;
    [SerializeField]
    int _slayed_count;

    public Quest_Objective_Monster(Enemy enemy, int count = 1)
    {
        this._enemy = enemy;
        this.count = count;
        _slayed_count = 0;
    }

    public Enemy Enemy { get => _enemy; set => _enemy = value; }
}
