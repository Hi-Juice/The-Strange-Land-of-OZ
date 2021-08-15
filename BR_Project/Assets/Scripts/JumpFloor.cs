using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFloor : MonoBehaviour
{
    public GameObject player;
    public BoxCollider2D Floor;
    public bool isGround = true;

    void Start()
    {
        player = GameObject.Find("Player");
    }
    
    void Update()
    {
        if (player.transform.position.y > transform.position.y)
        {
            Floor.enabled = true;
        
        }
        else
        {
            Floor.enabled = false;
        }
    }

}
