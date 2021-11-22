using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _hp = 100;

    public void GetDamage(int damage)
    {
        _hp -= damage;
    }
}
