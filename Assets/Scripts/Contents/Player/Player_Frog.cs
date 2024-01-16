using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Frog : PlayerController
{
    public static Player_Frog Instance;

    [SerializeField] private PlayerData data;
    public int infection;
    public int maxInfection;

    public int attack;
    public int attackSpeed;

    private void Start()
    {
        Instance = this;

        infection = data.infection;
        maxInfection = 100;
        attack = data.attack;
        attackSpeed = data.attackSpeed;
    }

    private void Update()
    {
        if (infection >= 100)
        {
            infection = 100;
            Debug.Log("Game Over!");
        }
    }

    public override void OnDamage(int value)
    {
        infection += value;
    }
}