using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject pointer_in_map, inventory_UI, quest_UI, quest_list_UI;
    public Camera map_cam, std_camera;
    bool map_status, inventory_is_open, quest_is_open;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        //std_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //map_cam = GameObject.FindGameObjectWithTag("Map Camera").GetComponent<Camera>();
        map_status = false;
        inventory_is_open = false;
    }

    #region open-close map
    private void OpenMap()
    {
        std_camera.gameObject.SetActive(false);
        map_cam.gameObject.SetActive(true);
        map_status = true;
    }
    private void CloseMap()
    {
        std_camera.gameObject.SetActive(true);
        map_cam.gameObject.SetActive(false);
        map_status = false;
    }

    public void open_inventory()
    {
        inventory_is_open = true;
        inventory_UI.GetComponent<Canvas>().enabled = true;
    }

    public void close_inventory()
    {
        inventory_is_open = false;
        inventory_UI.gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void open_quest()
    {
        quest_is_open = true;
        quest_UI.GetComponent<Canvas>().enabled = true;
    }

    public void close_quest()
    {
        quest_is_open = false;
        quest_UI.GetComponent<Canvas>().enabled = false;
    }

    public void open_quest_list()
    {
        quest_list_UI.GetComponent<Canvas>().enabled = true;
    }

    public void close_quest_list()
    {
        quest_list_UI.GetComponent<Canvas>().enabled = false;
    }

    public void toggle_map()
    {
        if (map_status)
        {
            CloseMap();
        }
        else
        {
            OpenMap();
        }
    }

    public void toggle_inventory()
    {
        if (inventory_is_open)
        {
            close_inventory();
        }
        else
        {
            open_inventory();
        }
    }

    public void toggle_quest()
    {
        if (quest_is_open)
        {
            close_quest();
        }
        else
        {
            open_quest();
        }
    }

    #endregion
}
