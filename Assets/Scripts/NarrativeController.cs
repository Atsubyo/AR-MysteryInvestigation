using System.Collections.Generic;
using UnityEngine;

static class NarrativeController
{
    public static void IsolateNarrativePiece(ref List<GameObject> narrativePieces, int idx)
    {
        for (int i = 0; i < narrativePieces.Count; ++i)
        {
            narrativePieces[i].SetActive(i == idx);
        }
    }
}
