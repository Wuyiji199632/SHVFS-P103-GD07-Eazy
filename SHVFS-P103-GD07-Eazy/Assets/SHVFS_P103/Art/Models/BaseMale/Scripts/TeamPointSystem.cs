using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
namespace Class3
{
    public class TeamPointSystem : Singleton<TeamPointSystem>
    {
        public List<Team> teams = new List<Team>();
        public List<ScorableZoneComponent> Zones = new List<ScorableZoneComponent>();
        public int MaxTeamScores;
        public int TickPoint;
        public int TickTime;
        public int m_RunTime;
        public int Team1Score;
        public int Team2Score;
        public int Team3Score;
        public ScoreComponent Team1Member;
        public ScoreComponent Team2Member;
        public ScoreComponent Team3Member;
        public bool Team1Scores = false;
        public bool Team2Scores = false;
        public bool Team3Scores = false;

        public override void Awake()
        {
            base.Awake();
            var Members = FindObjectsOfType<ScoreComponent>();
            Zones = FindObjectsOfType<ScorableZoneComponent>().ToList();
            foreach (var Member in Members)
            {
                AddMembers(Team1Member, Team2Member, Team3Member);
                //var matchingTeamIndex = -1;
                //for (var i = 0; i < teams.Count; i++)
                //{
                //    if (teams[i].ID.Equals(scoreComponent.TeamID))
                //    {
                //        matchingTeamIndex = i;
                //    }
                //}
                //if (matchingTeamIndex < 0)

                //{
                //    var team = new Team()
                //    { ID = scoreComponent.TeamID, Members = new List<ScoreComponent> { scoreComponent } };
                //    teams.Add(team);
                //}

                //else
                //{
                //    if (teams[matchingTeamIndex].Members.Contains(scoreComponent)) return;
                //    teams[matchingTeamIndex].Members.Add(scoreComponent);
                //}
            }

        }
        public void Update()
        {
            if (m_RunTime >= TickTime)
            {
                //GiveScores();
                m_RunTime = 0;
            }

        }
        public void AddMembers(ScoreComponent Member1, ScoreComponent Member2, ScoreComponent Member3)
        {
            if (!teams.Any(team => team.ID.Equals(Member1.TeamID)))
            {
                var team1 = new Team(Member1.TeamID = 1, new List<ScoreComponent> { Member1 }, Member1.Score);
                //var team2 = new Team2(Member.TeamID, new List<ScoreComponent> { Member }, Member.Score);
                //var team3 = new Team3(Member.TeamID, new List<ScoreComponent> { Member }, Member.Score);
                teams.Add(team1);
                //team2s.Add(team2);
                //team3s.Add(team3);
            }
            if (!teams.Any(team2 => team2.ID.Equals(Member2.TeamID)))
            {
                var team2 = new Team(Member2.TeamID = 2, new List<ScoreComponent> { Member2 }, Member2.Score);
                teams.Add(team2);
            }
            if (!teams.Any(team3 => team3.ID.Equals(Member3.TeamID)))
            {
                var team3 = new Team(Member3.TeamID = 3, new List<ScoreComponent> { Member3 }, Member3.Score);
                teams.Add(team3);
            }
            else
            {
                var team1 = teams.FirstOrDefault(team => team.ID.Equals(Member1.TeamID));
                //var team2 = team2s.FirstOrDefault(team2 => team2.ID.Equals(Member.TeamID));
                //var team3 = team3s.FirstOrDefault(team3 => team3.ID.Equals(Member.TeamID));
                if (team1.Members.Contains(Member1)) return;
                team1.Members.Add(Member1);
                var team2 = teams.FirstOrDefault(team => team.ID.Equals(Member2.TeamID));
                if (team2.Members.Contains(Member2)) return;
                team2.Members.Add(Member2);
                var team3 = teams.FirstOrDefault(team3 => team3.ID.Equals(Member3.TeamID));
                if (team3.Members.Contains(Member3)) return;
                team3.Members.Add(Member3);
            }

        }
        public void RemoveMembers(ScoreComponent Member1, ScoreComponent Member2, ScoreComponent Member3)
        {
            var team1 = teams.Find(team => team.ID == Member1.TeamID);
            var team2 = teams.Find(team => team.ID == Member2.TeamID);
            var team3 = teams.Find(team => team.ID == Member3.TeamID);
            if (team1 == null && team2 == null && team3 == null) return;
            if (team1.Members.Contains(Member1) && team2.Members.Contains(Member2) && team3.Members.Contains(Member3))
            {
                team1.Members.Remove(Member1);
                team2.Members.Remove(Member2);
                team3.Members.Remove(Member3);
            }
        }
        public void ScorePoints(int TeamNum)
        {
            //var team1 = teams.FirstOrDefault(team => team.ID == 1);
            //var team2 = teams.FirstOrDefault(team => team.ID == 2);
            //var team3 = teams.FirstOrDefault(team => team.ID == 3);
            var scorablezones = FindObjectsOfType<ScorableZoneComponent>();
            var BearsInCompetition = FindObjectsOfType<ScoreComponent>();
            //Team1Score = team1.Score;
            //Team2Score = team2.Score;
            //Team3Score = team3.Score;
            if (Team1Score < MaxTeamScores)
            {
                foreach (var scorablezone in scorablezones)
                {
                    if (scorablezone.GetComponent<ScorableZoneComponent>().CanScore() && teams != null)
                    {
                        foreach (var bear in BearsInCompetition)
                        {
                            if (bear.GetComponent<ScoreComponent>().TeamID == 1 && bear.GetComponent<ScoreComponent>().TeamID == TeamNum)
                            {

                                Team1Score = MaxTeamScores;
                                Team1Scores = true;
                                //team1.Score = MaxTeamScores;
                                

                            }
                        }
                    }
                }
                if (Team2Score < MaxTeamScores)
                {
                    foreach (var scorablezone in scorablezones)
                    {
                        if (scorablezone.GetComponent<ScorableZoneComponent>().CanScore() && teams != null)
                        {
                            foreach (var bear in BearsInCompetition)
                            {
                                if (bear.GetComponent<ScoreComponent>().TeamID == 2 && bear.GetComponent<ScoreComponent>().TeamID == TeamNum)
                                {

                                    Team2Score = MaxTeamScores;
                                    Team2Scores = true;
                                   

                                }

                            }
                        }
                    }

                }
            }
            if (Team3Score < MaxTeamScores)
            {
                foreach (var scorablezone in scorablezones)
                {
                    if (scorablezone.GetComponent<ScorableZoneComponent>().CanScore() && teams != null)
                    {
                        foreach (var bear in BearsInCompetition)
                        {

                            if (bear.GetComponent<ScoreComponent>().TeamID == 3 && bear.GetComponent<ScoreComponent>().TeamID == TeamNum)
                            {

                                Team3Score = MaxTeamScores;
                                Team3Scores = true;
                                //team1.Score = MaxTeamScores;
                                

                            }

                        }
                    }
                }
            }
            //var bears = FindObjectsOfType<ScoreComponent>();
            //var scores =GetComponent<Team>();
            //foreach (var bear in bears)
            //{
            //    if (teamID == bear.TeamID)
            //    {
            //        for (int i = 0; i < 3; i++)
            //        {
            //            teams[i].ID.Equals(teamID);

            //            if (i == 0 && bear.TeamID == 1)
            //            {

            //                scores.Team1Score += bear.TickPoints;


            //                //A bear with the Team ID of 1 can score only when no bears of other is inside the scorable zone.

            //                if (i == 0 && bear.TeamID == 1 && i == 1 && bear.TeamID == 2)
            //                {
            //                    Debug.Log("it works!");

            //                    bear.TickPoints = 0;
            //                }
            //                //Two bears of two different teams can't contribute to successful scoring.
            //                if (i == 0 && bear.TeamID == 1 && i == 2 && bear.TeamID == 3)
            //                {
            //                    Debug.Log("it works!");
            //                    bear.TickPoints = 0;
            //                }
            //            }
            //            //Two bears of two different teams can't contribute to successful scoring.
            //            if (i == 1 && bear.TeamID == 2)
            //            {
            //                scores.Team2Score += bear.TickPoints;

            //            //a bear with the team id of 2 can score only when no bears of other is inside the scorable zone.

            //            if (i == 1 && bear.TeamID == 2 && i == 2 && bear.TeamID == 3)
            //            {
            //                bear.TickPoints = 0;
            //            }
            //        }
            //        //Two bears of two different teams can't contribute to successful scoring.
            //        if (i == 2 && bear.TeamID == 3)
            //                {
            //                scores.Team3Score += bear.TickPoints;
            //                }
            //        //A bear with the Team ID of 2 can score only when no bears of other teams is inside the scorable zone.
            //        if ((i == 0 && bear.TeamID == 1 )|| (i == 1 && bear.TeamID == 2||( i == 2 && bear.TeamID == 3)))
            //        {
            //            scores.Team1Score += 0;
            //            scores.Team2Score += 0;
            //            scores.Team3Score += 0;
            //        }
            //        //three bears of three different teams can't contribute to successful scoring.
            //    }
            //    }
            //}
        }
        public void CheckForScoring()
        {
         if((Team1Scores==true&&Team2Scores==true)||(Team1Scores==true&&Team3Scores==true)||(Team2Scores==true&&Team3Scores==true)||(Team1Scores == true && Team2Scores == true&&Team3Scores==true))
            {
                Team1Score = 0;
                Team2Score = 0;
                Team3Score = 0;
            }
        }
        public void CheckWinsAndLosses()
        {
            
            if(Team1Score==MaxTeamScores)
            {
                SceneManager.LoadScene("WinScene");
            }
            else if(Team2Score==MaxTeamScores||Team3Score==MaxTeamScores)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }



        //public void GiveScores()
        //{
        //    foreach (var zone in Zones)
        //    {
        //        if (zone.BePartOfTeam1 != null)
        //        {
        //            ScorePoints();
        //        }
        //        if (zone.BePartOfTeam2 != null)
        //        {
        //            ScorePoints();
        //        }
        //        if (zone.BePartOfTeam3 != null)
        //        {
        //            ScorePoints();
        //        }
        //    }
        //}
    }
}
















