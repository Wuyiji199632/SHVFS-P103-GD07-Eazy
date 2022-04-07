using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    int EnemyHealth = 100;
    public bool BeHit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDies();
    }
    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag=="Bullet")
        {
            EnemyHealth -= 25;
            BeHit = true;
        }
    }
    void EnemyDies()
    {
        if(EnemyHealth<=0)
        {
            EnemyHealth = 0;
            Destroy(gameObject);
        }
    }
    
}
