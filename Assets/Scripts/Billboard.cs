using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //GameObject refObj = new GameObject();
        //refObj.transform.SetPositionAndRotation(mainCam.transform.position * -1f, Quaternion.Inverse(mainCam.transform.rotation));
        //refObj.transform.localScale = mainCam.transform.localScale * -1f;
        //transform.LookAt(mainCam.transform);
        //transform.LookAt(mainCam.transform.position, Vector3.up);
        //transform.forward = mainCam.transform.forward;
    }
}
