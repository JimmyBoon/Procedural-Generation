using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    List<Vector3> wallDirections = new List<Vector3>();
    List<float> wallRotations = new List<float>();


    [SerializeField] GameObject wallPrefab;

    [Header("Wall Directions")]
    [SerializeField] bool topWall = true;
    [SerializeField] bool leftWall = true;
    [SerializeField] bool rightWall = true;
    [SerializeField] bool bottomWall = true;

    [Header("Wall Settings")]
    [SerializeField] float wallHeight = 2.0f;
    GameObject wallInstance;

    public void SetWallDirections(bool top, bool left, bool right, bool bottom)
    {
        topWall = top;
        leftWall = left;
        rightWall = right;
        bottomWall = bottom;
    }

    private void CalculateWallDirections()
    {

        if (topWall)
        {
            wallDirections.Add(new Vector3(0, wallHeight, 5f));
            wallRotations.Add(90f);

        }
        if (leftWall)
        {
            wallDirections.Add(new Vector3(-5, wallHeight, 0));
            wallRotations.Add(0f);

        }
        if (rightWall)
        {
            wallDirections.Add(new Vector3(5f, wallHeight, 0));
            wallRotations.Add(0f);

        }
        if (bottomWall)
        {
            wallDirections.Add(new Vector3(0, wallHeight, -5f));
            wallRotations.Add(90f);

        }
    }

    public void BuildWalls()
    {
        CalculateWallDirections();

        if(wallDirections.Count == 0)
        { 
            return;
        }
        
        int index = Random.Range(0, wallDirections.Count);
        wallInstance = Instantiate(wallPrefab, transform.position + wallDirections[index], wallPrefab.transform.rotation, transform);
        wallInstance.transform.Rotate(Vector3.up, wallRotations[index]);
    }
}
