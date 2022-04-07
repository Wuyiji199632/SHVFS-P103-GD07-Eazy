using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackableComponent : MonoBehaviour
{
    public Guid GUID;
    public AttackGuidComponent AttackGuidComponent;
    public Guid guid => AttackGuidComponent.ID;
    // Start is called before the first frame update
    //void Start()
    //{
    //    GUID = AttackGuidComponent.ID;
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
