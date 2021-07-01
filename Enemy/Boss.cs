using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    protected Animator anim;
    public float _center_offset;
    [SerializeField]
    protected bool is_retreating, _is_healing;

    protected override void Update()
    {
        base.Update();
        if(target != null)
        {
            if (transform.position.x + _center_offset < target.position.x)
            {
                _sprtrenderer.flipX = false;
            }
            else if (transform.position.x - _center_offset > target.position.x)
            {
                _sprtrenderer.flipX = true;
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    public override void attack(IAttackable attacked_object, Vector2 knockback_force, float damage)//onhit enemy
    {
        base.attack(attacked_object, knockback_force, damage);       
    }

    public override void take_damage(float _damage, Vector2 force)
    {
        base.take_damage(_damage, force);
        anim.SetTrigger("takehit");
    }

    protected override void move()
    {
        base.move();
        anim.SetBool("moving", true);
    }

    protected override void die()
    {
        anim.SetTrigger("ded");
    }

    public virtual void die_after_anim()
    {
        Destroy(container_gameobject);
        drop_item();
        Destroy(gameObject);
    }

    public virtual void skill_after_anim()
    {

    }
}
