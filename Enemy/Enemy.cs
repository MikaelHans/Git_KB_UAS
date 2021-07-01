using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Enemy : Creature, IAttacking
{
    [SerializeField]
    protected bool is_targeting_player;
    [SerializeField]
    protected float stop_time = 3, stop_window = 0;
    [SerializeField]
    //follow player
    protected IAttackable _attacked_object_stored;
    protected Vector2 _stored_knockback_force;
    protected float _stored_damage;


    protected override void Start()
    {
        base.Start();
        is_targeting_player = false;
    }

    protected override void Update()
    {
        base.Update();        
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)// enemy in range
    {
        if (!target)
        {
            return;
        }
        else if ((target.gameObject == container_gameobject.gameObject || target == null) && collision.gameObject.GetComponent<Player>())
        {
            target = collision.gameObject.GetComponent<Transform>();
            is_targeting_player = true;
        }
    }
    //unfollow player when out of range
    protected virtual void OnTriggerExit2D(Collider2D collision)// enemy out of range
    {
        if (!target)
        {
            is_targeting_player = false;
            return;
        }
        else if (collision.gameObject == target.gameObject)
        {
            Debug.Log("TRIGGER EXIT");
            target = null;
            is_targeting_player = false;
        }
    }

    protected virtual void choose_random_spot()
    {
        Vector3 random_position = (Vector3)Random.insideUnitCircle * random_movement_radius;
        random_position += transform.position;
        container_gameobject.transform.position = new Vector3(0, 0, 0);
        container_gameobject.transform.position = random_position;
        target = container_gameobject.transform;
        //Debug.Log("Called");
    }

    protected override void move()
    {
        if (path == null || target == null)
        {
            choose_random_spot();
            return;
        }
        if (current_point >= path.vectorPath.Count && target.gameObject.GetComponent<Player>() == null)
        {
            target = null;
            return;
        }

        direction = ((Vector2)path.vectorPath[current_point] - ai_rigidbody.position).normalized;
        Vector3 targetVelocity = direction * _move_speed * Time.deltaTime;
        gameObject.transform.position += targetVelocity;

        float distance = Vector2.Distance(ai_rigidbody.position, path.vectorPath[current_point]);
        if (distance < next_waypoint_trigger)
        {
            if(target.gameObject.GetComponent<Player>() == null)
            {
                current_point++;
            }
            else
            {
                if (current_point + 1 < path.vectorPath.Count)
                {
                    current_point++;
                }
            }                     
        }
    }

    protected override void die()
    {
        base.die();
        if (FindObjectOfType<Event_Manager>() != null)
        {
            FindObjectOfType<Event_Manager>().Slayed_Monster.Invoke(GetType());
        }        
        Destroy(container_gameobject);
        drop_item();        
        Destroy(gameObject);
    }

    public virtual void attack(IAttackable attacked_object, Vector2 knockback_force, float damage)
    {
        //throw new System.NotImplementedException();
    }

    public virtual void attack_after_anim()
    {
        attack(_attacked_object_stored, _stored_knockback_force, _stored_damage);
    }

    protected void store_atk_info(IAttackable attacked_object, Vector2 knockback_force, float damage)
    {
        _attacked_object_stored = attacked_object;
        _stored_knockback_force = knockback_force;
        _stored_damage = damage;
    }
}
