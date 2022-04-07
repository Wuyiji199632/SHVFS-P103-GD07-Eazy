using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Class3
{
    public class ScorableZoneComponent : MonoBehaviour
    {
        public int MinimumNumToScore = 1;
        public Team BePartOfTeam1;
        public Team BePartOfTeam2;
        public Team BePartOfTeam3;
        public List<Team> Team1sInTheZone;
        public List<Team> Team2sInTheZone;
        public List<Team> Team3sInTheZone;      
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<ScoreComponent>();
        }

        // Update is called once per frame
        void Update()
        {

        }   
        
        public bool CanScore()
        {
            if (Team1sInTheZone == null&& Team2sInTheZone == null&& Team3sInTheZone == null) return false;
            if (Team1sInTheZone.Count < MinimumNumToScore ||Team2sInTheZone.Count < MinimumNumToScore || Team3sInTheZone.Count < MinimumNumToScore || Team3sInTheZone.Count< MinimumNumToScore) return false;
            if (((Team1sInTheZone.Count <MinimumNumToScore && Team1sInTheZone.Count <MinimumNumToScore) || (Team2sInTheZone.Count <MinimumNumToScore) && (Team3sInTheZone.Count <MinimumNumToScore) || (Team1sInTheZone.Count <MinimumNumToScore) && (Team3sInTheZone.Count <MinimumNumToScore)||(Team1sInTheZone.Count <MinimumNumToScore)&&(Team2sInTheZone.Count <MinimumNumToScore)&&(Team3sInTheZone.Count <MinimumNumToScore))) return false;
          //if ((Team1sInTheZone.Count >= MinimumNumToScore && Team2sInTheZone.Count >= MinimumNumToScore) || (Team1sInTheZone.Count >= MinimumNumToScore) && (Team3sInTheZone.Count >= MinimumNumToScore) || (Team2sInTheZone.Count >= MinimumNumToScore) && (Team3sInTheZone.Count >= MinimumNumToScore) || (Team1sInTheZone.Count >= MinimumNumToScore && Team2sInTheZone.Count >= MinimumNumToScore && Team3sInTheZone.Count > MinimumNumToScore)) return false;
            else return true;
        }
        public void AddTeamMembers(ScoreComponent Member1,ScoreComponent Member2,ScoreComponent Member3)
        {
            if(!Team1sInTheZone.Any(team1=>team1.ID.Equals(Member1.TeamID=1)))
            {
                var team1 = new Team(Member1.TeamID=1, new List<ScoreComponent> { Member1 },Member1.Score);
                //var team2 = new Team2(Member.TeamID, new List<ScoreComponent> { Member }, Member.Score);
                //var team3 = new Team3(Member.TeamID, new List<ScoreComponent> { Member }, Member.Score);
                Team1sInTheZone.Add(team1);               
            }
            if(!Team2sInTheZone.Any(team2 => team2.ID.Equals(Member2.TeamID=2)))
            {
                var team2 = new Team(Member2.TeamID, new List<ScoreComponent> { Member2 }, Member2.Score);
                Team2sInTheZone.Add(team2);
            }
            if(!Team3sInTheZone.Any(team3 => team3.ID.Equals(Member3.TeamID=3)))
            {
                var team3 = new Team(Member3.TeamID, new List<ScoreComponent> { Member3 }, Member3.Score);
                Team3sInTheZone.Add(team3);
            }

            else
            {
                var team1 = Team1sInTheZone.FirstOrDefault(team1 => team1.ID.Equals(Member1.TeamID));
                //var team2 = Team1sInTheZone.FirstOrDefault(team2 => team2.ID.Equals(Member.TeamID));
                //var team3 = Team1sInTheZone.FirstOrDefault(team3 => team3.ID.Equals(Member.TeamID));
                if (team1.Members.Contains(Member1)) return;
                team1.Members.Add(Member1);
                var team2 = Team2sInTheZone.FirstOrDefault(team2 => team2.ID.Equals(Member2.TeamID));
                if (team2.Members.Contains(Member2)) return;
                team2.Members.Add(Member2);
                var team3 = Team3sInTheZone.FirstOrDefault(team3 => team3.ID.Equals(Member3.TeamID));
                if (team3.Members.Contains(Member2)) return;
                team3.Members.Add(Member2);
            }
        }
        public void RemoveMembers(ScoreComponent Member1,ScoreComponent Member2,ScoreComponent Member3)
        {
                     
            var team1 = Team1sInTheZone.Find(team => team.ID == Member1.TeamID);
            if (team1 == null) return;
            if(team1.Members.Contains(Member1))
            {
                team1.Members.Remove(Member1);
            }
            var Team1Empty = Team1sInTheZone.Find(team1 => team1.Members.Count == 0);
            if(Team1Empty==null)
            {
                Team1sInTheZone.Remove(Team1Empty);
            }
            var team2 = Team2sInTheZone.Find(team2 => team2.ID == Member2.TeamID);
            if (team2 == null) return;
            if (team2.Members.Contains(Member2))
            {
                team2.Members.Remove(Member2);
            }
            var Team2Empty = Team2sInTheZone.Find(team2 => team2.Members.Count == 0);
            if (Team2Empty == null)
            {
                Team2sInTheZone.Remove(Team2Empty);
            }
            var team3 = Team3sInTheZone.Find(team3 => team3.ID == Member3.TeamID);
            if (team3 == null) return;
            if (team3.Members.Contains(Member3))
            {
                team3.Members.Remove(Member3);
            }
            var Team3Empty = Team3sInTheZone.Find(team3 => team3.Members.Count == 0);
            if (Team3Empty == null)
            {
                Team3sInTheZone.Remove(Team3Empty);
            }
        }
    }
}
        

