using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Creature : Item_Dropper, IAttackable
{
    [SerializeField]
    float defense;
    [SerializeField]
    Seeker path_seeker;
    [SerializeField]
    protected Path path;
    [SerializeField]
    protected Vector2 direction;

    protected int current_point;
    protected GameObject container_gameobject;
    public float random_movement_radius = 10;
    public Transform target;
    public float next_waypoint_trigger = 0.3f;


    protected override void Start()
    {
        base.Start();
        path_seeker = gameObject.GetComponent<Seeker>();        
        InvokeRepeating("update_path", 0f, 0.5f);
        current_point = 0;
        container_gameobject = new GameObject();
    }

    protected virtual void update_path()
    {
        if (target != null)
            path_seeker.StartPath(ai_rigidbody.position, target.position, on_path_complete);
    }

    protected virtual void on_path_complete(Path new_path)
    {
        if (!new_path.error)
        {
            path = new_path; 
            current_point = 0;
        }
    }

    public virtual void take_damage(float _damage, Vector2 force)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        _hp -= _damage;
    }
}
