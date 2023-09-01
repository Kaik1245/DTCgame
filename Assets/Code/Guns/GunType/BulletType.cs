using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletType : MonoBehaviour
{
    public bool ExplodesInCollision;

    public void Explode(float ExplosionRadius, float DamageAmount, float BlastAmount)
    {
        var Colliders = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
        foreach (var HitCollider in Colliders)
        {
            if (HitCollider.tag != "Player")
            {
                Vector2 ClosestHitPosition = HitCollider.ClosestPoint(transform.position);
                float distance = Vector3.Distance(ClosestHitPosition, transform.position);
                float DamagePercent = Mathf.InverseLerp(ExplosionRadius, 0, distance);

                // Add explosionForce
                if (BlastAmount != 0)
                {
                    Vector2 DistanceVector = (Vector2)HitCollider.transform.position - (Vector2)transform.position;
                    float ExplosionForce = BlastAmount / DistanceVector.magnitude;
                    if (HitCollider.GetComponent<EnemyTypeA>() != null)
                    {
                        HitCollider.GetComponent<EnemyTypeA>().rb.AddForce(DistanceVector.normalized * ExplosionForce);
                        HitCollider.GetComponent<EnemyTypeA>().health -= DamagePercent * DamageAmount;
                    }
                    else if (HitCollider.GetComponent<EnemyTypeB>() != null)
                    {
                        HitCollider.GetComponent<EnemyTypeB>().rb.AddForce(DistanceVector.normalized * ExplosionForce);
                        HitCollider.GetComponent<EnemyTypeB>().health -= DamagePercent * DamageAmount;
                    }
                    else if (HitCollider.GetComponent<EnemyTypeC>() != null)
                    {
                        HitCollider.GetComponent<EnemyTypeC>().rb.AddForce(DistanceVector.normalized * ExplosionForce);
                        HitCollider.GetComponent<EnemyTypeC>().health -= DamagePercent * DamageAmount;
                    }
                    else if (HitCollider.GetComponent<EnemyTypeD>() != null)
                    {
                        HitCollider.GetComponent<EnemyTypeD>().rb.AddForce(DistanceVector.normalized * ExplosionForce);
                        HitCollider.GetComponent<EnemyTypeD>().health -= DamagePercent * DamageAmount;
                    }
                }
            }
        }
    }
}
