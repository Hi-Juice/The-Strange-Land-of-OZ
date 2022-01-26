using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierBullet : MonoBehaviour
{
    Vector2[] point = new Vector2[4];
    bool hit = false;

    [SerializeField] [Range(0, 1)] private float t = 0;
    [SerializeField] public float speed = 5;
    [SerializeField] public float posA = 0.55f;
    [SerializeField] public float posB = 0.45f;

    public GameObject master;
    public GameObject enemy;

    public EffectManager effectManager;
    void Start()
    {
        if(enemy == null)
        {
            enemy = GameObject.Find("AttackPoint");
            effectManager = GameObject.Find("EffectManager").GetComponent<EffectManager>();
            if (enemy != null)

            {
                if (enemy.tag == "Obstacle")
                {
                    posB = 1; // »÷æÓ¡¸¿ª ¿˚∞‘ ∏∏µÍ
                }
            }
        }
        else
        {
            if (enemy.tag == "Obstacle")
            {
                posB = 1; // »÷æÓ¡¸¿ª ¿˚∞‘ ∏∏µÍ
            }
        }
        
        

        effectManager = GameObject.FindObjectOfType<EffectManager>();
        
        point[0] = master.transform.position; // P0
        point[1] = PointSetting(master.transform.position); // P1
        if(enemy != null)
        point[2] = PointSetting(enemy.transform.position); // P2
        

    }

    private void Update()
    {

        if (enemy != null)
        {
            point[3] = enemy.transform.position; // P3
            Vector2 direction = new Vector2(enemy.transform.position.x - this.transform.position.x, enemy.transform.position.y - this.transform.position.y);
            transform.right = direction;
        }   
        else
        {
            //Debug.Log("Bezier Bullet Destroy");
            GameObject vfx = effectManager.GetBulletEffect();
            vfx.transform.position = this.transform.position;
            Destroy(this.gameObject);

        }
    }

    void FixedUpdate()
    {
        if (t > 1) return;
        //if (hit) return;
        t += Time.deltaTime * speed;
        DrawTrajectory();
    }

    Vector2 PointSetting(Vector2 origin)
    {
        float x, y;

        x = posA * Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad)
            + origin.x;
        y = posB * Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad)
            + origin.y;
        return new Vector2(x, y);
    }

    void DrawTrajectory()
    {
        transform.position = new Vector2(
            FourPointBezier(point[0].x, point[1].x, point[2].x, point[3].x),
            FourPointBezier(point[0].y, point[1].y, point[2].y, point[3].y)
        );
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == enemy)
        {
            //hit = true;
            GameObject vfx = effectManager.GetBulletEffect();
            vfx.transform.position = this.transform.position;


            if(enemy.tag == "TinWood")
            {
                enemy.GetComponent<TinWoodman>().GetHit();
            }
            else if(enemy.tag == "ScareCrow")
            {
                enemy.GetComponent<ScareCrow>().GetHit();
            }
            else if(enemy.tag == "Lion")
            {
                enemy.GetComponentInParent<Boss_Lion>().GetHit();
            }
            Destroy(gameObject);
        }
        if(collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    private float FourPointBezier(float a, float b, float c, float d)
    {
        return Mathf.Pow((1 - t), 3) * a
            + Mathf.Pow((1 - t), 2) * 3 * t * b
            + Mathf.Pow(t, 2) * 3 * (1 - t) * c
            + Mathf.Pow(t, 3) * d;
    }


    //√‚√≥: https://tonikat.tistory.com/10 [Touniquet]
}
