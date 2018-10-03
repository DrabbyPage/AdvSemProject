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
        //rotationAngle = transform.eulerAngles.z;
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
            Destroy(gameObject);
        }

        // particle effect

        // destroy the object

    }

    void MoveBullet()
    {
        /*
        Debug.Log(rotationAngle);
        float newX, newY;
        newX = gameObject.transform.position.x + (bulletSpeed * Mathf.Cos(rotationAngle));
        newY = gameObject.transform.position.y + (bulletSpeed * Mathf.Sin(rotationAngle));

        // set the rotation to the rotation angle
        transform.eulerAngles = new Vector3(0.0f, 0.0f, rotationAngle);

        // move in the direction
        transform.position = new Vector2(newX, newY);
        */

        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed);
    }

    public void SetAngle(float newDegree)
    {
        rotationAngle = newDegree;
        transform.eulerAngles = new Vector3(0.0f, 0.0f, newDegree);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
