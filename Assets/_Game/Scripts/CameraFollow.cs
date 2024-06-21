using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform tf;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    private void LateUpdate()
    {
        tf.position = target.transform.position + offset;
    }
}
