using UnityEngine;
using System.Collections;

public class RestoreBoosts : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TankMovement player = other.GetComponent<TankMovement>();
            player.ResetAvailableBoosts();
            Destroy(gameObject);
        }
    }
}
