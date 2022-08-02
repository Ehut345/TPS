using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Things")]
    [SerializeField] public Camera cam;
    [SerializeField] public float giveDamageOf = 10f;
    [SerializeField] public float shootingRange = 100f;
    [SerializeField] public float fireCharge = 15f;
    [SerializeField] public Animator anim;
    [SerializeField] public Player player;


    [Header("Rifle Ammunition and Shooting")]
    private int maximumAmmunition = 20;
    private int mag = 15;
    private int presentAmmunition;
    private float reloadingTime = 1.3f;
    private bool setReloading = false;
    private float nextTimeToShoot = 0f;


    [Header("Rifle Effects")]
    [SerializeField] public ParticleSystem muzzleSpark;
    [SerializeField] public GameObject impactEffect;
    [SerializeField] public GameObject impactEffectForAll;
    [SerializeField] public GameObject goreEffect;
    [SerializeField] public GameObject droneEffect;

    [Header("Sounds and UI")]
    [SerializeField] private GameObject ammoOutUI;
    [SerializeField] private int timeToShowUI = 1;
    [SerializeField] private AudioClip shootingSound;
    [SerializeField] private AudioClip reloadingSound;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        presentAmmunition = maximumAmmunition;
    }
    void Update()
    {
        if (setReloading)
            return;
        if (presentAmmunition <= 0)
        {
            StartCoroutine(Relaod());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            anim.SetBool("Fire", true);
            anim.SetBool("Idel", false);
            nextTimeToShoot = Time.time + 1 / fireCharge;
            Shoot();
        }
        else if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("Idel", false);
            anim.SetBool("IdelAim", true);
            anim.SetBool("Walk", true);
            anim.SetBool("FireWalk", true);
            anim.SetBool("Reloading", false);
        }
        else if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            anim.SetBool("Idel", false);
            anim.SetBool("IdelAim", true);
            anim.SetBool("Walk", true);
            anim.SetBool("FireWalk", true);
            anim.SetBool("Reloading", false);
        }
        else
        {
            anim.SetBool("Fire", false);
            anim.SetBool("Idel", true);
            anim.SetBool("FireWalk", false);
            anim.SetBool("Reloading", false);
        }
    }

    void Shoot()
    {
        if (mag == 0)
        {
            StartCoroutine(ShowAmmoOut());
            return;
        }
        presentAmmunition--;
        if (presentAmmunition == 0)
        {
            mag--;
        }

        AmmoCount.occurrence.UpdateAmmoText(presentAmmunition);
        AmmoCount.occurrence.UpdateMagText(mag);

        audioSource.PlayOneShot(shootingSound);
        //muzzleSpark.Play();
        RaycastHit hitInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            Objects objects = hitInfo.transform.GetComponent<Objects>();

            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            EnemyDrone drone = hitInfo.transform.GetComponent<EnemyDrone>();

            if (objects != null)
            {
                muzzleSpark.Play();

                objects.ObjectHitDamage(giveDamageOf);
                GameObject impactGo = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo, 0.5f);
            }
            else if (enemy != null)
            {
                muzzleSpark.Play();

                enemy.EnemyHitDamage(giveDamageOf);
                GameObject impactGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo, 2.0f);
            }
            else if (drone != null)
            {
                muzzleSpark.Play();

                drone.EnemyDroneHitDamage(giveDamageOf);
                GameObject impactGo = Instantiate(droneEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo, 2.0f);
            }
            else if (hitInfo.transform.tag != "Player")
            {
                GameObject impactGo1 = Instantiate(impactEffectForAll, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo1, 0.5f);
            }
        }
    }
    IEnumerator Relaod()
    {
        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        setReloading = true;
        anim.SetBool("Reloading", true);
        audioSource.PlayOneShot(reloadingSound);
        yield return new WaitForSeconds(reloadingTime);
        anim.SetBool("Reloading", false);
        presentAmmunition = maximumAmmunition;
        player.playerSpeed = 1.9f;
        player.playerSprint = 3.0f;
        setReloading = false;
    }

    IEnumerator ShowAmmoOut()
    {
        ammoOutUI.SetActive(true);
        yield return new WaitForSeconds(timeToShowUI);
        ammoOutUI.SetActive(false);
    }
}
