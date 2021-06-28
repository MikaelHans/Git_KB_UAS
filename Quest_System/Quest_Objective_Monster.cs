using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Objective_Monster : Quest_Objective
{
    [SerializeField]
    Enemy _enemy;

    public Quest_Objective_Monster(Enemy enemy, int count = 1)
    {
        this._enemy = enemy;
        this.count = count;
    }

    public Enemy Enemy { get => _enemy; set => _enemy = value; }
}
