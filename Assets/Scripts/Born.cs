using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject[] EnemyPrefabList;
    public bool CreatPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 1.0f);
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BornTank()
    {
        if (CreatPlayer)
        {
            Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            int num = Random.Range(0, 2);
            if (num == 0)
            {
                Instantiate(EnemyPrefabList[0], transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(EnemyPrefabList[1], transform.position, Quaternion.identity);
            }
        }
        
    }
}
