using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Character : MonoBehaviour
{
    [SerializeField]
    protected float _move_speed;
    [SerializeField]
    protected float _hp = 10, attack_rate = 1, attack_window = 0;
    [SerializeField]
    protected float _defense, _damage, force, _attack_range;
    [SerializeField]
    protected Rigidbody2D ai_rigidbody;

    protected virtual void Awake()
    {
        
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        ai_rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_hp <= 0)
        {
            die();
        }
    }

    protected virtual void FixedUpdate()
    {
        move();
    }

    public float Get_Hp()
    {
        return _hp;
    }
    public float Get_Damage()
    {
        return _damage;
    }

    public float Get_Force()
    {
        return force;
    }
    public float Get_Speed()
    {
        return _move_speed;
    }

    public void set_hp(float hp)
    {
        _hp = hp;
    }
    public void set_damage(float hp)
    {
        _damage = hp;
    }
    public void set_force(float hp)
    {
        force = hp;
    }
    public void set_speed(float hp)
    {
        _move_speed = hp;
    }

    protected virtual void move()
    {

    }
    
    protected virtual void die()
    {

    }
}
