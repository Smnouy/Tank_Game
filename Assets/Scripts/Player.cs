using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //属性值
    public float MoveSpeed = 3f;
    private Vector3 BulletEulerAngles;//子弹需要旋转的角度
    private float BulletTimeVal;//子弹发射间隔时间
    private float DefendTimeVal = 3f;//保护时间
    private bool isDefend = true;//保护标志
    //引用
    private SpriteRenderer sr;
    public Sprite[] TankSpirte;
    public GameObject BulletPrefab;
    public GameObject ExplosiongPrefab;
    public GameObject DefendEffectPrefab;
    public AudioSource MoveAudio;
    public AudioClip[] TankAudio;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    private void Update()
    {
        //保护时间
        if (isDefend)
        {
            DefendEffectPrefab.SetActive(true);
            DefendTimeVal -= Time.deltaTime;
            if (DefendTimeVal < 0)
            {
                isDefend = false;
                DefendEffectPrefab.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
       
        if (PlayerManager.Instance.IsDefeat)
        {
            return;
        }
        Move();
        //攻击CD
        if (BulletTimeVal >= 0.4f)
        {
            Attack();

        }
        else
        {
            BulletTimeVal += Time.fixedDeltaTime;
        }
    }
    //坦克的移动方法
    private void Move()
    {   //垂直
        float v = Input.GetAxisRaw("Vertical");
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
        //播放音效
        if(Mathf.Abs(v) > 0.05f)
        {
            MoveAudio.clip = TankAudio[1];
           
            if (!MoveAudio.isPlaying)
            {
                MoveAudio.Play();
            }
        }
        else
        {
            MoveAudio.clip = TankAudio[0];

            if (!MoveAudio.isPlaying)
            {
                MoveAudio.Play();
            }
        }
        if (v != 0)
        {
            return;
        }
        //水平
        float h = Input.GetAxisRaw("Horizontal");
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
  
        if(Mathf.Abs(h) > 0.05f)
        {
            MoveAudio.clip = TankAudio[1];

            if (!MoveAudio.isPlaying)
            {
                MoveAudio.Play();
            }
        }
        else
        {
            MoveAudio.clip = TankAudio[0];

            if (!MoveAudio.isPlaying)
            {
                MoveAudio.Play();
            }
        }
    }
    //坦克的攻击方法
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //子弹产生的角度 = 当前坦克的角度 + 子弹应该旋转的角度
            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + BulletEulerAngles));
            BulletTimeVal = 0;
        }
    }
    //坦克的死亡方法
    private void Die()
    {
        if (isDefend)
        {
            return;
        }
        //玩家生命值减1
        PlayerManager.Instance.PlayerIsDead = true;
        //产生爆炸特效
        Instantiate(ExplosiongPrefab, transform.position, Quaternion.identity);
        //死亡
        Destroy(gameObject);
    }
}
