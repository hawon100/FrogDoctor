using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Data/EnemyData", order = int.MinValue)]
public class EnemyData : BaseData
{
    public int hp;
    public int infection;
}
