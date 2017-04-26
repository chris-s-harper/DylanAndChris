using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class RestoreBoosts : MonoBehaviour
{
    private AudioSource pickupSound;
    void Start()
    {
        pickupSound = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 5, 0,Space.World);
        gameObject.transform.Translate(0, 0.1f, 0, Space.World);
        gameObject.transform.Translate(0, -0.1f, 0, Space.World);
    }
    // Update is called once per frame
    void Update ()
    {
	
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

    void OnDisable()
    {
        pickupSound.Play();
    }
}
