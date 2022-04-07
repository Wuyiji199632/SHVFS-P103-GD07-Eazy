using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Class3
{
    public class ScoreComponent : MonoBehaviour
    {
        public int TeamID;
        public bool ControlZone;
        public ScorableZoneComponent ControllingTheZone;
        public int Score;
        public ScoreComponent Team1Member;
        public ScoreComponent Team2Member;
        public ScoreComponent Team3Member;          
        // Start is called before the first frame update
        void Awake()
        {
            //TeamPointSystem.Instance.AddMembers(this);
            
        }
        private void OnDestroy()
        {
            TeamPointSystem.Instance.RemoveMembers(Team1Member,Team2Member,Team3Member);
            if(ControlZone)
            {
                ControllingTheZone.RemoveMembers(Team1Member,Team2Member,Team3Member);
                ControlZone = false;
                ControllingTheZone = null;
            }
        }
        // Update is called once per frame

        private void OnTriggerStay(Collider other)
        {
            if (!other.GetComponent<ScorableZoneComponent>()) return;
            {
                ControlZone = true;               
                ControllingTheZone = other.GetComponent<ScorableZoneComponent>();
                ControllingTheZone.AddTeamMembers(Team1Member, Team2Member, Team3Member);
                TeamPointSystem.Instance.ScorePoints(TeamID);
                TeamPointSystem.Instance.CheckForScoring();
                TeamPointSystem.Instance.CheckWinsAndLosses();
                //TeamPointSystem.Instance.GiveScores();
                var TeamSystems = FindObjectsOfType<TeamPointSystem>();
                foreach (var TeamSystem in TeamSystems)
                {
                    TeamSystem.GetComponent<TeamPointSystem>().m_RunTime++;
                }                
            }           
        }
        private void OnTriggerExit(Collider other)
        {
            if(ControllingTheZone)
            {
                ControllingTheZone.RemoveMembers(Team1Member, Team2Member, Team3Member);              
            }
            ControlZone = false;
            ControllingTheZone = null;
        }
        //public void OnTriggerEnter(Collider other)
        //{
        //    if (!other.GetComponent<ScorableZoneComponent>()) return;
        //    IsScoring = true;
        //}
        //public void OnTriggerExit(Collider other)
        //{
        //    if (!other.GetComponent<ScorableZoneComponent>()) return;
        //    IsScoring = false;
        //}

    }
    
}

