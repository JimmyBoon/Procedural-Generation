using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSection : MonoBehaviour
{
    LevelBuilder levelBuilder;
    WallBuilder wallBuilder;

    List<Vector3Int> buildDirections = new List<Vector3Int>();
    int maxXPos;
    int maxZPos;

    [Header("Build Radius")]
    [SerializeField] int buildToX = 1;
    [SerializeField] int buildToZ = 1;

    [Header("Tile Settings/Info")]
    [SerializeField] Vector3Int gridPosition;
    [SerializeField] bool edge;
    [SerializeField] bool endTile;
    [SerializeField] bool startTile;
    [SerializeField] bool downTile;

    [Header("Wall Directions")]
    [SerializeField] bool topWall = true;
    [SerializeField] bool leftWall = true;
    [SerializeField] bool rightWall = true;
    [SerializeField] bool bottomWall = true;



    Rigidbody[] rigidbodies;

    private void Awake()
    {

        SetBuildDirections();
    }

    private void SetBuildDirections()
    {
        for (int x = -buildToX; x < buildToX + 1; x++)
        {
            for (int z = -buildToZ; z < buildToZ + 1; z++)
            {
                buildDirections.Add(new Vector3Int(x, 0, z));
            }

        }

        if(downTile)
        {
            buildDirections.Add(new Vector3Int(0, -1, 0));
        }
    }

    public void SetLimits(int maxX, int maxZ)
    {
        maxXPos = maxX;
        maxZPos = maxZ;
    }

    private void Start()
    {
        CalculateWallDirections();

        wallBuilder = GetComponent<WallBuilder>();
        if (wallBuilder)
        {
            wallBuilder.SetWallDirections(topWall, leftWall, rightWall, bottomWall);
            wallBuilder.BuildWalls();
        }


        //    Todo remove this somehow to reduce cirle dependencies
        if (!edge)
        {
            levelBuilder = FindObjectOfType<LevelBuilder>();
            levelBuilder.CheckAndBuild(gridPosition, buildDirections);
        }
    }

    private void CalculateWallDirections()
    {
        if (startTile)
        {
            topWall = false;
            leftWall = false;
            rightWall = false;
            bottomWall = false;
            return;
        }

        if ((gridPosition.x + 1) > maxXPos)
        {
            rightWall = false;
        }
        if ((gridPosition.z + 1) > maxZPos)
        {
            topWall = false;
        }
        if ((gridPosition.x - 1) < -maxXPos)
        {
            leftWall = false;
        }
        if ((gridPosition.z - 1) < -maxZPos)
        {
            bottomWall = false;
        }
    }

    public void SetGridLocation(Vector3Int location)
    {
        gridPosition = location;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (!edge && gridPosition.y < 0 && other.CompareTag("Player"))
        // {
        //     levelBuilder = FindObjectOfType<LevelBuilder>();
        //     levelBuilder.CheckAndBuild(gridPosition, buildDirections);
        // }
        if (endTile && other.CompareTag("Player"))
        {
            Debug.Log("End tile");
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public void SetStartTile()
    {
        startTile = true;
    }

    public bool GetStartTile()
    {
        return startTile;
    }
}
