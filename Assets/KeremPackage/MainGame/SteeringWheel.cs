using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SteeringWheel : XRBaseInteractable
{

    public UnityEvent<float> OnWheelRotated;

    private float currentAngle = 0.0f;
    private float totalAngleDifference = 0f;
    private Quaternion wheelDefaultRotation;


    private void Start()
	{
        wheelDefaultRotation = transform.localRotation;
	}
	protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        currentAngle = FindWheelAngle();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
		//currentAngle = FindWheelAngle();
        if(interactorsSelecting.Count==0)
		{

            transform.localRotation = wheelDefaultRotation;
            totalAngleDifference = 0f;
        }
	}

	public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
                RotateWheel();
        }
    }

    private void RotateWheel()
    {
        float totalAngle = FindWheelAngle();

        float angleDifference = Mathf.DeltaAngle(totalAngle, currentAngle);
        float maxAngleDifference = 180f * Time.deltaTime;
        angleDifference = Mathf.Clamp(angleDifference, -maxAngleDifference, maxAngleDifference);
        totalAngleDifference += angleDifference;
        if(totalAngleDifference>-160f && totalAngleDifference<160f)
		{
            transform.Rotate(transform.forward, -angleDifference, Space.World);
		}
        currentAngle = totalAngle;
        OnWheelRotated?.Invoke(angleDifference);

    }

    private float FindWheelAngle()
    {
        float totalAngle = 0;

        // Combine directions of current interactors
        foreach (IXRSelectInteractor interactor in interactorsSelecting)
        {
            Vector2 direction = FindLocalPoint(interactor.transform.position);
            totalAngle += ConvertToAngle(direction) * FindRotationSensitivity();
        }

        return totalAngle;
    }

    private Vector2 FindLocalPoint(Vector3 position)
    {
        // Convert the hand positions to local, so we can find the angle easier
        return transform.InverseTransformPoint(position).normalized;
    }

    private float ConvertToAngle(Vector2 direction)
    {
        // Use a consistent up direction to find the angle
        return Vector2.SignedAngle(Vector2.up, direction);
    }

    private float FindRotationSensitivity()
    {
        // Use a smaller rotation sensitivity with two hands
        return 1.0f / interactorsSelecting.Count;
    }

    

}