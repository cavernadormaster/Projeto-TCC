using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunSystem : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletPerTap;
    public bool allowButtonHold;
    public int bulletsLeft, bulletsShot;

    //bools
    public static bool shooting, readyToShoot, reloading;

    //Referance
    public GameObject fpsCam;
    public Transform attackPoint;
    public LayerMask whatIsEnemy;

    // Graphics
    public GameObject BulletHoleGrafic;
    public ParticleSystem mussleFlash;
    public Animator anim;
    public static bool isMoving;
    public AudioSource fireAudio;
    public AudioSource reloadAudio;
    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
        if (isMoving)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    private void MyInput()
    {
        

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading || bulletsLeft == 0)
        {
            Reload();
        }



        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            Debug.Log("Atirando");
            bulletsShot = bulletPerTap;
            shoot();
        }
        else
        {
            anim.SetBool("Shooting", false);
        }
    }

    private void shoot()
    {
        anim.SetBool("Shooting", true);
        fireAudio.Play();
        readyToShoot = false;

        //Spread
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);

        //Calculate Direction with spread

        Transform t = fpsCam.transform;
        t.Rotate(x, y, 0);
        Vector3 direction = t.forward;

        //Raycast
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range))
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.CompareTag("Player"))
            {
                //Here wahere the enemy get damage
                // after "GetComponent" is a example of script and function
                //rayhit.collider.GetComponent<ShootingAi>().TakeDamage(damage);
                Debug.Log("GetIT");
            }
        }

        //Shakecamera

        //Graphics
        mussleFlash.Play();
        GameObject impactGO = Instantiate(BulletHoleGrafic, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGO, 2f);

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        reloadAudio.Play();
        anim.SetBool("Reloading", true);
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        anim.SetBool("Reloading", false);
    }
}