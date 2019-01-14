using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    //选择的选项
    private int choice = 1;
    public Transform PosOne;
    public Transform PosTwo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            choice = 1;
            transform.position = PosOne.position;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            choice = 2;
            transform.position = PosTwo.position;

        }
        if(choice == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}
