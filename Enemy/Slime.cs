using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Slime : Enemy
{
    [SerializeField]
    protected float charge_range = 3, charge_force = 5;
    [SerializeField]
    bool in_range = false;
    protected override void Update()
    {             
        if (is_targeting_player)
        {
            // attack player            
            //charge towards targeted player
            Vector2 cur_pos = transform.position;
            Vector2 target_pos = target.position;
            float distance = Vector2.Distance(cur_pos, target_pos);
            in_range = true;
            if (Time.time >= attack_window && distance <= charge_range)
            {
                Vector2 charge = (cur_pos - target_pos) * charge_force;
                gameObject.GetComponent<Rigidbody2D>().AddForce(-charge, ForceMode2D.Impulse);
                float temp = Time.time + stop_time;
                attack_window = temp;
                stop_window = temp;
            }
            //if bump, attack player
            BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
            if (col.IsTouching(target.gameObject.GetComponent<BoxCollider2D>()))
            {
                IAttackable player = target.GetComponent<IAttackable>();
                Vector2 dir = ((target_pos - cur_pos));
                Vector2 knockback_force = dir * force;
                attack(player, knockback_force, _damage);
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

    public override void attack(IAttackable attacked_object, Vector2 knockback_force, float damage)
    {
        base.attack(attacked_object, knockback_force, damage);
        attacked_object.take_damage(damage, knockback_force);
    }   
}
