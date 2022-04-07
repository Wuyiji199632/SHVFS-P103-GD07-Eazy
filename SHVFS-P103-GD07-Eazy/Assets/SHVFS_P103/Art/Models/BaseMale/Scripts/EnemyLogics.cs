using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
namespace Class3
{
    enum EnemyStates
    {
        Idle, Patrol, Chase, Attack
    }

    public class EnemyLogics : MonoBehaviour
    {
        [SerializeField]
        EnemyStates Enemystate = EnemyStates.Idle;
        NavMeshAgent navmeshAgent;
        [SerializeField] Transform Destination;
        [SerializeField] Animator EnemyAnim;
        [SerializeField] Transform PatrolStartPos;
        [SerializeField] Transform PatrolEndPos;
        Vector3 CurrentPatrolDestination;
        [SerializeField] GameObject Player;
        float RadiusForSearch = 30.0f;
        float AttackRadius = 25.0f;
        const float MaxAttackCoolDown = 0.5f;
        float AttackCoolDownDefaulted = MaxAttackCoolDown;
        [SerializeField] GameObject EnemyBulletPrefab;
        [SerializeField] Transform BulletSpawnPos;
        [SerializeField] AudioClip ShotSound;
        AudioSource Audio;



        // Start is called before the first frame update
        void Awake()
        {
            navmeshAgent = GetComponent<NavMeshAgent>();
            CurrentPatrolDestination = PatrolStartPos.position;
            Player = GameObject.FindGameObjectWithTag("Player");
            Audio = GetComponent<AudioSource>();

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.25f);
            Gizmos.DrawSphere(transform.position, RadiusForSearch);
            Gizmos.DrawSphere(transform.position, AttackRadius);
        }

        // Update is called once per frame
        void Update()
        {
            if (PatrolEndPos && PatrolStartPos)
            {
                Patrol();
                navmeshAgent.isStopped = false;
            }
            switch (Enemystate)
            {
                case (EnemyStates.Idle):
                    SearchForPlayer();
                    break;
                case (EnemyStates.Patrol):
                    SearchForPlayer();
                    if (PatrolEndPos && PatrolStartPos)
                    {
                        Patrol();
                        navmeshAgent.isStopped = false;
                    }
                    break;
                case (EnemyStates.Chase):
                    ChasePlayer();
                    Attack();
                    break;
                case (EnemyStates.Attack):
                    Attack();
                    //SearchForPlayer();
                    break;
            }


            //if (navmeshAgent && Destination)
            //{
            //    navmeshAgent.SetDestination(Destination.position);
            //    float distance = Vector3.Distance(CurrentPatrolDestination, transform.position);
            //    if(distance<1.0f)
            //    {
            //        if(CurrentPatrolDestination==PatrolStartPos.position)
            //        {
            //            CurrentPatrolDestination = PatrolEndPos.position;
            //        }
            //    }
            //    else
            //    {
            //        CurrentPatrolDestination = PatrolStartPos.position;

            //    }
            //}
        }
        void Patrol()
        {
            if (navmeshAgent && CurrentPatrolDestination != Vector3.zero)
            {
                navmeshAgent.SetDestination(CurrentPatrolDestination);
            }

            float distance = Vector3.Distance(CurrentPatrolDestination, transform.position);
            if (distance < 1.5f)
            {
                if (CurrentPatrolDestination == PatrolStartPos.position)
                {
                    CurrentPatrolDestination = PatrolEndPos.position;

                }
                else
                {
                    CurrentPatrolDestination = PatrolStartPos.position;

                }
            }

        }
        void SearchForPlayer()
        {
            float distance = Vector3.Distance(transform.position, Player.transform.position);
            if (distance < RadiusForSearch)
            {
                Enemystate = EnemyStates.Chase;
                Attack();
            }
           
            
        }
        void ChasePlayer()
        {
            if (navmeshAgent && Destination)
            {
                navmeshAgent.SetDestination(Destination.position);
               // Enemystate = EnemyStates.Attack;
            }
            
                //navmeshAgent.isStopped = true;
                //navmeshAgent.velocity = Vector3.zero;
                                        
                navmeshAgent.isStopped = false;

        }
    

        void Attack()
        {           
            float distance = Vector3.Distance(transform.position, Player.transform.position);                      
            if (distance < AttackRadius||GetComponent<TakeDamage>().BeHit==true)
            {
                AttackCoolDownDefaulted -= Time.deltaTime;
                if (AttackCoolDownDefaulted < 0)
                {
                    //Attack the player;
                    
                            GameObject EnemyBullet = Instantiate(EnemyBulletPrefab, BulletSpawnPos.transform.position, BulletSpawnPos.transform.rotation);
                            //PlayerInputComponent playerInputComponent = Player.GetComponent<PlayerInputComponent>();
                            //if (playerInputComponent)
                            //{
                            //    playerInputComponent.TakeDamage(10);
                            //}
                            PlaySound(ShotSound);
                            AttackCoolDownDefaulted = MaxAttackCoolDown;
                            Debug.Log("Shoot!");
                                                         
                }
            }
            
                Enemystate = EnemyStates.Chase;
            
        }
        void PlaySound(AudioClip Sound)
        {
            if (Audio && Sound)
            {
                Audio.PlayOneShot(Sound);
            }
        }
    }
}

