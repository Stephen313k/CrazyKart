using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDie : MonoBehaviour {

    public float dieTime, damage;
    public GameObject dieEffect;

	// Use this for initialization
	void Start () {
        StartCoroutine(Timer());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //collisions hitting enemy car
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collision.gameObject.tag == "Enemy")
        {
            if(collisionGameObject.GetComponent<HealthScript>() != null)
            {
                collisionGameObject.GetComponent<HealthScript>().TakeDamage(damage);
            }
            Die();
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(dieTime);
        Die();
    }

    void Die()
    {
        if(dieEffect != null)
        {
            Instantiate(dieEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
