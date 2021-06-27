using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGoing_Quest_UI : MonoBehaviour
{
    //content here as in scrollview content
    public GameObject content;
    public GameObject prefab;
    protected Player player;

    public Player Player { get => player; set => player = value; }

    public virtual void add_ongoing_quest()
    {
        Instantiate(prefab, content.transform);
        GetComponentInChildren<OnGoing_Quest>().setup(player);
    }

    public virtual void clear_all()
    {
        GetComponent<Canvas>().enabled = false;
        //OnGoingQuest[] child = GetComponentsInChildren<OnGoingQuest>();
        foreach(OnGoing_Quest child in GetComponentsInChildren<OnGoing_Quest>())
        {
            Destroy(child.gameObject);
        }
    }

    public virtual void setup(Player _player)
    {
        player = _player;
        //foreach(Quest quest in player.Active_quest)
        //{

        //}
        add_ongoing_quest();
    }

    public void close_ui()
    {
        clear_all();
        GetComponentInChildren<Canvas>().enabled = false;
    }
}
