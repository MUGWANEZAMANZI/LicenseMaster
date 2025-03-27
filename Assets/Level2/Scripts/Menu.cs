using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void goToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void goToLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void goToLevel2()
    {
        SceneManager.LoadScene(2);
    }
}
