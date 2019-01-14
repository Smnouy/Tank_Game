using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //属性值
    public float MoveSpeed = 3f;
    private Vector3 BulletEulerAngles;//子弹需要旋转的角度
    private float v;
    private float h;
    //计时器
    private float BulletTimeVal;//子弹发射间隔时间
    private float TimeValDirection;//改变方向的时间

    //引用
    private SpriteRenderer sr;
    public Sprite[] TankSpirte;
    public GameObject BulletPrefab;
    public GameObject ExplosiongPrefab;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }

    private void Update()
    {
        //攻击CD
        if (BulletTimeVal >= 2.0f)
        {
            Attack();

        }
        else
        {
            BulletTimeVal += Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        Move();
        //Attack();
    }
    //坦克的移动方法
    private void Move()
    {   //垂直
        if(TimeValDirection >= 1)
        {
            int num = Random.Range(0, 8);
            if(num > 5)//下
            {
                v = -1;
                h = 0;
            }
            else if(num == 0)//上
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)//左
            {
                h = -1;
                v = 0;
            }
            else//右
            {
                h = 1;
                v = 0;
            }
            TimeValDirection = 0;
        }
        else
        {
            TimeValDirection += Time.deltaTime;
        }
        
        transform.Translate(Vector3.up * v * MoveSpeed * Time.deltaTime, Space.World);
        if (v < 0)//下
        {
            sr.sprite = TankSpirte[2];
            BulletEulerAngles = new Vector3(0, 0, 180);
        }
        else if (v > 0)//上
        {
            sr.sprite = TankSpirte[0];
            BulletEulerAngles = new Vector3(0, 0, 0);
        }
        if (v != 0)
        {
            return;
        }

        //水平
        transform.Translate(Vector3.right * h * MoveSpeed * Time.deltaTime, Space.World);
        if (h < 0)//左
        {
            sr.sprite = TankSpirte[3];
            BulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)//右
        {
            sr.sprite = TankSpirte[1];
            BulletEulerAngles = new Vector3(0, 0, -90);
        }
    }
    //坦克的攻击方法
    private void Attack()
    {
        //子弹产生的角度 = 当前坦克的角度 + 子弹应该旋转的角度
        Instantiate(BulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + BulletEulerAngles));
        BulletTimeVal = 0;
    }
    //坦克的死亡方法
    private void Die()
    {
        PlayerManager.Instance.PlayerScore++;
        //产生爆炸特效
        Instantiate(ExplosiongPrefab, transform.position, Quaternion.identity);
        //死亡
        Destroy(gameObject);
    }
    //如果敌人互相触碰到立即旋转
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            TimeValDirection = 4;
        }
    }
}
