using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilFlooding : MonoBehaviour
{
    public bool isPatten = false;
    public bool dir = true; // true = 위, false = 아래



    // Update is called once per frame
    void Update()
    {
        if(isPatten == true)
        {
            if (dir == true)
            {
                transform.Translate(Vector3.up * 0.5f * Time.deltaTime);
            }
            else if (dir == false)
            {
                transform.Translate(Vector3.down * 0.5f * Time.deltaTime);
            }

            if (transform.position.y > -4.5f)
            {
                dir = false;
            }
            else if (transform.position.y < -5f)
            {
                dir = true;
            }
        }
        else
        {
            if(transform.position.y > -6.8f)//오일 내리기
            {
                transform.Translate(Vector3.down * 1 * Time.deltaTime);
            }
        }
    }
}
