using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject boostPowerup;
    [SerializeField]
    private float maxZDistance;
    [SerializeField]
    private float maxXDistance;
    [SerializeField]
    private float minZDistance;
    [SerializeField]
    private float minXDistance;
    [SerializeField]
    private float waitTimeInSeconds;

    private const float constYLocation = 0.85f;
    private WaitForSeconds waitTime;
	// Use this for initialization
	void Start ()
    {
        waitTime = new WaitForSeconds(waitTimeInSeconds);
        StartCoroutine(spawnPowerUps());
	}

    private IEnumerator spawnPowerUps()
    {
        while (true)
        {
            yield return waitTime;
            float randomX = Random.Range(minXDistance, maxXDistance);
            float randomZ = Random.Range(minZDistance, maxZDistance);
            Vector3 spawnLocation = new Vector3(randomX,constYLocation,randomZ);
            Instantiate(boostPowerup, spawnLocation, boostPowerup.transform.rotation);
        }
    }
}
