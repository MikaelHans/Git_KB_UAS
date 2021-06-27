using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Position_Sync : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private Transform ThisObjTransform;

    Vector3 incomingPosContainer = new Vector3();

    [SerializeField]
    private float lerp_speed;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.ThisObjTransform.position.x);
            stream.SendNext(this.ThisObjTransform.position.y);
            stream.SendNext(this.ThisObjTransform.position.z);
        }
        else
        {
            incomingPosContainer = ThisObjTransform.position;
            float x = (float)stream.ReceiveNext();
            float y = (float)stream.ReceiveNext();
            float z = (float)stream.ReceiveNext();
            incomingPosContainer = new Vector3(x, y, z);
            #region old codes, might be useful
            //this.m_NetworkPosition = (Vector2)stream.ReceiveNext();

            //if (this.m_TeleportEnabled)
            //{
            //    if (Vector3.Distance(this.m_Body.position, this.m_NetworkPosition) > this.m_TeleportIfDistanceGreaterThan)
            //    {
            //        this.m_Body.position = this.m_NetworkPosition;
            //    }
            //}
            #endregion
        }
    }
    private void Awake()
    {
        ThisObjTransform = gameObject.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (!this.photonView.IsMine)//update postion through the network
        {
            this.ThisObjTransform.position = Vector3.Lerp(this.ThisObjTransform.position, this.incomingPosContainer, Time.deltaTime * lerp_speed);
        }
    }
}
