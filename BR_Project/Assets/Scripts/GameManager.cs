using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public void MoveMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void MoveChooseBoss()
    {
        SceneManager.LoadScene(1);
    }
    
    public void MoveLion()
    {
        SceneManager.LoadScene(2);
    }
    public void MoveTinWoodMan()
    {
        SceneManager.LoadScene(3);
    }
    public void MoveScareCrow()
    {
        SceneManager.LoadScene(4);
    }

}
