using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackGuidComponent : MonoBehaviour
{
    public Guid ID;
    // Start is called before the first frame update
    void Awake()
    {
        ID = Guid.NewGuid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
