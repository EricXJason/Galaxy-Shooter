using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float Speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        DestoryLaser();
    }

    void DestoryLaser()//超過範圍消毀自身
    {
        if (transform.position.y >= 8f)
        {
            Destroy(this.gameObject);
            //Destroy(this.gameObject, 5f);
        }
    }

    void Movement()//子彈移動方式
    {
        transform.Translate(0, Speed * Time.deltaTime *  5f, 0);
    }
}
