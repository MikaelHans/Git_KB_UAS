using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Goblin : Ranged_Enemy
{
    public Animator anim;
    public Rigidbody2D rgbd;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (path != null)
        {
            if (path.vectorPath.Count > current_point)
            {
                setDir((Vector2)path.vectorPath[current_point]);
            }
        }
    }

    Vector2 convert_to_direction(Vector2 rawdata)
    {
        Vector2 converteddata = new Vector2();

        float lX = Mathf.Pow(rawdata.x,2);
        float lY = Mathf.Pow(rawdata.y,2);
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

    public override void attack(IAttackable attacked_object, Vector2 knockback_force, float damage)
    {
        setDir(target.position);
        base.attack(attacked_object, knockback_force, damage);
        anim.SetTrigger("attack");
        Debug.Log("attack");
    }

    void setDir(Vector2 target)
    {
        direction = (target - ai_rigidbody.position).normalized;
        Vector2 tmp = convert_to_direction(direction);

        if (tmp.x != 0 || tmp.y != 0 && !in_range)
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

    public override void take_damage(float _damage, Vector2 force)
    {
        base.take_damage(_damage, force);
        setDir(target.position); 
        anim.SetTrigger("attacked");
    }
}
