using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CarController : MonoBehaviour
{
	[SerializeField]
	private WheelCollider frontDriverW, frontPassengerW;
	[SerializeField]
	private WheelCollider rearDriverW, rearPassangerW;
	[SerializeField]
	private Transform frontDriverT, frontPassengerT;
	[SerializeField]
	private Transform rearDriverT, rearPassengerT;

	[SerializeField]
	private float breakingForce = 3000f;
	[SerializeField]
	private float maxSteerAngle = 30;
	[SerializeField]
	private float motorforce = 50;

	[SerializeField]
	private SteeringWheel wheel;

	[SerializeField]
	private InputActionReference trigger;

	[SerializeField]
	private InputActionReference carBreak;

	[SerializeField]
	private InputActionReference gear;

	[SerializeField]
	private InputActionReference returnDefaultRotation;


	private Quaternion defaultRotation;


	private float axis = 1f;
	private float mSteeringAngle;
	private void Awake()
	{
		gear.action.started += OnGearChanged;
		returnDefaultRotation.action.started += OnReturnDefaultRotation;
		defaultRotation = transform.localRotation;
	}
	private void FixedUpdate()
	{
		Steer();
		Accalerate();
		UpdateWheelPoses();
	}


	private void Steer()
	{
		mSteeringAngle = maxSteerAngle * -wheel.transform.localRotation.z;
		frontDriverW.steerAngle = mSteeringAngle;
		frontPassengerW.steerAngle = mSteeringAngle;
	}

	private void Accalerate()
	{
		float value = trigger.action.ReadValue<float>() * axis;
		frontDriverW.motorTorque = value * motorforce;
		frontPassengerW.motorTorque = value * motorforce;
		float breakValue = carBreak.action.ReadValue<float>() * breakingForce;
		if(carBreak.action.ReadValue<float>()<0.005f)
		{
			breakingForce = 0f;
		}
		else
		{
			breakingForce = 3000f;
		}
		frontDriverW.brakeTorque = breakValue;
		frontPassengerW.brakeTorque = breakValue;
		rearDriverW.brakeTorque = breakValue;
		rearPassangerW.brakeTorque = breakValue;


	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontDriverW, frontDriverT);
		UpdateWheelPose(frontPassengerW, frontPassengerT);
		UpdateWheelPose(rearDriverW, rearDriverT);
		UpdateWheelPose(rearPassangerW, rearPassengerT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform transform)
	{
		Vector3 pos = transform.position;
		Quaternion rot = transform.rotation;

		_collider.GetWorldPose(out pos, out rot);


		transform.position = pos;
		transform.rotation = rot;

	}

	private void OnGearChanged(InputAction.CallbackContext context)
	{
		var value = gear.action.ReadValue<float>();
		if (value < 0)
		{
			axis = -1;
		}
		else if (value > 0)
		{
			axis = 1;
		}
	}

	private void OnReturnDefaultRotation(InputAction.CallbackContext context)
	{
		transform.localRotation = defaultRotation;
	}

	private void OnDestroy()
	{
		gear.action.started -= OnGearChanged;
		returnDefaultRotation.action.started -= OnReturnDefaultRotation;
	}


}
