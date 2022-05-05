using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    public float startHealth, chanceToSpawnAmmo;
    private float hp;

    public GameObject dieEffect, ammoDrop;
	// Use this for initialization
	void Start () {
        hp = startHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(float damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if(dieEffect != null)
        {
            Instantiate(dieEffect, transform.position, Quaternion.identity);
        }
        float spawnRate = Random.Range(0, 100f);
        if(spawnRate <= chanceToSpawnAmmo)
        {
            Instantiate(ammoDrop, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
