using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform defaultTransform;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Camera playerHead;

	public void ResetPosition()
	{
        var rotationAngleY = defaultTransform.rotation.eulerAngles.y - playerHead.transform.rotation.eulerAngles.y;
        player.transform.Rotate(0f, -rotationAngleY, 0);

        var distanceDiff = defaultTransform.position - playerHead.transform.position;
        player.transform.position += distanceDiff;

	}
	private void Update()
	{
        ResetPosition();
	}
}
