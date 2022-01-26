using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionFoot : MonoBehaviour
{
    CameraEffectManager mainCamManager;
    // Start is called before the first frame update
    void Start()
    {
        mainCamManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraEffectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShakeCamera()
    {
        mainCamManager.VibrateForTime(0.7f); 
        SoundManager.Instance.Play_LionMeowSound();
    }
}
