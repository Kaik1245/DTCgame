using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : GunType
{
    private Camera GameCamera;
    public Laser LaserScript;
    private GameObject LaserBeam;
    RaycastHit2D Laser2d;
    public float Distance;
    public float TimeAllowedToShoot;
    float ActualTimeShooting;
    bool IsReloading = false;
    public float CoolDownTime;
    public float TimeIncrement;
    public float TimeStillBeforeShooting;
    public float ActualTimeStillBeforeShooting;
    
    // Start is called before the first frame update
    void Start()
    {
        GameCamera = FindObjectOfType<Camera>();
        LaserBeam = Instantiate(LaserScript).gameObject;
        LaserBeam.SetActive(false);
        IsReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            ActualTimeStillBeforeShooting += TimeIncrement * Time.deltaTime;
        }
        else
        {
            ActualTimeStillBeforeShooting = 0;
            LaserBeam.SetActive(false);
        }
        if(ActualTimeStillBeforeShooting >= TimeStillBeforeShooting)
        {
            if(!IsReloading)
            {
                if(ActualTimeShooting < TimeAllowedToShoot)
                {
                    Laser2d = Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad)), Distance);

                    if(Laser2d.collider != null)
                    {
                        float dx = Laser2d.point.x - transform.position.x;
                        float dy = Laser2d.point.y - transform.position.y;
                        float DistanceCalc = Mathf.Sqrt(Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2));

                        // Divive by sixteen because that is the amount of pixels the texture has
                        LaserBeam.transform.localScale = new Vector2(DistanceCalc / 16, 0.4f / 16);
                    }
                    else{
                        LaserBeam.transform.localScale = new Vector2(Distance / 16, 0.4f / 16);
                    }
                    LaserBeam.SetActive(true);
                    LaserBeam.transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);

                    ActualTimeShooting += TimeIncrement * Time.deltaTime;
                }
                else
                {
                    StartCoroutine(LaserWeaponCoolDown());
                }
            }
            else{
                LaserBeam.SetActive(false);
            }
        }

        transform.rotation = Quaternion.Euler(0, 0, RotateTowardsPosition(transform.position, GameCamera.ScreenToWorldPoint(Input.mousePosition), 0));
        
        LaserBeam.transform.position = transform.position;
    }
    IEnumerator LaserWeaponCoolDown()
    {
        IsReloading = true;
        yield return new WaitForSeconds(CoolDownTime);
        ResetGun();
    }
    public void ResetGun()
    {
        IsReloading = false;
        ActualTimeShooting = 0;
    }
}
