using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int score;
    public float waitToScore;
    private int currentScore;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        currentScore = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (waitToScore > 0)
        {
            waitToScore -= Time.deltaTime;
        }
        if (other.tag == "Bear")
        {
            if (waitToScore <= 0)
            {
                score++;
            }
        }
    }
}
   
