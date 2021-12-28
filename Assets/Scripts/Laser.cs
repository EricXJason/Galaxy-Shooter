using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float Speed = 10f;
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

    public void DestoryLaser()
    {
        if (transform.position.y >= 8f)
        {
            Destroy(this.gameObject);
            //Destroy(this.gameObject, 5f);
        }
    }

    public void Movement()
    {
        transform.Translate(0, Speed * Time.deltaTime *  5f, 0);
    }
}
