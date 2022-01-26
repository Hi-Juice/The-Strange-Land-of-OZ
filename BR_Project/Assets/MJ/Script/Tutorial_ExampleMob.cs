using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_ExampleMob : MonoBehaviour
{
    int hp = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            hp--;
            if(hp <= 0)
            {
                GameObject.FindObjectOfType<TutorialTextManager>().SetBool_isDelay(false);
                Destroy(this.gameObject);
            }
        }
    }
}
