using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCameraScript : MonoBehaviour
{
 public Transform target;
    public float trailDistance = 10f;
    public float heightOffset = 4.0f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (!PlayerMovementScript.Instance.gameStarted)
        {
            Vector3 followPos = target.position - trailDistance * target.forward;
            followPos.y += heightOffset;
            transform.position = Vector3.Lerp(transform.position, followPos, .01f);
            transform.LookAt(target.transform);
        }
        else
        {
            transform.parent = target.transform;
        }

        if (CharacterScript.Instance.onTriggerActive )
        {
            Vector3 triggerPos = target.position - 10 * target.forward + Vector3.up * 8;
            transform.LookAt(target.transform);
            transform.position = Vector3.Lerp(transform.position, triggerPos, .01f);
        }
        else
        {
            Vector3 followPos = target.position - trailDistance * target.forward;
            followPos.y += heightOffset;
            transform.position = Vector3.Lerp(transform.position, followPos, .01f);
            transform.LookAt(target.transform);
        }

    }
}
