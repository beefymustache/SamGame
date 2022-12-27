using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class playerDeets : MonoBehaviour
{
    public int startingHealth;
    private float currentHealth;
    public int fallBoundary = -25;
    public GameObject playerr;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private float iframeTime;
    private Animator anim;
    private int coins;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private AudioSource collectSounFX;
    

    private void Start()
    {
        playerr = GameObject.Find("Player");
        startingHealth = 100;
        totalHealthBar.fillAmount = startingHealth / startingHealth;
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        coins = 0;

    }

    private void Update()
    {
        currentHealthBar.fillAmount = currentHealth / 100;
        if (transform.position.y < fallBoundary)
            DamagePlayer(999);
        if (Input.GetKeyDown(KeyCode.E))
        {
            DamagePlayer(19);

        }
    }


    public void DamagePlayer(float _damage)
    {
        currentHealth -= _damage;
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine (Ifarm());
        }
        if (currentHealth <= 0)
        {
            anim.SetTrigger("Die");
            Invoke(nameof(KillPlayer), .5f);
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void KillPlayer()
    {
        Destroy(playerr);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator Ifarm()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        yield return new WaitForSeconds(iframeTime);
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            //collectSounFX.Play();
            Destroy(collision.gameObject);
            coins++;
            coinsText.text = "Coins: " + coins;
        }
    }

}
