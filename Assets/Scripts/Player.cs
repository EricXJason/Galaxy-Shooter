using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float HorizontalInput;
    float VerticalInput;
    public float Speed = 10f;
    [SerializeField] float LimitX = 8f;
    [SerializeField] float LimitY = 4f;
    [SerializeField] GameObject Laser;
    [SerializeField] float LaserOffset = 1f;//子彈偏移本體的距離
    [SerializeField] float FireRate = 0.1f;//越低冷卻時間越短
    float CanFire = 0f;
    [SerializeField] int Lives = 3;

    void Start()
    {
        //玩家初始位置
        transform.position = new Vector3(0, -3.5f, 0);
    }

    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > CanFire)
        {
            FireLaser();
        }
    }

    void Movement()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        Vector3 Direction = new Vector3(HorizontalInput, VerticalInput, 0);
        /*float PlayerX = HorizontalInput * Time.deltaTime* Speed;
        float PlayerY = VerticalInput * Time.deltaTime* Speed;
        transform.Translate(PlayerX, PlayerY, 0);*/
        transform.Translate(Direction* Time.deltaTime* Speed);
        /*if (transform.position.x >= LimitX)
        {
            transform.position = new Vector3(6f, transform.position.y, 0);
        }
        else if(transform.position.x <= -1 * LimitX)
        {
            transform.position = new Vector3(-6f, transform.position.y, 0);
        }*/
        //以上為使用條件式去限制移動範圍的寫法，下方為使用Mathf.Clamp的精簡寫法
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, LimitX* -1, LimitX), transform.position.y,0);  

        /*if (transform.position.y >= LimitY)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if(transform.position.y <= LimitY * -1)
        {
            transform.position = new Vector3(transform.position.x, LimitY * -1, 0);
        }*/
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, LimitY * -1, LimitY),0); 
    }

    void FireLaser()
    {
        Vector3 LaserPosition = new Vector3(transform.position.x, transform.position.y + LaserOffset, transform.position.z);
        //定義射擊速度-藉由不斷增將FireRate加到CanFire變數中可以定義下可發射的時間
        CanFire = Time.time + FireRate;
        //Quaternion.identity為(0, 0, 0)
        Instantiate(Laser, LaserPosition, Quaternion.identity);
    }

    public void Damage()
    {
        Lives -= 1;
        print("Lives "+Lives);
        if (Lives < 1)
        {
            Destroy(this.gameObject);
        }
    }
}
