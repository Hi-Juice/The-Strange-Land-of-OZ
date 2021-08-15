using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAlpha : MonoBehaviour
{

    Color color;
    public bool check_boolean;
    public SpriteRenderer SpriteRenderer_Light;

    bool boolValue = false;
    void Start()
    {
        SpriteRenderer_Light = GetComponent<SpriteRenderer>();

        color = SpriteRenderer_Light.color;
    }

    // Update is called once per frame

    private void Update()
    {
        if (boolValue == true)
        {
            color.a += 0.005f;
            SpriteRenderer_Light.color = color;
        }
        else
        {
            color.a -= 0.005f;
            SpriteRenderer_Light.color = color;
        }


        if (color.a > 2)
        {
            boolValue = false;
        }
        else if (color.a < 0)
        {
            boolValue = true;
        }

        //Debug.Log(color.a);
    }
}
