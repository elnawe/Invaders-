using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public float meteoriteSpawnTimeSeconds = 1;
    public float meteoriteBigSpawnChance = 0.5f;
    public GameObject meteoriteSmallGameObject;
    public GameObject meteoriteBigGameObject;

    private float spawnTimeReady;

    void Start () {
        spawnTimeReady = Time.time + meteoriteSpawnTimeSeconds;
    }

    void Update () {
        if (Time.time >= spawnTimeReady) {
            this.SpawnMeteorite();
            spawnTimeReady = Time.time + meteoriteSpawnTimeSeconds;
        }
    }

    void SpawnMeteorite () {
        // TODO: Add big meteorites
        GameObject.Instantiate(meteoriteSmallGameObject);
    }
}
