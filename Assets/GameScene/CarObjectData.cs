using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCarData", menuName = "Scriptable/CarData")]
public class CarObjectData : ScriptableObject
{
    public GameObject cartPrefab;
    public int cartLevel;
    public int cartHp;
    public int cartAttack;
    public float reCharging;
    public int upgradeCost;
}
