using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosAtStart : MonoBehaviour
{
    [SerializeField] private Vector3 _pos;

    void Start()
    {
        transform.position = _pos;
    }
}
