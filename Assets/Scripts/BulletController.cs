using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public float damage = 20;
    public float criticalStrikeChance = 0.2f;
    public float criticalStrikeMultiplier = 2;
    public float screenSize = 5;
    public float verticalSpeed = 10;
    public float testCode;
    private Vector3 creatorPosition;

    void Start () {
        creatorPosition = GameObject.Find("Cannon").transform.position;

        transform.position = new Vector3(creatorPosition.x, creatorPosition.y, creatorPosition.z);
    }
	
    void Update () {
        transform.Translate(Vector2.up * verticalSpeed * Time.deltaTime);

        if (transform.position.y > screenSize) {
            Destroy(gameObject);
        }
    }

    public float calculateDamage () {
        float totalDamage = damage;

        if (Random.value < (1f - criticalStrikeChance)) {
            totalDamage = damage * criticalStrikeMultiplier;
        }

        return totalDamage;
    }
}
