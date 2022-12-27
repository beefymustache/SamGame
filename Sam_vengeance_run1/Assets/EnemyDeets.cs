using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyDeets : MonoBehaviour
{
    public int enStartingHealth;
    public float enCurrentHealth;
    public int enFallBoundary = -25;
    public GameObject frenemy;
    public HealthBarBehavior HealthBar;
    private Animator anim;
    //[SerializeField] private Image totalHealthBar;
    //[SerializeField] private Image currentHealthBar;
    //[SerializeField] private float iframeTime;
   

    private void Start()
    {
        anim = GetComponent<Animator>();
        
        enCurrentHealth = enStartingHealth;
        
    }

    private void Update()
    {
        
        if (transform.position.y < enFallBoundary)
            DamageFrenemy(999);
        HealthBar.SetHealth(enCurrentHealth, enStartingHealth);

    }


    public void DamageFrenemy(float en_damage)
    {
        enCurrentHealth -= en_damage;
        HealthBar.SetHealth(enCurrentHealth, enStartingHealth);

        if (enCurrentHealth <= 0)
        {
            anim.SetTrigger("EnDeath");
            Invoke(nameof(KillFrenemy), .5f);
        }
    }

  

    public void KillFrenemy()
    {
        Destroy(frenemy);
        
    }

 
}