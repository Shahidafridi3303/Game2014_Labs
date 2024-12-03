using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHazards : MonoBehaviour, IDamage
{
    [SerializeField] private int _damage = 30;

    public int Damage()
    {
        return _damage;
    }
}
