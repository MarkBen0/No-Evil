using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    public int health;
    public int attack;

    public void TakeDamage(int damage)
    {
        health -= damage;

        //if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributeManager>();

        if (atm != null)
        {
            atm.TakeDamage(attack);
        }
    }
}
