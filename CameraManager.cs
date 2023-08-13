using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform _self;
    
    [SerializeField] private Transform _target;

    private void Update()
    {
        _self.LookAt(_target);
    }
}
