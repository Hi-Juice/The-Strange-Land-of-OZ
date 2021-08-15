using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * 5.0f * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * 5.0f * Time.deltaTime);
        }
    }
}
