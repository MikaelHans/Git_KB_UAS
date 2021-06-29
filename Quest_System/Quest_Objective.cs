using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Objective : MonoBehaviour
{
    [SerializeField]
    protected GameObject _objective;
    [SerializeField]
    protected int count;
    public string _name;
    public int Count { get => count; set => count = value; }
    public GameObject Objective { get => _objective; set => _objective = value; }


    public virtual int completed()
    {
        return 0;
    }

    public virtual void complete_quest() { }

    public virtual void init() { }

}
