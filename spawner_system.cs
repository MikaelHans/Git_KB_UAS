using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner_system : MonoBehaviour
{    
    [SerializeField]
    protected float spawn_time = 0;
    [SerializeField]
    protected float spawn_rate = 10;
    [SerializeField]
    bool on_start_spawn = true;
    [SerializeField]
    List<Spawner> spawner_list = new List<Spawner>();
    public Creature prefab_path;
    public int max_spawn;
    public Object spawn_type;
    public string _typename;
    [SerializeField]
    protected int _spawn_population = 0;
    public float _timenow = 0;

    private void Start()
    {
        spawner_list.AddRange(GetComponentsInChildren<Spawner>());
        foreach (Spawner _spawn in spawner_list)
        {
            _spawn.set_prefab(prefab_path);
        }
        if (on_start_spawn)
        {
            spawn_all();
        }
        //Debug.Log(spawn_type.GetType().FullName);
    }

    protected virtual void Update()
    {
        if(Time.time >= spawn_time + spawn_rate)
        {
            spawn_all();
            spawn_time = Time.time + spawn_rate;
        }
        _timenow = Time.time;
        //Debug.Log(Time.time);
    }

    public void spawn_all()
    {
        _spawn_population = FindObjectsOfType(System.Type.GetType(_typename)).Length;
        foreach (Spawner _spawn in spawner_list)
        {
            if(_spawn_population <= max_spawn)
            {                
                _spawn.spawn();
            }
            else
            {
                break;
            }
        }
    }

    public void delete_all()
    {
        foreach (Spawner _spawn in spawner_list)
        {
            _spawn.delete_spawned();
        }
    }
}
