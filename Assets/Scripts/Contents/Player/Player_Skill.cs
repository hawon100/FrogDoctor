using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Skill : MonoBehaviour
{
    public static Player_Skill Instance;

    [SerializeField] private Image syringeSkillFilter;
    [SerializeField] private Image changeSkillFilter;

    [SerializeField] private Text syringeCoolTimeCounter;
    [SerializeField] private Text changeCoolTimeCounter;

    public float syringeCoolTime;
    public float changeCoolTime;

    public float SyringeCoolTime { get =>  syringeCoolTime; set => syringeCoolTime = value; }
    public float ChangeCoolTime { get => changeCoolTime; set => changeCoolTime = value; }

    [SerializeField] private float syringeCurrentCoolTime;
    [SerializeField] private float changeCurrentCoolTime;

    private bool syringeCanUseSkill = true;
    private bool changeCanUseSkill = true;

    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform fireDirection;

    PlayerInput input;
    Controls controls = new Controls();

    private void Awake()
    {
        Instance = this;
        input = GetComponent<PlayerInput>();
    }

    void Start()
    {
        syringeSkillFilter.fillAmount = 0;
        changeSkillFilter.fillAmount = 0;
        syringeCoolTimeCounter.text = "";
        changeCoolTimeCounter.text = "";
        syringeCoolTime = GameManager.instance.gameData.skillSyringeCoolTime;
        changeCoolTime = GameManager.instance.gameData.skillChangeCoolTime;
    }

    private void Update()
    {
        controls = input.GetInput();
        if (GameManager.instance.syringeSkill.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                UseSkill("Syringe");
            }
        }
        if (GameManager.instance.changeSkill.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                UseSkill("Change");
            }
        }
    }

    private void UseSkill(string value)
    {
        if(value == "Syringe")
        {
            if (syringeCanUseSkill)
            {
                SkillSyringe();

                Debug.Log("Syringe Use Skill");
                syringeSkillFilter.fillAmount = 1;
                StartCoroutine(Cooltime("Syringe"));

                syringeCurrentCoolTime = syringeCoolTime;
                syringeCoolTimeCounter.text = "" + syringeCurrentCoolTime;

                StartCoroutine(CoolTimeCounter("Syringe"));

                syringeCanUseSkill = false;
            }
            else
            {
                Debug.Log("아직 스킬을 사용할 수 없스니다.");
            }
        }
        else if(value == "Change")
        {
            if (changeCanUseSkill)
            {
                SkillChange();

                Debug.Log("Change Use Skill");
                changeSkillFilter.fillAmount = 1;
                StartCoroutine(Cooltime("Change"));

                changeCurrentCoolTime = syringeCoolTime;
                changeCoolTimeCounter.text = "" + changeCurrentCoolTime;

                StartCoroutine(CoolTimeCounter("Change"));

                changeCanUseSkill = false;
            }
            else
            {
                Debug.Log("아직 스킬을 사용할 수 없스니다.");
            }
        }
    }

    private void SkillSyringe()
    {
        if (controls.HorizontalMove >= 0)
        {
            GameObject projectile = Instantiate(bullet, transform.position, Quaternion.identity);

            Rigidbody2D rigid = projectile.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector2.right * 10.0f, ForceMode2D.Impulse);
        }
        else if (controls.HorizontalMove <= 0)
        {
            GameObject projectile = Instantiate(bullet, transform.position, Quaternion.identity);

            Rigidbody2D rigid = projectile.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector2.left * 10.0f, ForceMode2D.Impulse);
        }
    }

    private void SkillChange()
    {
        Player_Frog.Instance.hSpeed += 120.0f / 100.0f;
    }

    private IEnumerator Cooltime(string value)
    {
        if(value == "Syringe")
        {
            while (syringeSkillFilter.fillAmount > 0)
            {
                syringeSkillFilter.fillAmount -= 1 * Time.smoothDeltaTime / syringeCoolTime;

                yield return null;
            }

            syringeCoolTimeCounter.text = "";
            syringeCanUseSkill = true;

            yield break;
        }
        else if( value == "Change")
        {
            while (changeSkillFilter.fillAmount > 0)
            {
                changeSkillFilter.fillAmount -= 1 * Time.smoothDeltaTime / changeCoolTime;

                yield return null;
            }

            changeCoolTimeCounter.text = "";
            changeCanUseSkill = true;

            yield break;
        }
    }

    private IEnumerator CoolTimeCounter(string value)
    {
        if (value == "Syringe")
        {
            while (syringeCurrentCoolTime > 0)
            {
                yield return new WaitForSeconds(1.0f);

                syringeCurrentCoolTime -= 1.0f;
                syringeCoolTimeCounter.text = "" + syringeCurrentCoolTime;
            }

            yield break;
        }
        else if (value == "Change")
        {
            while (changeCurrentCoolTime > 0)
            {
                yield return new WaitForSeconds(1.0f);

                changeCurrentCoolTime -= 1.0f;
                changeCoolTimeCounter.text = "" + changeCurrentCoolTime;
                if (changeCurrentCoolTime < 3)
                {
                    Player_Frog.Instance.hSpeed = 10f;
                }
                if(changeCurrentCoolTime % 2 == 0)
                {
                    Player_Frog.Instance.OnDamage(-1);
                }
            }

            yield break;
        }
    }
}
