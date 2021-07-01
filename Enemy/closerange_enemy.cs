using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class closerange_enemy : Enemy
{
   
    [SerializeField]
    protected bool in_range = false;
    public Animator anim;
    public Rigidbody2D rgbd;
    protected override void Update()
    {
        if (is_targeting_player)
        {
            // attack player            
            //charge towards targeted player
            if(target != null)
            {
                Vector2 cur_pos = transform.position;
                Vector2 target_pos = target.position;
                float distance = Vector2.Distance(cur_pos, target_pos);
                in_range = true;
                if (Time.time >= attack_window && distance <= _attack_range)
                {
                    float temp = Time.time + stop_time;
                    attack_window = temp;
                    stop_window = temp;

                    IAttackable player = target.GetComponent<IAttackable>();
                    Vector2 dir = ((target_pos - cur_pos));
                    Vector2 knockback_force = dir * force;
                    attack(player, knockback_force, _damage);
                }
            }
        }
        else
        {
            in_range = false;
        }
        base.Update();

        if (path != null)
        {
            if (path.vectorPath.Count > current_point)
            {
                setDir((Vector2)path.vectorPath[current_point]);
            }
        }

        if (path != null)
        {
            if (path.vectorPath.Count > current_point)
            {
                setDir((Vector2)path.vectorPath[current_point]);
            }
        }
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

    Vector2 convert_to_direction(Vector2 rawdata)
    {
        Vector2 converteddata = new Vector2();

        float lX = Mathf.Pow(rawdata.x, 2);
        float lY = Mathf.Pow(rawdata.y, 2);
        if (lX > 0 || lY > 0)
        {
            if (lX > lY)
            {
                converteddata.x = 1;
                converteddata.y = 0;
                if (rawdata.x < 0)
                    converteddata *= -1;
            }
            else
            {
                converteddata.x = 0;
                converteddata.y = 1;
                if (rawdata.y < 0)
                    converteddata *= -1;
            }
        }
        else
        {
            converteddata.x = 0;
            converteddata.y = 0;
        }
        return converteddata;
    }

    void setDir(Vector2 target)
    {
        direction = (target - ai_rigidbody.position).normalized;
        Vector2 tmp = convert_to_direction(direction);

        if (tmp.x != 0 || tmp.y != 0)
        {
            anim.SetInteger("horizontal_state", (int)tmp.x);
            anim.SetInteger("vertical_state", (int)tmp.y);
            anim.SetBool("ismoving", true);
        }
        else
        {
            anim.SetBool("ismoving", false);
        }
    }

    public override void attack(IAttackable attacked_object, Vector2 knockback_force, float damage)
    {
        setDir(target.position);
        if (attacked_object != null)
        {
            base.attack(attacked_object, knockback_force, damage);
            attacked_object.take_damage(damage, knockback_force);
            anim.SetTrigger("attack");
            Debug.Log("attack");
        }        
    }
    public override void take_damage(float _damage, Vector2 force)
    {
        base.take_damage(_damage, force);
        setDir(target.position);
        anim.SetTrigger("attacked");
    }
}
