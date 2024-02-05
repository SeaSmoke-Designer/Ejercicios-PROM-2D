using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;

    SpriteRenderer targetSr;

    [Range(1, 10), SerializeField]
    float smoothFactor;

    [SerializeField]
    float horizontalOffset;

    int horizontalOffsetDirection;

    [SerializeField]
    float verticalOffset;

    [SerializeField]
    Vector2 minValues, maxValues;

    Transform camTransform;
    bool cameraIsMoving = false;

    void FindPlayer()
    {
        camTransform = Camera.main.transform;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players != null && players.Length > 0)
        {
            target = players[0].transform;
            targetSr = players[0].GetComponent<SpriteRenderer>();
        }
    }

    void FixedUpdate()
    {
        if (target == null) FindPlayer();
        Follow();
    }

    void Follow()
    {
        horizontalOffsetDirection = targetSr.flipX ? -1 : 1;
        Vector3 followPosition = new Vector3(target.position.x + horizontalOffset * horizontalOffsetDirection, target.position.y + verticalOffset, camTransform.position.z);
        Vector3 boundPosition = new Vector3(
            Math.Clamp(followPosition.x, minValues.x, maxValues.x),
            Math.Clamp(followPosition.y, minValues.y, maxValues.y),
            camTransform.position.z);
        Vector3 smoothPosition = Vector3.Lerp(camTransform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        camTransform.position = smoothPosition;
        if (Math.Abs(camTransform.position.x - boundPosition.x) < .5f) 
            cameraIsMoving = false;
        else 
            cameraIsMoving = true;
    }

    public bool CameraIsMoving()
    {
        return cameraIsMoving;
    }
}
