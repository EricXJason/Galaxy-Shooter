using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 1f;
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

    void OnCollisionEnter(Collision other)
    {
        //接觸玩家後直接擊殺玩家
        /*if(other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }*/
        if(other.gameObject.tag == "Laser")
        {
            Destroy(this.gameObject);
            print("hit");
        }
    }

    void Movement()
    {
        transform.Translate(0, Speed * Time.deltaTime *  -1, 0);
    }

    public void DestoryEnemy()
    {
        if (transform.position.y <= -8f)
        {
            Destroy(this.gameObject);
        }
    }

    public void ReSpawn()
    {
        float RandomX = Random.Range(-8f, 8f);
        if(transform.position.y <= -8f)
        {
            transform.position = new Vector3(RandomX, 8f, transform.position.z);
        }
    }
}
