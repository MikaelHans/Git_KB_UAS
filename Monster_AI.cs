using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Photon.Pun;
using Photon.Realtime;

public class Monster_AI : MonoBehaviour
{
    #region private members
    Seeker path_seeker;
    Path path;
    Rigidbody2D ai_rigidbody;
    int current_point;
    [SerializeField]
    float _hp;
    #endregion
    #region public members
    public float random_movement_radius, move_speed;
    public Transform target;
    public float next_waypoint_trigger;
    public CircleCollider2D trigger_chase_radius;
    public GameObject container_gameobject;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        path_seeker = gameObject.GetComponent<Seeker>();
        ai_rigidbody = gameObject.GetComponent<Rigidbody2D>();
        InvokeRepeating("update_path", 0f, 0.5f);       
        current_point = 0;
        container_gameobject = new GameObject();
        _hp = 100f;
    }

    #region pathfinding section
    void update_path()
    {
        if (target != null)
            path_seeker.StartPath(ai_rigidbody.position, target.position, on_path_complete);
        //else
        //{
        //    Debug.Log("tes");
        //}
    }

    public float Get_Hp()
    {
        return _hp;
    }

    public void set_hp(float hp)
    {
        _hp = hp;
    }

    void on_path_complete(Path new_path)
    {
        if (!new_path.error)
        {
            path = new_path;
            current_point = 0;
        }
    }

    void choose_random_spot()
    {
        Vector3 random_position = (Vector3)Random.insideUnitCircle * random_movement_radius;
        random_position += transform.position;
        container_gameobject.transform.position = new Vector3(0, 0, 0);
        container_gameobject.transform.position = random_position;
        target = container_gameobject.transform;
    }
    #endregion

    #region player interaction functions

    public void take_damage(float _damage, Vector2 force)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        _hp -= _damage;
    }

    public void die()
    {
        Instantiate(Resources.Load("NPC Prefabs/key"), transform.position, transform.rotation);
        Destroy(container_gameobject);
        PhotonNetwork.Destroy(this.gameObject);
        //Destroy(this.gameObject);
    }

    #endregion

    private void Update()
    {
        if(_hp <= 0)
        {
            die();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null || target == null)
        {
            choose_random_spot();
            return;
        }
        if(current_point >= path.vectorPath.Count)
        {
            target = null;
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[current_point] - ai_rigidbody.position).normalized;
        Vector2 force = direction * move_speed * Time.deltaTime;

        ai_rigidbody.AddForce(force);

        float distance = Vector2.Distance(ai_rigidbody.position, path.vectorPath[current_point]);
        if(distance < next_waypoint_trigger)
        {
            current_point++;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!target)
        {
            return;
        }
        else if ((target.gameObject == container_gameobject.gameObject || target == null))
        {
            target = collision.gameObject.GetComponent<Transform>();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!target)
        {
            return;
        }
        else if (collision.gameObject == target.gameObject)
        {
            Debug.Log("TRIGGER EXIT");
            target = null;
        }
    }

    private void OnDrawGizmos()
    {
        //UnityEditor.Handles.color = Color.red;
        //UnityEditor.Handles.DrawWireDisc(transform.position, new Vector3(0, 0, 1), random_movement_radius);
    }
}
