using UnityEngine;
using System.Collections;

public class PillGeneration : MonoBehaviour
{
    public Transform pillPrefab;
    public int numberOfPills;
    private float rangeNum = 75.0f;

    // Use this for initialization
    void Start()
    {
        Create();
    }

    void Create()
    {
        for (int currentPills = 0; currentPills < numberOfPills; currentPills++)
        {
            Transform pill = (Transform)Instantiate(pillPrefab, new Vector3(Random.Range(-rangeNum, rangeNum), 0.2F, Random.Range(-rangeNum, rangeNum)), pillPrefab.rotation);
            pill.name = "(" + pill.position.x + "," + pill.position.y + "," + pill.position.z + ")";
        }
    }
}
