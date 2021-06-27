using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Enemy : Enemy
{
    [SerializeField]
    protected float charge_force = 5;
    [SerializeField]
    protected bool in_range = false;
    public projectile _bullet;
    protected override void Update()
    {
        if (is_targeting_player)
        {
            // attack player            
            //charge towards targeted player
            Vector2 cur_pos = transform.position;
            Vector2 target_pos = target.position;
            float distance = Vector2.Distance(cur_pos, target_pos);
            if(distance <= _attack_range)
            {
                in_range = true;
            }
            else
            {
                in_range = false;
            }
            
            if (Time.time >= attack_window && in_range)
            {
                Vector2 charge = (target_pos - cur_pos) * charge_force;
                attack(target.GetComponent<IAttackable>(), charge, 20);
                float temp = Time.time + stop_time;
                attack_window = temp;
                stop_window = temp;
            }
        }
        else
        {
            in_range = false;
        }
        base.Update();
    }

    protected override void FixedUpdate()
    {
        if (Time.fixedTime >= stop_window)
        {
            base.FixedUpdate();
            //Debug.Log("is_moving \n");
        }
        else
        {
            //Debug.Log("FixedTime: " + Time.fixedTime +"\n StopWindow: "+ stop_window);
        }
    }

    public override void attack(IAttackable attacked_object, Vector2 charge, float damage)
    {
        //base.attack(attacked_object, knockback_force, damage);
        Vector2 cur_pos = transform.position;
        Vector2 target_pos = target.position;
        float distance = Vector2.Distance(cur_pos, target_pos);        
        Vector3 adjustspawn = (target_pos - cur_pos).normalized * 2;
        object[] bulletinitdata = new object[2];
        bulletinitdata[0] = charge.x;
        bulletinitdata[1] = charge.y;
        projectile elo = Instantiate(_bullet, transform.position + adjustspawn, transform.rotation, transform);
        elo.GetComponent<Rigidbody2D>().AddForce(new Vector2(charge.x, charge.y), ForceMode2D.Impulse);
    }

    //private void OnDrawGizmos()
    //{
    //    UnityEditor.Handles.color = Color.red;
    //    UnityEditor.Handles.DrawWireDisc(transform.position, new Vector3(0, 0, 1), random_movement_radius);

    //    UnityEditor.Handles.color = Color.blue;
    //    UnityEditor.Handles.DrawWireDisc(transform.position, new Vector3(0, 0, 1), _attack_range);
    //}
}
