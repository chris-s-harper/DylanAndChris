using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.Rigidbody))]
public class TankMovement : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    private string triggers;
    [SerializeField]
    private string leftStick;
    [SerializeField]
    private string driftButton;
    [SerializeField]
    private string boostButton;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float boostSpeed;
    [SerializeField]
    private int maxBoosts;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float driftTurnSpeed;
    [SerializeField]
    private float maxSpeedInMPH;
    [SerializeField]
    private AudioSource engineAudio;
    [SerializeField]
    private AudioSource boostAudio;
    [SerializeField]
    private ParticleSystem dustParticles;
    [SerializeField]
    private Slider boostSlider;
    #endregion

    private float triggerInput;
    private float leftStickInput;
    private bool isAccelerating;
    private int availableBoosts;
    private Vector3 movementVector;
    private Rigidbody myRigidBody;
    private WaitForSeconds particleDelay = new WaitForSeconds(1);

    #region Constants
    private const float yConstant = 0;
    private const float xConstant = 0;
    private const float zConstant = 0;
    #endregion

    // Use this for initialization
    void Start()
    {
        //gets the rigidbody component, which is used for acceleration and turning
        ResetAvailableBoosts();
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        Accelerate();
        CarAudio();
    }

    private void Accelerate()
    {
        /*gets a value from the controller tiggers, then multiplies said value by negative one
        (this is because the left trigger returns a positive value, and the right trigger returns a negative value)
        */
        triggerInput = (Input.GetAxis(triggers) * -1);
        movementVector = new Vector3(xConstant, yConstant, triggerInput);

        //converts to MPH
        const float milesPerHourConst = 2.23694f;
        float speedInMPH = myRigidBody.velocity.magnitude * milesPerHourConst;

        //TODO: Check speed for Reversing if needs revision

        //if statement caps speed
        if (speedInMPH < maxSpeedInMPH)
        {
            //movement speed is actual "speed" of zamboni
            myRigidBody.AddRelativeForce(movementVector * movementSpeed);
            //debug statement to check if it's going faster than max speed
            //Debug.Log("Speed is at " + (myRigidBody.velocity.magnitude * milesPerHourConst).ToString());
        }

        Boost();

        UpdateIsAccelerating();

        //Debug.Log(triggerInput.ToString());
    }

    private void UpdateIsAccelerating()
    {
        if ((Input.GetAxis(triggers) > 0.1) || (Input.GetAxis(triggers) < -0.1))
        {
            isAccelerating = true;
        }
        else
        {
            isAccelerating = false;
        }
    }

    private void Turn()
    {
        if (isAccelerating)
        {
            if (Input.GetButton(driftButton))
            {
                leftStickInput = (Input.GetAxis(leftStick) * driftTurnSpeed);
                Debug.Log("I'm drifting!");
            }
            else
            {
                leftStickInput = (Input.GetAxis(leftStick) * turnSpeed);
            }
            
            gameObject.transform.Rotate(xConstant, leftStickInput, zConstant);
        }
    }

    private void Boost()
    {
        if (availableBoosts > 0 && Input.GetButtonDown(boostButton))
        {
            boostAudio.Play();
            myRigidBody.AddRelativeForce(movementVector * boostSpeed);
            StartCoroutine(expandParticleSize());
            availableBoosts--;
            UpdateBoostSlider();
            Debug.Log("I have boosted! " + availableBoosts.ToString() + " left");
        }
    }

    private IEnumerator expandParticleSize()
    {
        dustParticles.startSize = dustParticles.startSize * 2;
        yield return particleDelay;
        dustParticles.startSize = dustParticles.startSize / 2;
    }

    private void CarAudio()
    {
        if (isAccelerating && engineAudio.isPlaying == false)
        {
            engineAudio.Play();
        }
        if (!isAccelerating)
        {
            engineAudio.Stop();
        }
    }

    private void UpdateBoostSlider()
    {
        boostSlider.value = availableBoosts;
    }

    public void ResetAvailableBoosts()
    {
        Debug.Log("Boosts Reset");
        availableBoosts = maxBoosts;
        UpdateBoostSlider();
    }
}
