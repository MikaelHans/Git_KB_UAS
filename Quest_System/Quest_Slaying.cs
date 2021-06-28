using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Slaying : Quest
{
    public override void init()
    {
        //inisialisasi quest objective monster soale unity retard, prefab gaonok start
        foreach (Quest_Objective qbj in Quest_objective)
        {
            qbj.init();
        }
    }
}
