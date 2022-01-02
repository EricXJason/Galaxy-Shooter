using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    float Timer_f = 0f;
    int Timer_I = 1;
    float HorizontalInput;
    float VerticalInput;
    [SerializeField] float Speed = 10f;//玩家移動速度
    [SerializeField] float LimitX = 8f;//X軸邊界
    [SerializeField] float LimitY = 4f;//Y軸邊界
    [SerializeField] GameObject LaserPrefab;//抓取Laser的Prefab
    [SerializeField] float LaserOffset = 1.2f;//子彈偏移本體的距離
    [SerializeField] float FireRate = 0.1f;//越低冷卻時間越短
    float CanFire = 0f;
    [SerializeField] int Lives = 3;//玩家生命值
    SpawnManager spawnManager;
    [SerializeField]GameObject LaserContainer;//生成子彈的容器

    void Start()//定義玩家初始位置
    {
        transform.position = new Vector3(0, -3.5f, 0);
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (spawnManager != null)
        {
            print("Can't access the component");
        }
    }

    void Update()
    {
        Timer();
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > CanFire)
        {
            FireLaser();
        }
    }

    void Timer()//簡易計時器
    {
        Timer_f = Time.time;
        Timer_I = Mathf.FloorToInt(Timer_f);
        //print(Timer_I);
        //GameObject.Find("Timer").GetComponent<Text>().text = "Timne:"+Timer_I;
    }

    void Movement()//玩家操作方式
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
            transform.position = new Vector3(LimitX, transform.position.y, 0);
        }
        else if(transform.position.x <= -1 * LimitX)
        {
            transform.position = new Vector3(-1 * LimitX, transform.position.y, 0);
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

    void FireLaser()//生成子彈
    {
        //生成子彈的座標，LaserOffset為偏離gameObject的距離，避免子彈於gameObject中間生成
        Vector3 LaserPosition = new Vector3(transform.position.x, transform.position.y + LaserOffset, transform.position.z);
        //定義射擊速度-藉由不斷增將FireRate加到CanFire變數中可以定義下可發射的時間
        CanFire = Time.time + FireRate;
        //生成子彈
        //Quaternion.identity為(0, 0, 0)
        GameObject NewLaser = Instantiate(LaserPrefab, LaserPosition, Quaternion.identity);
        NewLaser.transform.parent = LaserContainer.transform;
    }

    public void Damage()//扣除生命
    {
        Lives -= 1;
        print("Lives "+Lives);
        if (Lives < 1)
        {
            //停止產生敵人
            spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
