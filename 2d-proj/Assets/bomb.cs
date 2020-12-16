using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public GameObject exp;
    public float expForce, radius;
    public LayerMask layerToHit;

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject _exp = Instantiate(exp, transform.position, transform.rotation);
        knockBack();
        Destroy(_exp, 2);
    }

    void knockBack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerToHit);
        foreach (Collider2D nearby in colliders)
        {
            Rigidbody2D rig = nearby.GetComponent<Rigidbody2D>();
            Vector2 direction = nearby.transform.position - transform.position;
            if (rig != null)
            {
                rig.AddForce(expForce * direction, ForceMode2D.Impulse);
            }
        }
    }
}
