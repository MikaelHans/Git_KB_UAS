using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wraith_King : Boss
{
    [SerializeField]
    protected float charge_force = 5;
    [SerializeField]
    protected bool in_range = false;
    [SerializeField]
    float _closeattack_range, _closeattack_window, _closeattack_rate, _closeattack_force, _closeattack_damage,
        _chargeattack_rate, _charge_window, _charge_active_time, _charge_speed, _retreat_speed
        , _healing_rate, _healing_limit;
    float _initial_speed, _initial_HP;
    public int summoned_enemy_onlowhp = 8;
    bool has_summoned_lowhp, _is_charging;

    Summoner summon;
    Rage rage;
    /*
     * Note:
     * _attack_window yang di game_character dibuat sebagai chargeattack window  
     */
    protected override void Update()
    {
        base.Update();
        //Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
        if (is_retreating == true)
        {
            if(current_point >= path.vectorPath.Count)//lek ws teko nde healing spot
            {         
                _is_healing = true;
                is_retreating = false;
            }
        }
        else if (_is_healing)
        {
            _move_speed = 0;
            heal();
        }
        else
        {
            if (_hp <= _initial_HP * 10 / 100 && !has_summoned_lowhp)
            {
                summon.on_buildup_action(summoned_enemy_onlowhp);
                has_summoned_lowhp = true;
                retreat();
                is_retreating = true;
            }
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
                            _is_charging = true;
                            _charge_window = Time.time + _charge_active_time;
                            _move_speed = _charge_speed;
                            float temp = Time.time + _chargeattack_rate;
                            attack_window = temp;
                            anim.SetBool("charging", _is_charging);
                        }
                    }
                    //closerange attack
                    else if (_closeattack_window <= Time.time && _charge_window < Time.time && !is_retreating)
                    {
                        //if in range of close attack and within attack window
                        if (range_between_object_and_target <= _closeattack_range)
                        {
                            float temp = Time.time + _closeattack_rate;
                            _closeattack_window = temp;

                            IAttackable player = target.GetComponent<IAttackable>();
                            Vector2 dir = ((enemypos - current_pos));
                            Vector2 knockback_force = dir.normalized * _closeattack_force;//supaya lebih rendah efek charge dan close
                            /*
                             * attack diganti ke state supaya lebih apik anim e
                             */
                            anim.SetTrigger("attack");
                            store_atk_info(player, knockback_force, _closeattack_damage);
                            
                            Debug.Log("Close Hit");
                        }
                    }
                    //check collsion when charging by lala
                    if (_charge_window >= Time.time)
                    {
                        check_collision_when_charging(enemypos, current_pos);
                    }
                    else if (_charge_window <= Time.time && _is_charging == true)//brati wes berakhir charge time e
                    {
                        _move_speed = _initial_speed;
                        _is_charging = false;
                        anim.SetBool("charging", _is_charging);
                    }
                }
            }            
        }
        
    }

    protected override void Awake()
    {
        base.Awake();
        _initial_speed = _move_speed;
        summon = GetComponent<Summoner>();
        _initial_HP = _hp;
        has_summoned_lowhp = false;
        rage = GetComponent<Rage>();
        is_retreating = false;
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

    //protected override void OnTriggerStay2D(Collider2D collision)// enemy in range
    //{
    //    if (!target)
    //    {
    //        return;
    //    }
    //    else if ((target.gameObject == container_gameobject.gameObject || target == null) 
    //        && collision.gameObject.GetComponent<Player>() && !is_retreating)
    //    {
    //        target = collision.gameObject.GetComponent<Transform>();
    //        is_targeting_player = true;
    //    }
    //}
    //unfollow player when out of range
    protected override void OnTriggerExit2D(Collider2D collision)// enemy out of range
    {
        if (!target)
        {
            return;
        }
        else if (collision.gameObject == target.gameObject)
        {
            Debug.Log("TRIGGER EXIT");
            target = null;
            is_targeting_player = false;
        }
    }

    public override void attack(IAttackable attacked_object, Vector2 knockback_force, float damage)//onhit enemy
    {
        base.attack(attacked_object, knockback_force, damage);
        attacked_object.take_damage(damage, knockback_force);
    }

    protected int check_attack_readiness()
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
        if (col.IsTouching(target.gameObject.GetComponent<BoxCollider2D>()))//check with velocity if velocity great then charging
        {
            IAttackable player = target.GetComponent<IAttackable>();
            Vector2 dir = ((target_pos - cur_pos));
            Vector2 knockback_force = dir.normalized * force;
            attack(player, knockback_force, _damage);
        }
    }

    public override void take_damage(float _damage, Vector2 force)
    {
        base.take_damage(_damage, force);
        summon.add_buildup(_damage);
        rage.add_buildup(_damage);
        if (_is_healing)
        {
            _is_healing = false;
            _move_speed = _initial_speed;
        }
    }

    void retreat()
    {
        target = GameObject.FindGameObjectWithTag("Sleep_Spot").transform;
        is_targeting_player = false;
        _move_speed = _retreat_speed;
    }

    void heal()
    {
        if(_hp <= _healing_limit)
        {
            _hp += _healing_rate;
            _move_speed = 0;
        }
        else
        {
            _is_healing = false;
            _move_speed = _initial_speed;
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
