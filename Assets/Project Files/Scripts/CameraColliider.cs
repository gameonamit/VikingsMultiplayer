using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColliider : MonoBehaviour
{
    [SerializeField] private Transform referenceTransform;
    [SerializeField] private float collisionOffset = 0.3f;
    [SerializeField] private float cameraSpeed = 15f;

    Vector3 defaultPos, directionNormalized;
    Transform parentTransform;
    float defaultDistance;

    private void Start()
    {
        defaultPos = transform.localPosition;
        directionNormalized = defaultPos.normalized;
        parentTransform = transform.parent;
        defaultDistance = Vector3.Distance(defaultPos, Vector3.zero);
    }
    private void LateUpdate()
    {
        RaycastHit hit;
        Vector3 dirTmp = parentTransform.TransformPoint(defaultPos) - referenceTransform.position;
        if (Physics.SphereCast(referenceTransform.position, collisionOffset, dirTmp, out hit, defaultDistance))
        {
            transform.localPosition = (directionNormalized * (hit.distance - collisionOffset));
        }
        else transform.localPosition = Vector3.Lerp(transform.localPosition, defaultPos, Time.deltaTime * cameraSpeed);
    }
}
