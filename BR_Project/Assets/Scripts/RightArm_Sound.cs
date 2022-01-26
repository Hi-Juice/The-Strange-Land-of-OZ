using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArm_Sound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void play_AxeSound()
    {
        SoundManager.Instance.Play_TinWoodManAxeSound();
    }

}
