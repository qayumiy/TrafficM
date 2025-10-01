using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CarEngineNew : MonoBehaviour {

    public Transform path;

	public int brakeNode;
   
	//public GameObject dustParticles;
	//public GameObject exhParticles;

	[Header("WheelCols")]

	public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    
	[Header("EngineVar")]
	public float maxSteerAngle = 45f;
	public float maxMotorTorque = 80f;
    public float maxBrakeTorque = 150f;
    public float currentSpeed;
    public float maxSpeed = 10f;
    public Vector3 centerOfMass;
	//public AudioSource m_AudioSourceA;
	//public AudioClip honk;
    
	public AudioSource m_AudioSourceEngine;
	public AudioClip engineAudio;

	[Header("Braking")]
	public bool forceS = false;
	public bool isBraking = false;
	//public Texture2D textureNormal;
 //   public Texture2D textureBraking;
 //   public Renderer carRenderer;

	[Header("Turning")]
	private bool turning;
	public float closeEnough=5f;
    private List<Transform> nodes;
    private int currentNode = 0;
    
	[Header("Sensors")]
	public float sensorLength;
	public Vector3 frontSensorpos= new Vector3(0.2f,0.2f,-1f);
	public float frontSensorAngle = 30f;


	//[Header("headlights")]
	//public GameObject leftLight;
	//public Renderer headlightleft;
	//public Renderer headlightright;
	//public Material headlightsOn;
	//public Material headlightsOff;

	[Header("AudioMgmt")]
	public List<int> rpms;
	public int topRpm;

    private void Start () {
		forceS = false;
		GetComponent<Rigidbody>().centerOfMass = centerOfMass;
		m_AudioSourceEngine = this.gameObject.GetComponent<AudioSource> ();

        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++) {
            if (pathTransforms[i] != path.transform) {
                nodes.Add(pathTransforms[i]);
            }
        }
    }


	private void FixedUpdate () {
        ApplySteer();

		if (forceS != true)
		{
			Drive();
		}
		else {
			maxMotorTorque = 0;
		}
       
		CheckWaypointDistance();
        Braking();
		Sensors ();
		//Headlights ();

		//Debug.Log (this.gameObject.name + forceS.ToString());

		if (currentSpeed <= 5) {
		//	dustParticles.SetActive (false);
		} else {
			//dustParticles.SetActive (true);

		}
	}

    private void ApplySteer() {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive() {
		
		currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;


		//TopRpmforSound
		int rpmtmp = Mathf.RoundToInt (wheelFL.rpm);
		if(!rpms.Contains(rpmtmp)){
			rpms.Add (rpmtmp);
			rpms = rpms.OrderBy(w=>w).ToList();
			if (currentSpeed > maxSpeed && currentSpeed <= (maxSpeed+0.2)) {
				topRpm = rpms.Last ();
		//	Debug.Log (topRpm);
			}

            //m_audiosourceengine.pitch = wheelfl.rpm / toprpm;
            //if (m_audiosourceengine.pitch >= 1)
            //{
            //    m_audiosourceengine.pitch = 1;
            //}

        }


        if (currentSpeed < maxSpeed && !isBraking) {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;

			if (maxMotorTorque <= 50) {
			//	exhParticles.SetActive (true);
			} else {
				//exhParticles.SetActive (false);

			}

        } else {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
		

        }
    }

    private void CheckWaypointDistance() {
		Vector3 offset = nodes [currentNode].position - transform.position;
		float sqrlen = offset.sqrMagnitude;

		if (sqrlen < closeEnough * closeEnough) {
			turning = true;

			if (currentNode == nodes.Count - 1) {

				currentNode = 0;

			} else {
				currentNode++;
			//	Debug.Log (currentNode);
			//	m_AudioSourceA.PlayOneShot (honk, 0.5f);

				if (currentNode == 13 && this.gameObject.activeInHierarchy) {
					Destroy (this.gameObject);
				}
			}

		}

    }

    private void Braking() {
        if (isBraking) {
          //  carRenderer.material.mainTexture = textureBraking;
            wheelRL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque;
        } else {
           // carRenderer.material.mainTexture = textureNormal;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }


		if ((currentNode == brakeNode && currentNode>=1)||forceS) {
		  isBraking = true;
		}
    }

	private void Sensors(){

		RaycastHit hit;

		Vector3 sensorStartPos = transform.position;
		sensorStartPos += transform.forward * frontSensorpos.x;
		sensorStartPos += transform.up * frontSensorpos.y;


		//slowdown before head on collision
		if (Physics.Raycast (sensorStartPos, transform.forward, out hit, sensorLength)||
			Physics.Raycast (sensorStartPos, Quaternion.AngleAxis(frontSensorAngle,transform.up)*transform.forward, out hit, sensorLength)||
			Physics.Raycast (sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle,transform.up)*transform.forward, out hit, sensorLength)) 
		
		{

			//Debug.Log(hit.collider.gameObject.name);

			Debug.DrawLine (sensorStartPos, hit.point, Color.blue);


			if (hit.collider.CompareTag ("npc"))
			{
			
				//Debug.Log (hit.collider.gameObject.name);
				Debug.DrawLine (sensorStartPos, hit.point, Color.yellow);
				//forceStop = true;
				isBraking = true;

				wheelRL.brakeTorque = maxBrakeTorque;
				wheelRR.brakeTorque = maxBrakeTorque;

				wheelFL.motorTorque = 0;
				wheelFR.motorTorque = 0;
				//Debug.Log ("onZeb");
			}
			else
			{
				//	Debug.Log ("noVeh");
				//forceStop = false;
				isBraking = false;
				wheelFL.motorTorque = maxMotorTorque;
				wheelFR.motorTorque = maxMotorTorque;

			}


			if (hit.collider.CompareTag ("Vehicle"))
				{
				//Debug.Log (hit.collider.gameObject.name);
				Debug.DrawLine (sensorStartPos, hit.point, Color.red);
				//forceStop = true;

				isBraking = true;
				//forceStop = true;


				wheelRL.brakeTorque = maxBrakeTorque;
				wheelRR.brakeTorque = maxBrakeTorque;

			//	Debug.Log ("vehicleObs");
				}
			else
			{
				//	Debug.Log ("noVeh");
				//forceStop = false;

				isBraking = false;
				wheelFL.motorTorque = maxMotorTorque;
				wheelFR.motorTorque = maxMotorTorque;

			}


			if (hit.collider.CompareTag("goTag"))
			{
				Debug.DrawLine(sensorStartPos, hit.point, Color.green);

				isBraking = false;
				wheelFL.motorTorque = maxMotorTorque;
				wheelFR.motorTorque = maxMotorTorque;
			}
						


		} 

	
	}

	//private void Headlights(){

	////	if (nightController.nighting) {
	//		leftLight.SetActive (true);
	//		headlightleft.material = headlightsOn;
	//		headlightright.material = headlightsOn;

	//	//} else {
	//		leftLight.SetActive (false);
	//		headlightleft.material = headlightsOff;
	//		headlightright.material = headlightsOff;
	////	}
	//}

}

