using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Boss
{
    [SerializeField]
    protected float charge_force = 5;
    [SerializeField]
    protected bool in_range = false;
    public projectile _bullet;
    [SerializeField]
    float _closeattack_range, _closeattack_window, _closeattack_rate, _chargeattack_rate;
    /*
     * Note:
     * _attack_window yang di game_character dibuat sebagai chargeattack window  
     */
    protected override void Update()
    {
        if (is_targeting_player)//istargeting iku shortcut
        {
            if (check_attack_readiness() >= 0)// if any attack is ready
            {
                Vector2 enemypos = target.transform.position;// get enemy pos
                Vector2 current_pos = transform.position;//get this pos
                float range_between_object_and_target = Vector2.Distance(current_pos, enemypos);
                if (attack_window <= Time.time)//if charge attack is ready
                {
                    //if in range of charged attack and within attack window
                    if (range_between_object_and_target <= _attack_range)
                    {
                        /*
                         * charge no attack because attack is handled by check_collision_when_charging() func
                         */
                        Vector2 charge = (current_pos - enemypos) * charge_force;
                        gameObject.GetComponent<Rigidbody2D>().AddForce(-charge, ForceMode2D.Impulse);
                        float temp = Time.time + _chargeattack_rate;
                        attack_window = temp;
                        Debug.Log("CHARGE!!!!!");
                    }
                }
                //closerange attack
                else if (_closeattack_window <= Time.time)
                {
                    //if in range of close attack and within attack window
                    if (range_between_object_and_target <= _closeattack_range)
                    {
                        float temp = Time.time + _closeattack_rate;
                        _closeattack_window = temp;

                        IAttackable player = target.GetComponent<IAttackable>();
                        Vector2 dir = ((enemypos - current_pos));
                        Vector2 knockback_force = dir * force;

                        attack(player, knockback_force, _damage);
                    }
                }
                //check collsion when charging by lala
                check_collision_when_charging(enemypos, current_pos);
            }
        }
        base.Update();
    }

    protected override void FixedUpdate()//move
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

    public override void attack(IAttackable attacked_object, Vector2 knockback_force, float damage)//onhit enemy
    {
        base.attack(attacked_object, knockback_force, damage);
        attacked_object.take_damage(damage, knockback_force);
    }

    int check_attack_readiness()
    {
        int ret = 0;
        if (attack_window <= Time.time)// cek apakah charge attack sudah siap
        {
            ret++;
        }
        if (_closeattack_window <= Time.time)
        {
            ret++;
        }
        return ret;
    }

    void check_collision_when_charging(Vector2 target_pos, Vector2 cur_pos)
    {
        //check if collide when charging
        BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
        if (col.IsTouching(target.gameObject.GetComponent<BoxCollider2D>())
            && gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 10)//check with velocity if velocity great then charging
        {
            IAttackable player = target.GetComponent<IAttackable>();
            Vector2 dir = ((target_pos - cur_pos));
            Vector2 knockback_force = dir * force;
            attack(player, knockback_force, _damage);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    UnityEditor.Handles.color = Color.red;
    //    UnityEditor.Handles.DrawWireDisc(transform.position, new Vector3(0, 0, 1), random_movement_radius);

    //    UnityEditor.Handles.color = Color.blue;
    //    UnityEditor.Handles.DrawWireDisc(transform.position, new Vector3(0, 0, 1), _attack_range);
    //}
}
