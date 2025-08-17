using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegementGenerator : MonoBehaviour
{
    public GameObject[] segment;
    [SerializeField] int zPos = 39;
    [SerializeField] bool creatingSegment = false;
    [SerializeField] int segmentNum;


    void Update()
    // This checks if the player has reached the end of the segment and if so, it generates a new segment
    {
        if (creatingSegment == false)
        {
            creatingSegment = true;
            StartCoroutine(SegmentGen());
        }

    }
    // This coroutine generates a segment at a random index from the segment array  
    IEnumerator SegmentGen()
    {
        segmentNum = Random.Range(0, 3);
        Instantiate(segment[segmentNum], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 50;
        yield return new WaitForSeconds(3);
        creatingSegment = false;
    }

}
