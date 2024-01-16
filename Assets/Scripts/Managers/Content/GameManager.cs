using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMovement player;
    public Image infectionSlider;
    public GameObject ItemWindow;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        infectionSlider.fillAmount = Player_Frog.Instance.infection / (float)Player_Frog.Instance.maxInfection;
    }
}
