using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunkontrol : MonoBehaviour
{
    [SerializeField] float fireRate;
    [SerializeField] bool semiAuto;
    float fireRateTimer;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletsPerShot;
    aimyonetim aim;

    [SerializeField] AudioClip gunShot;
    AudioSource AudioSource;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<aimyonetim>();
        fireRateTimer = fireRate;
    }

    
    void Update()
    {
        if (ShouldFire()) Fire();

    }
    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
       // if (ammo.currentAmmo == 0) return false;
       // if (actions.currentState == actions.Reload) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }
    void Fire()
    {
        fireRateTimer = 0;
        barrelPos.LookAt(aim.aimPos);
        AudioSource.PlayOneShot(gunShot);

        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);

            bullet bulletScript = currentBullet.GetComponent<bullet>();
           // bulletScript.weapon = this;

            //bulletScript.dir = barrelPos.transform.forward;

            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
