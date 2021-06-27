using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest_spawner : spawner_system
{
    protected override void Update()
    {
        if (Time.time >= spawn_time + spawn_rate)
        {
            delete_all();
            spawn_all();
            spawn_time = Time.time + spawn_rate;
        }
        _timenow = Time.time;
    }
}
