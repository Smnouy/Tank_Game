using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //属性值
    public int PlayerLifeVal = 3;
    public int PlayerScore = 0;
    public bool PlayerIsDead;
    public bool IsDefeat;
    //引用
    public GameObject Born;
    public Text PlayerScoreText;
    public Text PlayerLifeValText;
    public GameObject IsDefeatUI;
    //单例
    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDefeat)
        {
            IsDefeatUI.SetActive(true);
            Invoke("ReturnToMain", 3);
            return;
        }
        if(PlayerIsDead)
        {
            Recover();
        }
        PlayerScoreText.text = PlayerScore.ToString();
        PlayerLifeValText.text = PlayerLifeVal.ToString();
    }

    private void Recover()
    {
        if (PlayerLifeVal <= 0)
        {
            IsDefeat = true;
            //游戏失败返回主界面
           
        }
        else
        {
            PlayerLifeVal--;
            GameObject go = Instantiate(Born,new Vector3(-2,-8,0),Quaternion.identity);
            go.GetComponent<Born>().CreatPlayer = true;
            PlayerIsDead = false;
        }
    }

    private void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
}
