using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Photon_Sync_HP : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private Transform ThisObjTransform;

    Vector3 incomingPosContainer = new Vector3();

    [SerializeField]
    private float lerp_speed;
    float new_hp_data;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.ThisObjTransform.gameObject.GetComponent<Game_Character>().Get_Hp());
        }
        else
        {
            new_hp_data = (float)stream.ReceiveNext();
        }
    }
    private void Awake()
    {
        ThisObjTransform = gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        if (!this.photonView.IsMine)//update postion through the network
        {
            ThisObjTransform.gameObject.GetComponent<Game_Character>().set_hp(new_hp_data);
            //Debug.Log(new_hp_data);
        }
    }
}
