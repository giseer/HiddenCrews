using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrientationer : MonoBehaviour
{
    private enum OrientationMode
    {
        CameraForward,
        ToTarget,
        ToMovement,
    }
    
    [SerializeField] private OrientationMode orientationMode = OrientationMode.CameraForward;
    [SerializeField] private float angularSpeed = 360f;
    [SerializeField] private Camera orientationCamera;
    [SerializeField] private Transform target;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    
    public void UpdateOrientation(Vector3 currentVelocity)
    {
        
        Vector3 desiredDirection = Vector3.forward;
        switch(orientationMode)
        {
            case OrientationMode.CameraForward:
                desiredDirection =
                    _mainCamera.transform.forward;
                break;
            case OrientationMode.ToTarget:
                desiredDirection =
                    target.position - transform.position;
                break;
            case OrientationMode.ToMovement:
                desiredDirection = currentVelocity;
                break;
        }
        desiredDirection.y = 0f;
    
        float angularDistance = Vector3.SignedAngle(
            transform.forward, desiredDirection, Vector3.up);
    
        float AngleBeacauseSpeed = angularSpeed * Time.deltaTime;
        float remainingAngle =
            Mathf.Abs(angularDistance);
        float angleToApply =
            Mathf.Sign(angularDistance) *
            Mathf.Min(AngleBeacauseSpeed, remainingAngle);
    
        Quaternion rotationToApply = Quaternion.AngleAxis(
            angleToApply,
            Vector3.up
        );
        transform.rotation = rotationToApply * transform.rotation;
    }
}
