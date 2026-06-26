using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour,IDamageable
{
    Player2D player;
    public float maxHP;
    public float currentHP;

    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider ghostSlider;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    float v;
    public float ghostSliderDuration = 1f;
    float timeTotal;
    float t;
    bool easeNow;

    void OnEnable()
    {
        player = FindAnyObjectByType<Player2D>();
    }

    void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        timeTotal = 0;
        maxHP = player.playerMaxHP;

        currentHP = maxHP;
        hpSlider.maxValue = maxHP;
        ghostSlider.maxValue = maxHP;

        hpSlider.value = maxHP;
        ghostSlider.value = maxHP;

        Regenerate();
    }

    void Update()
    {
        CappingHP();
        hpSlider.value = currentHP;
        EaseHP();
    }

    void CappingHP()
    {
        if(currentHP <= 0)
        {
            currentHP = 0;
        }
        if(currentHP >= 100)
        {
            currentHP = 100;
        }
    }


    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        v = ghostSlider.value;
        easeNow = true;
        timeTotal = 0;

        Death();
        
    }

    void EaseHP()
    {
        
        if(easeNow)
        {
            t = timeTotal / ghostSliderDuration;

            if(t>1)
            {
                easeNow = false;
                return;
            }


            ghostSlider.value = EaseOutCirc.easeOutCirc(v, currentHP, t);
            

            timeTotal += Time.deltaTime;

        }
        
    }

    void Death()
    {
        if (currentHP <= 0)
        {
            Debug.Log("Player is Dead");
            loseScreen.SetActive(true); 
        }
    }

    void Regenerate()
    {
        StartCoroutine(RegenerateOverTime());
    }

    IEnumerator RegenerateOverTime()
    {
        yield return new WaitForSeconds(1f);
        currentHP += 1f;
        Regenerate();
    }
  

}

public interface IDamageable
{
    void TakeDamage(float damage);
}
