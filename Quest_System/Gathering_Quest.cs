﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gathering_Quest : Quest
{
    [SerializeField]
    List<Quest_Item> quest_item = new List<Quest_Item>();

    public List<Quest_Item> Quest_item { get => quest_item; set => quest_item = value; }

    public override void check_if_complete()
    {
        base.check_if_complete();

    }
}
