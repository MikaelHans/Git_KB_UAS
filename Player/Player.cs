using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : Game_Character, IAttackable, IAttacking
{
    #region public members
    public float f;
    public float horizontal_move, vertical_move, lerpSpeed, horizontal_state, vertical_state;

    #endregion

    #region private Seriazable members
    [SerializeField]
    public camera cam;
    [SerializeField]
    public int direction;
    [SerializeField]
    private float interact_range;
    [SerializeField]
    private float offset_x, offset_y, points;
    [SerializeField]
    private bool isFacingRight, isFacingUp, isIdle, has_active_quest;
    
    private Animator PlayerAnimator;
    [SerializeField]
    TextMeshPro playerName;
    [SerializeField]
    private Inventory player_inventory = new Inventory();
    [SerializeField]
    Quest active_quest;
    

    public bool Has_active_quest { get => has_active_quest; set => has_active_quest = value; }
    public Quest Active_quest { get => active_quest; set => active_quest = value; }

    #endregion
    protected override void Awake()
    {
        base.Awake();        
        DontDestroyOnLoad(this.gameObject);//when change scene, dont destroy on load
        direction = 1;
        Debug.Log(gameObject.GetComponent<Rigidbody2D>());
        f = 15f;
        PlayerAnimator = transform.GetComponent<Animator>();
        isIdle = true;
        playerName = gameObject.GetComponentInChildren<TextMeshPro>();
        player_inventory = GameObject.FindGameObjectsWithTag("Inventory_UI")[1].GetComponent<Inventory>();
        //DontDestroyOnLoad(GameObject.FindGameObjectsWithTag("Inventory_UI")[1].GetComponent<Inventory>());
        
        Has_active_quest = false;
       
       
    }

    protected override void Start()
    {
        base.Start();
        player_inventory = FindObjectOfType<Inventory>();
    }

    protected override void Update()
    {
        base.Update();
        get_input();
        SyncAnimation(horizontal_state, vertical_state);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected void get_movement_input()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        horizontal_move = Input.GetAxisRaw("Horizontal");
        vertical_move = Input.GetAxisRaw("Vertical");

        if (horizontal_move != 0 || vertical_move != 0)
        {
            isIdle = false;
            horizontal_state = horizontal_move;//direction where player is facing
            vertical_state = vertical_move;//direction where player is facing                
        }
        else
        {
            isIdle = true;
        }
        SyncAnimation(horizontal_state, vertical_state);
    }

    protected void get_interaction_input(RaycastHit2D hit)
    {
        if (hit.collider != null && Input.GetKeyDown(KeyCode.F))// juga player ingin berinteraksi
        {
            var result = hit.transform.gameObject;
            Debug.Log(result.name);
            if (result != null && result.GetComponent<Item>())//pick up item
            {
                player_inventory.add_item(result.GetComponent<Item>());
                Destroy(result.gameObject);
            }
            Interactables is_interactables = result.GetComponent<Interactables>();
            if (is_interactables != null)
            {
                is_interactables.interact(this);
            }
        }
    }

    protected void get_combat_input(RaycastHit2D hit)
    {
        if (Input.GetKeyDown(KeyCode.E))// attack
        {
            if (hit.collider != null)
            {
                var result = hit.transform.gameObject;
                Debug.Log(result.name);
                Vector2 knockback_force = new Vector2(horizontal_state * force, vertical_state * force);
                IAttackable check_if_attackable = result.GetComponent<IAttackable>();
                if (result != null && check_if_attackable != null)//attack enemy
                {
                    //check_if_attackable.take_damage(10, knockback_force);
                    attack(check_if_attackable, knockback_force, _damage);
                }
            }
            PlayerAnimator.SetTrigger("attack");
            PlayerAnimator.SetBool("is_attacking", true);
        }

    }

    protected void get_debug_input()
    {
        if (Input.GetKeyDown(KeyCode.O))// attack
        {
            Debug.Log("MINIGAME");
            //_chest_minigame.generate_grid(3,3, transform.position);
        }
    }

    protected void get_input()
    {
        //player direction
        Vector2 direction = new Vector2(horizontal_state, vertical_state).normalized;
        //draw debug ray for debugging purposes
        Debug.DrawRay(new Vector3(gameObject.transform.position.x + offset_x * horizontal_state, gameObject.transform.position.y + offset_y * vertical_state, gameObject.transform.position.z),
            direction * interact_range, Color.red, 0.0f);//raycast debug view since raycast is invisible
                                                         //contact filter karena enemy ada 2 collider, jdie collider yg trigger diabaikan
        Debug.DrawRay(new Vector3(gameObject.transform.position.x + offset_x * horizontal_state, gameObject.transform.position.y + offset_y * vertical_state, gameObject.transform.position.z),
            direction * _attack_range, Color.blue, 0.0f);

        ContactFilter2D rigidbody_filter = new ContactFilter2D();
        rigidbody_filter.useTriggers = false;
        //array untuk menyimpan yg terkena ray
        RaycastHit2D[] hit_result = new RaycastHit2D[10];
        Physics2D.Raycast(new Vector3(gameObject.transform.position.x + offset_x * horizontal_state, gameObject.transform.position.y + offset_y * vertical_state, gameObject.transform.position.z),
            direction, rigidbody_filter, hit_result, interact_range);//raycast
                                                                     //supaya code gk penuh ae, buat nyimpen obj pertama
        RaycastHit2D hit = hit_result[0];
        get_movement_input();
        get_interaction_input(hit);
        Physics2D.Raycast(new Vector3(gameObject.transform.position.x + offset_x * horizontal_state, gameObject.transform.position.y + offset_y * vertical_state, gameObject.transform.position.z),
            direction, rigidbody_filter, hit_result, _attack_range);//raycast
                                                                    //supaya code gk penuh ae, buat nyimpen obj pertama
        hit = hit_result[0];
        get_combat_input(hit);
        get_debug_input();
    }

    protected override void move()
    {
        base.move();
        Vector3 targetVelocity = new Vector2(horizontal_move, vertical_move) * _move_speed * Time.deltaTime;
        //m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        gameObject.transform.position += targetVelocity;
    }

    public void SyncAnimation(float _horizontal_state, float _vertical_state)
    {
        PlayerAnimator.SetBool("Is_Idle", isIdle);
        PlayerAnimator.SetInteger("Horizontal_Move", (int)_horizontal_state);
        PlayerAnimator.SetInteger("Vertical_Move", (int)_vertical_state);
    }

    public void take_damage(float _damage, Vector2 force)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        _hp -= _damage;
    }

    public string getUsername()
    {
        return PhotonNetwork.NickName;
    }

    public void set_is_attacking(bool state)
    {
        PlayerAnimator.SetBool("is_attacking", state);
    }

    public bool GetIdlingStatus()
    {
        return isIdle;
    }

    public void SetIdlingStatus(bool status)
    {
        isIdle = status;
    }

    public void attack(IAttackable attacked_object, Vector2 knockback_force, float damage)
    {
        //needs more work
        attacked_object.take_damage(damage, knockback_force);
        //attacked_object.take_damage(damage, knockback_force);
    }

    public bool accept_quest(Quest accepted_quest)
    {
        if (has_active_quest == false)
        {
            active_quest = accepted_quest;
            Has_active_quest = true;
            return true;
        }
        return false;
    }

    public bool finish_quest()
    {
        //if (has_active_quest == true)
        //{
        //    foreach (Quest item in active_quest.Quest_item)
        //    {
        //        player_inventory.subtract_item(item);
        //        active_quest = null;
        //    }
        //    //player_active_quest_UI.setup_UI(active_quest);
        //    Has_active_quest = false;
        //    return true;
        //}
        return false;
    }

    public Inventory get_inventory()
    {
        return player_inventory;
    }
}
