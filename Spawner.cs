using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Creature spawn_prefab_path;  
   
    public void spawn()
    {
        Creature elo = Instantiate(spawn_prefab_path, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity);
        elo.transform.parent = transform;
    }

    public void delete_spawned()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void set_prefab(Creature prefab)
    {
        spawn_prefab_path = prefab;
    }
}
