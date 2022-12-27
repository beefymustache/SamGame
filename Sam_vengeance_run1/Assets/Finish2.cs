using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish2 : MonoBehaviour
{
    private AudioSource finishSound;
    private bool levelDone = false;
    void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelDone)
        {
            finishSound.Play();
            levelDone = true;
            Invoke("levelEnding", 2f);
        }
    }

    private void levelEnding()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

}
