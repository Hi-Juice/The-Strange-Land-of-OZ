using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArm_Sound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void play_AxeSound()
    {
        SoundManager.Instance.Play_TinWoodManAxeSound();
    }

}
