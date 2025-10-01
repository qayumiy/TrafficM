using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;



public class CharacterNavigationController : MonoBehaviour
{
    public Vector3 destination;

    Vector3 lastPosition;
    public bool reachedDestination;
    public float stopDistance = 1;
    public float rotationSpeed;
    public float minSpeed, maxSpeed;
    public float movementSpeed;
    Vector3 velocity;

    Animator myAnim;

    [Header("Sensors")]
    public float sensorLength;
    public Vector3 frontSensorpos = new Vector3(0.2f, 0.2f, -1f);
    public float frontSensorAngle = 30f;

    public bool forceStop;

    AudioSource aSource;

    public UnityEvent callEnd;

    int loadedNumber;




    private void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();

        movementSpeed = Random.Range(minSpeed, maxSpeed);

        aSource = this.gameObject.GetComponent<AudioSource>();


        forceStop = false;
    }
    private void Update()
    {
       // Debug.Log(loadedNumber);

        if (forceStop==false)
        {
            if (transform.position != destination)
            {
                Vector3 destinationDirection = destination - transform.position;
                destinationDirection.y = 0;

                float destinationDistance = destinationDirection.magnitude;

                if (destinationDistance >= stopDistance)
                {
                    reachedDestination = false;
                    Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                }
                else
                {
                    reachedDestination = true;
                }

                velocity = (transform.position - lastPosition) / Time.deltaTime;
                velocity.y = 0;
                var velocityMagnitude = velocity.magnitude;
                velocity = velocity.normalized;
                var fwdDotProduct = Vector3.Dot(transform.forward, velocity);
                var rightDotProduct = Vector3.Dot(transform.right, velocity);
            }


        }

        Sensors();


    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log(other.gameObject.name.ToString());

        if (other.gameObject.CompareTag("Vehicle"))
        {
            Killaguy();

        }
    }

    public void Killaguy() {
       
        loadedNumber = PlayerPrefs.GetInt("death");

        aSource.PlayOneShot(aSource.clip);

        myAnim.SetBool("killed", true);

        loadedNumber++;

        PlayerPrefs.SetInt("death", loadedNumber);


               
    }

    private void OnCollisionEnter(Collision collision)
    {

     //   Debug.Log(collision.gameObject.name.ToString());

        if (collision.gameObject.CompareTag("Vehicle")) {

            myAnim.SetBool("killed", true);


        }
    }
   

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }



    private void Sensors()
    {

        RaycastHit hit;

        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * frontSensorpos.x;
        sensorStartPos += transform.up * frontSensorpos.y;


        //slowdown before head on collision
        if (Physics.Raycast(sensorStartPos, -transform.forward, out hit, sensorLength) ||
            Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength) ||
            Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))

        {

          //  Debug.Log(hit.collider.gameObject.name);

           // Debug.DrawLine(sensorStartPos, hit.point, Color.blue);


            if (hit.collider.CompareTag("npc"))
            {

                //Debug.Log (hit.collider.gameObject.name);
              // Debug.DrawLine(sensorStartPos, hit.point, Color.yellow);
              //  reachedDestination = true;

            }


            if (hit.collider.CompareTag("Vehicle"))
            {
              //  Debug.Log(hit.collider.gameObject.name);
               Debug.DrawLine(sensorStartPos, hit.point, Color.red);
                forceStop = true;
                reachedDestination = true;
                	//Debug.Log ("vehicleObs");
            }

           


        }
        else
        {
            //	Debug.Log ("noVeh");
            forceStop = false;

            reachedDestination = false;

        }


    }












}