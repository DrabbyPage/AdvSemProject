using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float rotationAngle;
    float bulletSpeed = 40.0f;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(DestroyBullet());
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveBullet();
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Zombie" || col.gameObject.tag == "Player")
        {
            // hurt the obj
            col.gameObject.GetComponent<HealthScript>().HurtCharacter();
        }

        // destroy the object
        Destroy(gameObject);
    }

    void MoveBullet()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed);
    }

    public void SetAngle(float newDegree)
    {
        rotationAngle = newDegree;
        transform.eulerAngles = new Vector3(0.0f, 0.0f, rotationAngle);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
