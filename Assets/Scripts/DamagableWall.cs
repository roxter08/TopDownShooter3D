using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableWall : MonoBehaviour, IDamagable
{
    public void TakeDamage(int damageXP)
    {
        //Play some effect and then destroy
       Destroy(gameObject);
    }
}
