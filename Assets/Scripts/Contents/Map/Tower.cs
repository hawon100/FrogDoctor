using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject inputText;
    public AudioClip overSound;

    private void Update()
    {
        if(inputText.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                if(GameManager.instance.vaccineCount == 0)
                {
                    GameManager.instance.gameData.isLive = false;
                    GameManager.instance.gameOverText.SetActive(true);
                    Debug.Log("GameOver");
                    Managers.Sound.Play(overSound);
                }
                else
                {
                    ItemSelectActive();
                    Time.timeScale = 0.0f;
                    ItemController.Instance.SelectCard();
                    inputText.SetActive(false);
                }
            }
        }
    }

    private void ItemSelectActive()
    {
        GameManager.instance.gameData.isLive = true;
        GameManager.instance.ItemWindow.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inputText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inputText.SetActive(false);
        }
    }
}
