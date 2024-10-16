using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private bool completed;
    [SerializeField] CompletionController progress;
    [SerializeField] ZoneFiveController points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puzzle"))
        {
            if(this.gameObject.Equals(progress.check1))
            {
                if(!progress.part1)
                {
                    Debug.Log("Part 1 complete");
                    progress.part1 = true;
                }
            }
            else if(this.gameObject.Equals(progress.check2))
            {
                if(progress.part1)
                {
                    Debug.Log("Part 2 complete");
                    progress.part2 = true;
                }
                else
                {
                    points.resetPuzzlePiece();
                    progress.resetProgress();
                }
            }
            else if(this.gameObject.Equals(progress.check3))
            {
                if(progress.part2)
                {
                    Debug.Log("Part 3 complete");
                    progress.part3 = true;
                }
                else
                {
                    points.resetPuzzlePiece();
                    progress.resetProgress();
                }
            }
            else if(this.gameObject.Equals(progress.check4))
            {
                if(progress.part3)
                {
                    Debug.Log("Part 4 complete");
                    progress.part4 = true;
                }
                else
                {
                    points.resetPuzzlePiece();
                    progress.resetProgress();
                }
            }
            else if(this.gameObject.Equals(progress.check5))
            {
                if(progress.part4)
                {
                    Debug.Log("Part 5 complete");
                    progress.part5 = true;
                    completed = true;
                    points.puzzleSolved = true;
                }
                else
                {
                    points.resetPuzzlePiece();
                    progress.resetProgress();
                }
            }
        }
    }
}
