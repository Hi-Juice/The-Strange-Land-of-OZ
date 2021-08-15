using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Parent;
    private bool dir;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -5);
        if (dir == true)
        {
            Parent.transform.Translate(Vector3.right * 5f * Time.deltaTime);
        }
        else if (dir == false)
        {
            Parent.transform.Translate(Vector3.left * 5f * Time.deltaTime);
        }

        if (Parent.transform.position.x < -17f)
        {
            dir = true;
        }

        else if (Parent.transform.position.x > 17f)
        {
            dir = false; 
        }
    }
}
