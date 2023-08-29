using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GunType : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //this returns the direction the player would be shot towards
    public Vector2 Recoil(float RecoilAmount, Vector2 DesiredPosition, Vector2 OriginalPosition, float OffsetAngle)
    {
        float dx = DesiredPosition.x - OriginalPosition.x;
        float dy = DesiredPosition.y - OriginalPosition.y;
        //Make it + 90
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg + OffsetAngle;
        Vector2 RecoilDirection = new Vector2(Mathf.Sin(-angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)) * RecoilAmount;
        return RecoilDirection;
    }
    public void ShootBullet(BulletType bullet, Vector2 OriginPosition, float Rotation)
    {
        Instantiate(bullet, OriginPosition, Quaternion.Euler(0, 0, Rotation));
    }
    public float RotateTowardsPosition(Vector2 OriginalPosition, Vector2 DesiredPosition, float AngleOffset)
    {
        float dx = DesiredPosition.x - OriginalPosition.x;
        float dy = DesiredPosition.y - OriginalPosition.y;
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg + AngleOffset;
        return angle;
    }
}
