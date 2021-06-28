using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour, IAttacking
{
    float lifetime = 2f, curentTime = 0, _knockBackForce = 1500, _damage=5;
    public CircleCollider2D col;

    private void Start()
    {
        curentTime = Time.time;        
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= curentTime + lifetime)
        {
            Destroy(gameObject);            
        }
        Collider2D[] hit = new Collider2D[5];
        //contactfilter
        ContactFilter2D rigidbody_filter = new ContactFilter2D();
        rigidbody_filter.useTriggers = false;
        //check whos colliding with projectile
        

        if (col.OverlapCollider(rigidbody_filter, hit) >= 1)
        {
            if (hit[0].GetComponent<Player>() != null)
            {
                Player target = hit[0].GetComponent<Player>();
                IAttackable player = target.GetComponent<IAttackable>();
                Vector2 cur_pos = transform.position, target_pos = target.transform.position;
                Vector2 dir = ((target_pos - cur_pos)).normalized;
                Vector2 knockback_force = dir * _knockBackForce;
                attack(player, knockback_force, _damage);                
            }
            Destroy(gameObject);
        }
    }

    public void attack(IAttackable attacked_object, Vector2 knockback_force, float damage)
    {
        attacked_object.take_damage(damage, knockback_force);
        //Debug.Log("hit");
    }
}
