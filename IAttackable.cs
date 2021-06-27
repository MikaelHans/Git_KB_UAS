using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public interface IAttackable 
{
    [PunRPC]
    void take_damage(float _damage, Vector2 force);
    
}

