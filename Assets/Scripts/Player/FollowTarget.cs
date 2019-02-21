using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Transform playerTransform;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
    }
}
