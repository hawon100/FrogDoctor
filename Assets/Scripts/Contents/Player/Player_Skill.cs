using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Skill : MonoBehaviour
{
    [SerializeField] private Image syringe;
    [SerializeField] private Image change;

    [SerializeField] private Text syringeCoolTimeCounter;
    [SerializeField] private Text changeCoolTimeCounter;

    [SerializeField] private float syringeCoolTime;
    [SerializeField] private float changeCoolTime;

    [SerializeField] private float syringeCurrentCoolTime;
    [SerializeField] private float changeCurrentCoolTime;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
