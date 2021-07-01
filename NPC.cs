using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class NPC : MonoBehaviour, Interactables
{
    [SerializeField]
    protected string _name;
    [SerializeField]
    public List<string> conversation_dialogues = new List<string>();
    [SerializeField]
    List<NPC_Path> movepath = new List<NPC_Path>();
    public Dialogue_UI dialogue_UI_Script;
    public Canvas dialogue_canvas;
    public Seeker seeker;

    [SerializeField]
    protected Path path;
    protected int current_point;
    public Transform target;
    public float next_waypoint_trigger, _move_speed;
    protected Player _player;


    protected virtual void Start()
    {
        dialogue_UI_Script = GameObject.FindGameObjectWithTag("Dialogue_UI").GetComponent<Dialogue_UI>();
        dialogue_canvas = dialogue_UI_Script.GetComponent<Canvas>();

        //seeker = gameObject.GetComponent<Seeker>();
        //InvokeRepeating("update_path", 0f, 0.5f);
        //current_point = 0;
    }

    protected virtual void update_path()
    {
        if (target != null)
            seeker.StartPath(transform.position, target.position, on_path_complete);
    }

    protected virtual void on_path_complete(Path new_path)
    {
        if (!new_path.error)
        {
            path = new_path;
            current_point = 0;
        }
    }

    public virtual void interact(Player player)
    {
        //dialogue
        _player = player;
        dialogue_UI_Script.display_UI(this);
    }

    private void Update()
    {
        //if(movepath.Count > 0)
        //{
        //    //move to path
        //    move();
        //}
    }

    void move()
    {
        if (current_point >= path.vectorPath.Count)
        {
            target = null;
            return;
        }
        Vector2 direction = ((Vector3)path.vectorPath[current_point] - transform.position).normalized;
        Vector3 targetVelocity = direction * _move_speed * Time.deltaTime;
        gameObject.transform.position += targetVelocity;

        float distance = Vector2.Distance(transform.position, path.vectorPath[current_point]);
        if (distance < next_waypoint_trigger)
        {
            if (current_point + 1 < path.vectorPath.Count)
            {
                current_point++;
            }
        }
    }
}
