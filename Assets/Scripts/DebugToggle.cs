using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugToggle : MonoBehaviour
{
    [SerializeField] MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleRendering()
    {
        mesh.enabled = !mesh.enabled;
    }
}
