using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PresentData", menuName = "Data/PresentData", order = int.MinValue)]
public class PresentData : BaseData
{
    public int score;

    public float infection;
    public int attack;
    public int attackSpeed;
    public bool shield;
    public bool revival;
    public bool skillSyringe;
    public bool skillChange;
    public float skillSyringeCoolTime;
    public float skillChangeCoolTime;

    public float enemyHp;
    public float enemyInfection;
    public float enemyInfectionSpeed;
    public float enemySpeed;
}
