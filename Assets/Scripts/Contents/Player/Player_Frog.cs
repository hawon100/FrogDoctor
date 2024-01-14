using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Frog : PlayerController
{
    [SerializeField] private int hp;
    [SerializeField] private int maxHp;
    [SerializeField] private int attack;
    [SerializeField] private int attackSpeed;
    [SerializeField] private int infectionRate;

 

    private void Start()
    {
        hp = Managers.Data.LoadData<PlayerData>("Player/P_Frog").hp;
        maxHp = Managers.Data.LoadData<PlayerData>("Player/P_Frog").maxHp;
        attack = Managers.Data.LoadData<PlayerData>("Player/P_Frog").attack;
        attackSpeed = Managers.Data.LoadData<PlayerData>("Player/P_Frog").attackSpeed;
        infectionRate = Managers.Data.LoadData<PlayerData>("Player/P_Frog").infectionRate;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Debug.Log("Game Over!");
        }
    }

    protected override void OnDamage(int value)
    {
        hp -= value;
    }
}