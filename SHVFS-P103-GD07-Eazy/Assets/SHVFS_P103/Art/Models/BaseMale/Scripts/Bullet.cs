using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody BulletRigid;
    public float ShotSpped;  
    // Start is called before the first frame update
    void Start()
    {
        if (BulletRigid)
        {
            BulletRigid.velocity = transform.forward * ShotSpped;
        }


    }

    // Update is called once per frame
    
}
