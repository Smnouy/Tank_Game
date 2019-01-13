using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr;
    public GameObject ExplosionPrefab;

    public Sprite BrokenSprite;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Die()
    {
        sr.sprite = BrokenSprite;
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
    }
}
