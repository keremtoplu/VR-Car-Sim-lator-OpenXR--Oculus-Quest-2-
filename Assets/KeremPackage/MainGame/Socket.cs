using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Socket : MonoBehaviour
{
    private bool isActive = false;

    public bool IsActive => isActive;

	private XRSocketInteractor xRSocket;

	private void Start()
	{
		xRSocket = GetComponent<XRSocketInteractor>();
	}

	public void GoToDesiredPosition(Vector3 desiredPosition)
	{
		xRSocket.attachTransform.localPosition = desiredPosition;
		isActive = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		var casette = other.GetComponent<XRGrabInteractable>();
		if(casette)
		{
			Debug.Log("isactive");
			isActive = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		var casette = other.GetComponent<XRGrabInteractable>();
		if (casette)
		{
			isActive = false;
			casette.GetComponent<AudioSource>().Stop();
			Debug.Log("isactive false");
		}
	}
}
