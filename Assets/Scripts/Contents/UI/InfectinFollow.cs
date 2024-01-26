using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectinFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;

    [SerializeField] private Vector3 offset;

    Vector3 targetPos;

    private void Update()
    {
        targetPos = new Vector3(target.transform.position.x + offset.x, target.transform.position.y + offset.y, target.transform.position.z + offset.z);
        transform.position = targetPos;
    }
}
