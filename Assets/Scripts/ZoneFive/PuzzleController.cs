using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PuzzleController : MonoBehaviour
{
    public bool completed;
    [SerializeField] GameObject puzzle;
    CompletionController progress;
    [SerializeField] ZoneFiveController points;
    [SerializeField] Material correctMaterial;
    [SerializeField] Material incorrectMaterial;
    public bool placementMode = false;
    private bool puzzleMode = false;
    private bool puzzleCheck = false;
    private float scaleAmount;
    private float placementDistance = 1.0f;
    private GameObject placedPuzzle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(points.puzzleSolving && !puzzleMode)
        {
            placementMode = true;
        }
        if(Input.touchCount>0 && placementMode)
        {
            Touch currTouch = Input.GetTouch(0);

            switch(currTouch.phase)
            {
                case (TouchPhase.Began):
                    scaleAmount = 0.8f;
                    break;

                case (TouchPhase.Moved):
                    scaleAmount *= 1.01f;
                    break;

                case (TouchPhase.Stationary):
                    scaleAmount *= 1.01f;
                    break;

                case (TouchPhase.Ended):
                    placedPuzzle.transform.localScale = new Vector3(scaleAmount,0.00625f * scaleAmount,scaleAmount);
                    progress = placedPuzzle.GetComponent<CompletionController>();
                    placementMode = false;
                    puzzleMode = true;
                    break;
            }
        }
        if(puzzleMode)
        {
            if(Input.touchCount>0)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    var rigid = hit.collider.GetComponent<Rigidbody>();
                    if(rigid != null)
                    {
                        if (!puzzleCheck)
                        {
                            puzzleCheck = true;
                            checkPuzzle(rigid);
                            puzzleCheck = false;
                        }
                    }
                }
            }
        }
    }

    private void checkPuzzle(Rigidbody rigid)
    {
        if (rigid.GetComponent<MeshRenderer>().sharedMaterial == incorrectMaterial)
        {
            if (progress != null && progress.getCurrentCheck() != null)
            {
                if (progress.getCurrentCheck().GetComponent<Rigidbody>() == rigid)
                {
                    rigid.GetComponent<MeshRenderer>().material = correctMaterial;
                    progress.currentcheck += 1;
                    if(progress.currentcheck == 5)
                    {
                        completed = true;
                        points.puzzleSolved = true;
                    }
                }
                else
                {
                    progress.resetProgress();
                }
            }
        }
    }

    public void PuzzlePlace(Transform cameraPos)
    {
        if (placementMode)
        {
            Vector3 placementPos = new Vector3(cameraPos.position.x + (placementDistance * cameraPos.forward.x), cameraPos.position.y + (placementDistance * cameraPos.forward.y), cameraPos.position.z + (placementDistance * cameraPos.forward.z));
            GameObject UnanchoredObj = Instantiate(puzzle, placementPos, Quaternion.Euler(90, 0, 0));
            AnchorBehaviour myAnchor = UnanchoredObj.AddComponent<AnchorBehaviour>();
            myAnchor.ConfigureAnchor("Anchor", placementPos, Quaternion.Euler(90, 0, 0));
            placedPuzzle = UnanchoredObj;
        }
    }
}
