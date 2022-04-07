using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackerComponent : MonoBehaviour
{
    public AttackGuidComponent AttackGuidComponent;
    public Guid ID;
    public GameObject Attacker;
    public float AttackActiveTime;
    private float attackActiveTimer;
    private Guid guid;
    public float AttackPower;
    // Start is called before the first frame update
    private void OnEnable()
    {
       
        Attacker.SetActive(false);
        Debug.Log(ID);
    }
    void Start()
    {
        guid = AttackGuidComponent.ID;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackActiveTimer < 0)
        {
            attackActiveTimer = 0f;
        }
        attackActiveTimer -= Time.deltaTime;
        Attacker.transform.localScale = Vector3.one * attackActiveTimer / AttackActiveTime;
        attackActiveTimer -= Time.deltaTime;
        Attacker.transform.localScale = Vector3.one * attackActiveTimer / AttackActiveTime;
        if(attackActiveTimer>0f)
        {
            Attacker.SetActive(true);
            return;
        }
        Attacker.SetActive(false);
        if (!Input.GetMouseButtonDown(0)) return;
        
            attackActiveTimer = AttackActiveTime;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<AttackableComponent>()) return;       

        if (other.GetComponent<AttackableComponent>().GUID.Equals(guid)) return;
        Debug.Log(other.name);
        other.GetComponent<Rigidbody>().AddForce(-transform.up*AttackPower, ForceMode.Impulse);
    }
}
