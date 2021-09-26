using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour
{
    public void StartLevel1()
    {
        SceneManager.LoadScene("Beach 'scape");
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("City 'scape");
    }

    public void StartLevel3()
    {
        SceneManager.LoadScene("Forest 'scape");
    }
}
