using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
namespace Class3
{
    public class WeaponLogics : MonoBehaviour
    {
        public Transform BulletSpawnPos;
        [SerializeField] GameObject ImpactPos;
        [SerializeField] GameObject BulletPrefab;
        LineRenderer Linerenderer;
        [SerializeField] Rigidbody BulletRigid;
        public float LineRendererLength;
        MeshRenderer ImpactMeshRenderer;
        public int BulletNum;
        public int MaxBullet;
        int RemainingBullets;
        public const float MaxCoolDown = 0.1f;
        public float ShotSpeed;
        public float ShotCoolDown = MaxCoolDown;
        AudioSource Audio;
        [SerializeField] AudioClip ShotSound;
        [SerializeField] AudioClip Reload;
        [SerializeField] AudioClip EmptyGun;
        public Text BulletNumText;
        public Text MaxBulletText;
        public bool HasPickedUpAmmo = false;       
        public float TimerForStoppingInfiniteBullets;
        RaycastHit Hit;
        [SerializeField] float RayDistance;
        public bool HasSurrendered = false;
        public Animator CivilianAnim;
        // Start is called before the first frame update

        void Awake()
        {
            Linerenderer = GetComponent<LineRenderer>();
            //RemainingBullets = MaxBullet - BulletNum;
            if (ImpactPos)
            {
                ImpactMeshRenderer = ImpactPos.GetComponent<MeshRenderer>();
                ImpactMeshRenderer.enabled = false;
            }
            BulletNum = 30;
            MaxBullet = 150;
            BulletNumText.text = BulletNum.ToString();
            MaxBulletText.text = MaxBullet.ToString();
            Audio = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateLineRenderer();
            DetectTargetToAim();
            UpdateAmmoAmount();
            if (ShotCoolDown > 0)
            {
                ShotCoolDown -= Time.deltaTime;
                RemainingBullets = 30 - BulletNum;
            }
            if (Input.GetButton("Fire1") && ShotCoolDown < 0)
            {
                if (BulletNum > 0)
                {
                    Shoot();
                }
                else
                {
                    //    if (MaxBullet <= 0||BulletNum<=0)
                    //    {
                    //        MaxBullet = 0;
                    //        MaxBulletText.text = MaxBullet.ToString();
                    PlaySound(EmptyGun);
                    //}               
                }
                ShotCoolDown = MaxCoolDown;
            }
            if (Input.GetKeyDown(KeyCode.R) && MaxBullet > 0 && BulletNum < 30)
            {
                ReloadGun();
            }

        }
        void DetectTargetToAim()
        {
            Debug.DrawRay(transform.position, transform.right, Color.black);
            if (Physics.Raycast(transform.position, transform.right, out Hit, RayDistance))
            {
                if (Hit.transform.gameObject.GetComponent<Civilian>() != null)
                {
                    HasSurrendered = true;
                    if (HasSurrendered == true)
                    {
                        CivilianAnim.SetTrigger("Surrender");
                        Debug.Log("Civilian surrenders!");
                    }
                    //play the surrendering animation.
                }
                else
                {
                    HasSurrendered = false;

                }
            }

        }
        void UpdateLineRenderer()
        {
            if (Linerenderer)
            {
                Linerenderer.SetPosition(0, BulletSpawnPos.position);
                Ray ray = new Ray(BulletSpawnPos.position, BulletSpawnPos.transform.forward * LineRendererLength);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit, LineRendererLength))
                {
                    Linerenderer.SetPosition(1, rayHit.point);
                    ImpactPos.transform.position = rayHit.point;
                    ImpactMeshRenderer.enabled = true;
                }
                else
                {
                    Linerenderer.SetPosition(1, BulletSpawnPos.position + BulletSpawnPos.transform.forward * LineRendererLength);
                    ImpactMeshRenderer.enabled = true;
                }
                //Linerenderer.SetPosition(1, BulletSpawnPos.position + BulletSpawnPos.transform.forward);
            }
        }
        void Shoot()
        {
            --BulletNum;
            GameObject Bullet = Instantiate(BulletPrefab, BulletSpawnPos.position, BulletSpawnPos.rotation);
            //BulletRigid.velocity = transform.forward * ShotSpeed;
            PlaySound(ShotSound);
            BulletNumText.text = BulletNum.ToString();
        }
        void ReloadGun()
        {
            PlaySound(Reload);
            BulletNum += RemainingBullets;
            MaxBullet -= RemainingBullets;
            if (MaxBullet <= 0 || BulletNum <= 0)
            {
                MaxBullet = 0;
                MaxBulletText.text = MaxBullet.ToString();
                PlaySound(EmptyGun);
            }
            BulletNumText.text = BulletNum.ToString();
            MaxBulletText.text = MaxBullet.ToString();
        }
        void PlaySound(AudioClip Sound)
        {
            if (Audio && Sound)
            {
                Audio.PlayOneShot(Sound);
            }
        }
        void UpdateAmmoAmount()
        {
            if (HasPickedUpAmmo == true)
            {
                TimerForStoppingInfiniteBullets -= Time.deltaTime;
                MaxBullet += 30;
                MaxBulletText.text = MaxBullet.ToString();              
                if(TimerForStoppingInfiniteBullets<=0)
                {
                    MaxBullet += 0;
                    MaxBulletText.text = MaxBullet.ToString();
                    HasPickedUpAmmo = false;
                }
            }
        }

    }
}
