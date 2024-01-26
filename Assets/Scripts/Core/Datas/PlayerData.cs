using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Data/PlayerData", order = int.MinValue)]
public class PlayerData : BaseData
{
    public int infection;
    public int attack;
    public int attackSpeed;
}
