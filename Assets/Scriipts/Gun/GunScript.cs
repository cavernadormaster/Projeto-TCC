
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float Damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public CameraShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GunShoot();
        }
    }

    void GunShoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }

        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGO, 2f);
    }
}
