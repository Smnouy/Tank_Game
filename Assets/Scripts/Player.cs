using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 3f;

    //引用
    private SpriteRenderer sr;
    public Sprite[] TankSpirte;

    // Start is called before the first frame update
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * MoveSpeed * Time.deltaTime, Space.World);
        if (v < 0)//下
        {
            sr.sprite = TankSpirte[2];
        }
        else if (v > 0)//上
        {
            sr.sprite = TankSpirte[0];
        }
        if (v != 0)
        {
            return;
        }
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * MoveSpeed * Time.deltaTime,Space.World);
        if (h < 0)//左
        {
            sr.sprite = TankSpirte[3];
        }
        else if (h > 0)//右
        {
            sr.sprite = TankSpirte[1];
        }
       

    }
}
