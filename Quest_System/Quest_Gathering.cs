using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Gathering : Quest
{
    public override void complete_quest()
    {
        Player player = FindObjectOfType<Player>();
        Inventory playerinventory = player.get_inventory();
        foreach(Quest_Objective qbj in Quest_objective)
        {
            playerinventory.subtract_item(qbj);
        }
    }
}
