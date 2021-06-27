using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacking
{
    void attack(IAttackable attacked_object, Vector2 knockback_force, float damage);
}