using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class camera : MonoBehaviour
{
    public Transform player;
    public float smoothspeed;
    public Vector3 offset;
    private bool hasDebugLog;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        hasDebugLog = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {        
        try
        {
            Vector3 desiredposition = player.position + offset;
            Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredposition, smoothspeed * Time.deltaTime);
            transform.position = smoothedposition;
        }
        catch (UnassignedReferenceException e)
        {
            if(hasDebugLog == false)
            {
                Debug.LogWarning("Waiting To Join Room");
                hasDebugLog = true;
            }
        }  
    }

    #region public Functions
    public void setPlayer(GameObject playerObj)
    {
        player = playerObj.GetComponent<Transform>();
    }
    #endregion
}
