using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public void justQuit()
    {
        SceneManager.LoadScene(0);
    }

}
