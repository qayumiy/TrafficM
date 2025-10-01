using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class Crane : MonoBehaviour {

	public Transform Piston1A;
	public Transform Piston2A;
	public Transform Piston1B;
	public Transform Piston2B;

	public Transform rack_FL;
	public Transform rack_FR;
	public Transform rack_BL;
	public Transform rack_BR;

    public Transform Arrow1;
	public Transform Arrow2;
	public Transform Arrow3;
	public Transform Arrow4;
	public Transform Arrow5;
	public Transform Arrow6;
	public Transform Arrow7;

	public float arrowRot1Speed;
	public float arronRotSpeed;
	public float arronSpeed;
	public float rackSpeed;
	public Vector3 forward_rack_FL;
	public Vector3 back_rack_FL;
	public Vector3 forward_rack_FR;
	public Vector3 back_rack_FR;
	public Vector3 forward_rack_BL;
	public Vector3 back_rack_BL;
	public Vector3 forward_rack_BR;
	public Vector3 back_rack_BR;
	public Vector3 Arrow4for;
	public Vector3 Arrow4back;
	public Vector3 Arrow5for;
	public Vector3 Arrow5back;
	public Vector3 Arrow6for;
	public Vector3 Arrow6back;
	public Vector3 Arrow7for;
	public Vector3 Arrow7back;


	public KeyCode Key_for;
	public KeyCode Key_Back;
	public KeyCode rack_AB_FL;
	public KeyCode rack_AB_FR;
	public KeyCode rack_AB_BL;
	public KeyCode rack_AB_BR;
	public KeyCode KeyAB_Arrow4_5_6_7;
	public KeyCode KeyAB_Arrow1;
	public KeyCode KeyAB_Arrow2;
	public KeyCode KeyAB_Arrow3;


	private Vector3 myRotation_Arrow2;
	private Vector3 myRotation_Arrow3;

	public float minValue_Arrow2;
	public float maxValue_Arrow2;
	public enum RotAxis_Arrow2  {
		XAxis,
		YAxis,
		ZAxis
	}
	public RotAxis_Arrow2 myRotAxis_Arrow2;

	public float minValue_Arrow3;
	public float maxValue_Arrow3;
	public enum RotAxis_Arrow3  {
		XAxis,
		YAxis,
		ZAxis
	}
	public RotAxis_Arrow3 myRotAxis_Arrow3;

	void Start(){

		myRotation_Arrow2 = Arrow2.localEulerAngles;
		myRotation_Arrow3 = Arrow3.localEulerAngles;

	}
	void Update(){

		if (Input.GetKey (Key_for) && Input.GetKey (rack_AB_FL)) {
			RackFL_up ();
		} else if (Input.GetKey (Key_Back) && Input.GetKey (rack_AB_FL)) {
			RackFL_down ();
		}
		if (Input.GetKey (Key_for) && Input.GetKey (rack_AB_FR)) {
			RackFR_up ();
		} else if (Input.GetKey (Key_Back) && Input.GetKey (rack_AB_FR)) {
			RackFR_down ();
		}
		if (Input.GetKey (Key_for) && Input.GetKey (rack_AB_BL)) {
			RackBL_up ();
		} else if (Input.GetKey (Key_Back) && Input.GetKey (rack_AB_BL)) {
			RackBL_down ();
		}
		if (Input.GetKey (Key_for) && Input.GetKey (rack_AB_BR)) {
			RackBR_up ();
		} else if (Input.GetKey (Key_Back) && Input.GetKey (rack_AB_BR)) {
			RackBR_down ();
		}
		if (Input.GetKey (Key_for) && Input.GetKey (KeyAB_Arrow4_5_6_7)) {
			ArrowFor_Up();
		} else if (Input.GetKey (Key_Back) && Input.GetKey (KeyAB_Arrow4_5_6_7)) {
			ArrowFor_Down();
		}
		if (Input.GetKey (Key_for) && Input.GetKey (KeyAB_Arrow1)) {
			ArrowRot_A_Up();
		} else if (Input.GetKey (Key_Back) && Input.GetKey (KeyAB_Arrow1)) {
			ArrowRot_A_Down();
		}
		if (Input.GetKey (Key_for) && Input.GetKey (KeyAB_Arrow2)) {
			ArrowRot_B_Up();
		} else if (Input.GetKey (Key_Back) && Input.GetKey (KeyAB_Arrow2)) {
			ArrowRot_B_Down();
		}
		if (Input.GetKey (Key_for) && Input.GetKey (KeyAB_Arrow3)) {
			ArrowRot_C_Up();
		} else if (Input.GetKey (Key_Back) && Input.GetKey (KeyAB_Arrow3)) {
			ArrowRot_C_Down();
		}

	}
		void LateUpdate(){

		if (Piston1A != null && Piston2A != null) {
			Piston1A.LookAt (Piston2A.position, Piston1A.up);
			Piston2A.LookAt (Piston1A.position, Piston2A.up);
		}
		if (Piston1B != null && Piston2B != null) {
			Piston1B.LookAt (Piston2B.position, Piston1B.up);
			Piston2B.LookAt (Piston1B.position, Piston2B.up);
		}
	}
	public void RackFL_up()
	{
		rack_FL.transform.localPosition = Vector3.MoveTowards (rack_FL.transform.localPosition,forward_rack_FL,rackSpeed * Time.deltaTime);
	}
	public void RackFL_down()
	{
		rack_FL.transform.localPosition = Vector3.MoveTowards (rack_FL.transform.localPosition,back_rack_FL,rackSpeed * Time.deltaTime);
	}
	public void RackFR_up()
	{
		rack_FR.transform.localPosition = Vector3.MoveTowards (rack_FR.transform.localPosition,forward_rack_FR,rackSpeed * Time.deltaTime);
	}
	public void RackFR_down()
	{
		rack_FR.transform.localPosition = Vector3.MoveTowards (rack_FR.transform.localPosition,back_rack_FR,rackSpeed * Time.deltaTime);
	}
	public void RackBL_up()
	{
		rack_BL.transform.localPosition = Vector3.MoveTowards (rack_BL.transform.localPosition,forward_rack_BL,rackSpeed * Time.deltaTime);
	}
	public void RackBL_down()
	{
		rack_BL.transform.localPosition = Vector3.MoveTowards (rack_BL.transform.localPosition,back_rack_BL,rackSpeed * Time.deltaTime);
	}
	public void RackBR_up()
	{
		rack_BR.transform.localPosition = Vector3.MoveTowards (rack_BR.transform.localPosition,forward_rack_BR,rackSpeed * Time.deltaTime);
	}
	public void RackBR_down()
	{
		rack_BR.transform.localPosition = Vector3.MoveTowards (rack_BR.transform.localPosition,back_rack_BR,rackSpeed * Time.deltaTime);
	}
	public void ArrowFor_Up()
	{
		Arrow4.transform.localPosition = Vector3.MoveTowards (Arrow4.transform.localPosition,Arrow4for,arronSpeed * Time.deltaTime);
		Arrow5.transform.localPosition = Vector3.MoveTowards (Arrow5.transform.localPosition,Arrow5for,arronSpeed * Time.deltaTime);
		Arrow6.transform.localPosition = Vector3.MoveTowards (Arrow6.transform.localPosition,Arrow6for,arronSpeed * Time.deltaTime);
		Arrow7.transform.localPosition = Vector3.MoveTowards (Arrow7.transform.localPosition,Arrow7for,arronSpeed * Time.deltaTime);
	}
	public void ArrowFor_Down()
	{
		Arrow4.transform.localPosition = Vector3.MoveTowards (Arrow4.transform.localPosition,Arrow4back,arronSpeed * Time.deltaTime);
		Arrow5.transform.localPosition = Vector3.MoveTowards (Arrow5.transform.localPosition,Arrow5back,arronSpeed * Time.deltaTime);
		Arrow6.transform.localPosition = Vector3.MoveTowards (Arrow6.transform.localPosition,Arrow6back,arronSpeed * Time.deltaTime);
		Arrow7.transform.localPosition = Vector3.MoveTowards (Arrow7.transform.localPosition,Arrow7back,arronSpeed * Time.deltaTime);
	}
	public void ArrowRot_A_Up()
	{
		Arrow1.transform.Rotate (Vector3.up, arrowRot1Speed * Time.deltaTime);
	}
	public void ArrowRot_A_Down()
	{
		Arrow1.transform.Rotate (Vector3.down, arrowRot1Speed * Time.deltaTime);
	}
	public void ArrowRot_B_Up()
	{
		switch (myRotAxis_Arrow2) {
		case RotAxis_Arrow2.XAxis:
			myRotation_Arrow2.x = Mathf.Clamp(myRotation_Arrow2.x + arronRotSpeed * Time.deltaTime, minValue_Arrow2, maxValue_Arrow2);
			break;
		case RotAxis_Arrow2.YAxis:
			myRotation_Arrow2.y = Mathf.Clamp(myRotation_Arrow2.y + arronRotSpeed * Time.deltaTime, minValue_Arrow2, maxValue_Arrow2);
			break;
		case RotAxis_Arrow2.ZAxis:
			myRotation_Arrow2.z = Mathf.Clamp(myRotation_Arrow2.z + arronRotSpeed * Time.deltaTime, minValue_Arrow2, maxValue_Arrow2);
			break;
		}
		Arrow2.transform.localRotation = Quaternion.Euler(myRotation_Arrow2);
	}
	public void ArrowRot_B_Down()
	{
		switch (myRotAxis_Arrow2) {
		case RotAxis_Arrow2.XAxis:
			myRotation_Arrow2.x = Mathf.Clamp(myRotation_Arrow2.x - arronRotSpeed * Time.deltaTime, minValue_Arrow2, maxValue_Arrow2);
			break;
		case RotAxis_Arrow2.YAxis:
			myRotation_Arrow2.y = Mathf.Clamp(myRotation_Arrow2.y - arronRotSpeed * Time.deltaTime, minValue_Arrow2, maxValue_Arrow2);
			break;
		case RotAxis_Arrow2.ZAxis:
			myRotation_Arrow2.z = Mathf.Clamp(myRotation_Arrow2.z - arronRotSpeed * Time.deltaTime, minValue_Arrow2, maxValue_Arrow2);
			break;
		}
		Arrow2.transform.localRotation = Quaternion.Euler(myRotation_Arrow2);
	}
	public void ArrowRot_C_Up()
	{
		switch (myRotAxis_Arrow3) {
		case RotAxis_Arrow3.XAxis:
			myRotation_Arrow3.x = Mathf.Clamp(myRotation_Arrow3.x + arronRotSpeed * Time.deltaTime, minValue_Arrow3, maxValue_Arrow3);
			break;
		case RotAxis_Arrow3.YAxis:
			myRotation_Arrow3.y = Mathf.Clamp(myRotation_Arrow3.y + arronRotSpeed * Time.deltaTime, minValue_Arrow3, maxValue_Arrow3);
			break;
		case RotAxis_Arrow3.ZAxis:
			myRotation_Arrow3.z = Mathf.Clamp(myRotation_Arrow3.z + arronRotSpeed * Time.deltaTime, minValue_Arrow3, maxValue_Arrow3);
			break;
		}
		Arrow3.transform.localRotation = Quaternion.Euler(myRotation_Arrow3);
	}
	public void ArrowRot_C_Down()
	{
		switch (myRotAxis_Arrow3) {
		case RotAxis_Arrow3.XAxis:
			myRotation_Arrow3.x = Mathf.Clamp(myRotation_Arrow3.x - arronRotSpeed * Time.deltaTime, minValue_Arrow3, maxValue_Arrow3);
			break;
		case RotAxis_Arrow3.YAxis:
			myRotation_Arrow3.y = Mathf.Clamp(myRotation_Arrow3.y - arronRotSpeed * Time.deltaTime, minValue_Arrow3, maxValue_Arrow3);
			break;
		case RotAxis_Arrow3.ZAxis:
			myRotation_Arrow3.z = Mathf.Clamp(myRotation_Arrow3.z - arronRotSpeed * Time.deltaTime, minValue_Arrow3, maxValue_Arrow3);
			break;
		}
		Arrow3.transform.localRotation = Quaternion.Euler(myRotation_Arrow3);
	}
}
