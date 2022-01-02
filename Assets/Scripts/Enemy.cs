using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float Speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ReSpawn();
        //DestoryEnemy();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("hit " + other.name);
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            //檢查Player這個Component是否存在
            if (player != null)
            {
                player.Damage();
            }
            //銷毀自身(敵人)
            Destroy(this.gameObject);
        }
        if(other.tag == "Laser")
        {
            //銷毀碰撞敵人後的子彈(碰撞物)
            Destroy(other.gameObject);
            //銷毀敵人(自身)
            Destroy(this.gameObject);
        }
        
    }
    void Movement()//敵人飛船的向下移動
    {
        transform.Translate(0, Speed * Time.deltaTime *  -1, 0);
    }

    void DestoryEnemy()//超過範圍消毀自身
    {
        if (transform.position.y <= -8f)
        {
            Destroy(this.gameObject);
        }
    }

    void ReSpawn()//超過範圍後於重生(X軸為隨機)
    {
        float RandomX = Random.Range(-8f, 8f);
        if(transform.position.y <= -8f)
        {
            transform.position = new Vector3(RandomX, 8f, transform.position.z);
        }
    }
}
