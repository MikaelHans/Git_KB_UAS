﻿using System.Collections;
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

    public virtual bool check_if_complete()
    {
        return false;
    }

    public virtual int completed()
    {

        return 0;
    }

    private void Awake()
    {
        
    }
}
