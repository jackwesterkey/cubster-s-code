using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recoil : MonoBehaviour
{
    public Vector3 upRecoil;
    Vector3 orignalRotation;

    void Start()
    {
        orignalRotation = transform.localEulerAngles;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AddRecoil();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopRecoil();
        }
    }

    private void AddRecoil()
    {
        transform.localEulerAngles += upRecoil;
    }

    private void StopRecoil()
    {
        transform.localEulerAngles = orignalRotation;
    }

}