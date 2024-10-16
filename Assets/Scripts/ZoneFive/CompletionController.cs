using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionController : MonoBehaviour
{
    public bool part1;
    public bool part2;
    public bool part3;
    public bool part4;
    public bool part5;
    [SerializeField] public GameObject check1;
    [SerializeField] public GameObject check2;
    [SerializeField] public GameObject check3;
    [SerializeField] public GameObject check4;
    [SerializeField] public GameObject check5;
    // Start is called before the first frame update
    void Start()
    {
        part1 = false; part2 = false; part3 = false; part4 = false; part5 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetProgress()
    {
        part1 = false; part2 = false; part3 = false; part4 = false; part5 = false;
    }
}
