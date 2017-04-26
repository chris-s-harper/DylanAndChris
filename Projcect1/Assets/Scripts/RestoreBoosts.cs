using UnityEngine;
using System.Collections;

public class RestoreBoosts : MonoBehaviour
{
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
}
