using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolderScript : MonoBehaviour
{
    public Transform cameraPosition;


    private void Update()
    {
         transform.position = cameraPosition.position;
    }
}
