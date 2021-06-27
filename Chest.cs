using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour, Interactables
{
    Canvas chest_canvas;
    bool chest_is_closed;
    Item dropped_item;
    pinball_recall pinball_recall;
    private void Start()
    {
        chest_canvas = GameObject.FindGameObjectWithTag("Chest").GetComponentInChildren<Canvas>();
        chest_is_closed = true;
        pinball_recall = FindObjectOfType<pinball_recall>();
    }

    public void Finish_Game(bool isCorrect)
    {
        if (isCorrect)
        {
            Instantiate(Resources.Load("Items/key"), transform.position, transform.rotation);
        }        
        Destroy(gameObject);
    }

    public void open_chest(Player player)
    {
        #region old codes
        //chest_canvas.enabled = true;
        //chest_canvas.GetComponentInChildren<Button>().onClick.AddListener(get_item);
        //chest_is_closed = false;
        #endregion
        #region open minigame
        pinball_recall.Start_Game(3,3, player.transform.position, this);
        #endregion
    }

    public void interact(Player player)
    {
        open_chest(player);
    }
}
