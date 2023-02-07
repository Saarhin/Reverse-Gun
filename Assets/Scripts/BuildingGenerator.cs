using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] buildingElementPrefabs;
    internal Queue<GameObject> buildingElements;
    internal List<GameObject> floorParents;
    GameObject buildingParent;
    int topMostGeneratedFloor;
    [SerializeField] int generatedFloorCount;
    private void Start()
    {
        floorParents = new List<GameObject>();
        buildingElements = new Queue<GameObject>();
        buildingParent = new GameObject("BuildintParent");
        for (int i = 0; i < generatedFloorCount; i++)
        {
            GenerateNextFloor();
        }
    }

    public void GenerateNextFloor()
    {
        topMostGeneratedFloor++;
        if (topMostGeneratedFloor > generatedFloorCount)
            DestroyOffCameraFloor();
        GameObject floorParent = new GameObject("FloorParent");
        for (int j = -1; j <= 1; j++)
        {
            GameObject buildingElement = Instantiate(buildingElementPrefabs[Random.Range(0, buildingElementPrefabs.Length)], new Vector2(j * GameManager.instance.mapElementRadius, topMostGeneratedFloor * GameManager.instance.mapElementRadius - (GameManager.instance.mapElementRadius / 2)), Quaternion.identity,floorParent.transform);
            buildingElements.Enqueue(buildingElement);
        }
        floorParents.Add(floorParent);
        floorParent.transform.parent = buildingParent.transform;
    }

    private void DestroyOffCameraFloor()
    {
        Destroy(floorParents[0]);
        floorParents.RemoveAt(0);
        for (int i = 0; i < 3; i++)
        {
            GameObject gameObject = buildingElements.Dequeue();
            if (gameObject.GetComponent<BuildingElement>().isBroken == true)
            {
                GameManager.instance.Lose();
            }
            Destroy(gameObject);
        }
    }

}
