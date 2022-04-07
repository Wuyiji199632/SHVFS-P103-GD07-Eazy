using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Class3
{
    [System.Serializable]   
        public class Team:MonoBehaviour
        {
            public int ID;
            public List<ScoreComponent> Members;
            public int Score;
        //public int Team1Score;
        //public int Team2Score;
        //public int Team3Score;
        private void Awake()
        {
            var scoreComponents = FindObjectsOfType<ScoreComponent>();
            foreach (var scoreComponent in scoreComponents)
            {
                Score = scoreComponent.GetComponent<ScoreComponent>().Score;
                ID = scoreComponent.GetComponent<ScoreComponent>().TeamID;
            }
        }
        public Team(int id, List<ScoreComponent> members, int score)
            {
                ID = id;
                Members = members;
                Score = score;

            }
        }
    //public class Team2
    //{
    //    public int ID;
    //    public List<ScoreComponent> Members;
    //    public int Score;
    //    //public int Team1Score;
    //    //public int Team2Score;
    //    //public int Team3Score;
    //    public Team2(int id, List<ScoreComponent> members, int score)
    //    {
    //        ID = id;
    //        Members = members;
    //        Score = score;

    //    }
    //}
    //public class Team3
    //{
    //    public int ID;
    //    public List<ScoreComponent> Members;
    //    public int Score;
    //    //public int Team1Score;
    //    //public int Team2Score;
    //    //public int Team3Score;
    //    public Team3(int id, List<ScoreComponent> members, int score)
    //    {
    //        ID = id;
    //        Members = members;
    //        Score = score;
    //    }
    //}
}


