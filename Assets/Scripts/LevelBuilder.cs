using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Block Types:")]
    [SerializeField] GameObject startTilePrefab;
    [SerializeField] GameObject endTilePrefab;
    [SerializeField] GameObject edgePrefab;
    [SerializeField] GameObject featureTilePrefab;
    [SerializeField] GameObject downTilePrefab;
    [SerializeField] List<GameObject> thingsForFeatures = new List<GameObject>();

    [Header("Prefabs for levels, 0 = Ground")]
    [SerializeField] GameObject[] blockPrefabArray;
    [SerializeField] Transform parentTransform;
    [SerializeField] int maxXPos = 10;
    [SerializeField] int maxZPos = 10;
    [SerializeField] List<GameObject> props = new List<GameObject>();

    RandPerm randPerm = new RandPerm();
    List<int> endTileIndex;
    Grid grid;


    Dictionary<Vector3Int, GameObject> LocationsStatusDict = new Dictionary<Vector3Int, GameObject>();

    GameManager manager;
    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        manager.GameEnded += HandleGameEnd;
    }

    private void HandleGameEnd()
    {
        Debug.Log("Handle End Game from level builder");
        // for(int x = -maxXPos; x == maxXPos; x++)
        // {
        //     for (int z = -maxZPos; z == maxZPos; z++)
        //     {
        //         Vector3Int position = new Vector3Int(x,0,z);
        //         // Rigidbody[] rigidbodies;

        //         // rigidbodies = LocationsStatusDict[position].GetComponentsInChildren<Rigidbody>();
        //         // foreach (Rigidbody body in rigidbodies)
        //         // {
        //         //     body.isKinematic = false;
        //         // }

        //         Debug.Log(position);

        //         Destroy(LocationsStatusDict[position]);

        //         LocationsStatusDict.Remove(position);
        //     }

        // }



        // foreach (KeyValuePair<Vector3Int, GameObject> entry in LocationsStatusDict)
        // {
        //     GameObject section = entry.Value;
        //     //Destroy(section);
        //     Rigidbody[] rigidbodies;

        //     rigidbodies = section.GetComponentsInChildren<Rigidbody>();
        //     foreach (Rigidbody body in rigidbodies)
        //     {
        //         body.isKinematic = false;
        //     }
        // }
    }

    void Start()
    {
        grid = GetComponent<Grid>();
        endTileIndex = randPerm.RandomPerm(441);
        BuildEnds();

        BuildStart();
        
        BuildDownTile();

        BuildFeature();
        BuildFeature();
        BuildFeature();
        BuildFeature();
    }

    private void BuildStart()
    {
        GameObject blockInstance = Instantiate(startTilePrefab, grid.CellToWorld(new Vector3Int(0, 0, 0)), transform.rotation, parentTransform);
        blockInstance.GetComponent<LevelSection>().SetStartTile();
        LocationsStatusDict.Add(new Vector3Int(0, 0, 0), blockInstance);
    }

    public void CheckAndBuild(Vector3Int gridLocation, List<Vector3Int> buildDirections)
    {
        foreach (Vector3Int direction in buildDirections)
        {
            Vector3Int buildLocationOnGrid = gridLocation + direction;


            bool outsideBuildLimits = (buildLocationOnGrid.x > maxXPos || buildLocationOnGrid.x < -maxXPos || buildLocationOnGrid.z > maxXPos || buildLocationOnGrid.z < -maxXPos);

            if (!LocationsStatusDict.ContainsKey(buildLocationOnGrid) && outsideBuildLimits)
            {
                BuildLevelSection(buildLocationOnGrid, edgePrefab);
                continue;
            }

            if (!LocationsStatusDict.ContainsKey(buildLocationOnGrid))
            {

                int level = -buildLocationOnGrid.y;
                BuildLevelSection(buildLocationOnGrid, blockPrefabArray[level]);

            }
        }
    }

    private void BuildLevelSection(Vector3Int buildLocationOnGrid, GameObject sectionPrefab)
    {
        Vector3 buildLocationInWorld = grid.CellToWorld(buildLocationOnGrid);
        GameObject blockInstance = Instantiate(sectionPrefab, buildLocationInWorld, transform.rotation, parentTransform);
        LocationsStatusDict.Add(buildLocationOnGrid, blockInstance);
        LevelSection blockLevelSection = blockInstance.GetComponent<LevelSection>();
        blockLevelSection.SetGridLocation(buildLocationOnGrid);
        blockLevelSection.SetLimits(maxXPos,maxZPos);
    }

    private void BuildEnds()
    {

        BuildLevelSection(new Vector3Int(0, 0, 11), endTilePrefab);
        BuildLevelSection(new Vector3Int(0, 0, -11), endTilePrefab);
        BuildLevelSection(new Vector3Int(11, 0, 0), endTilePrefab);
        BuildLevelSection(new Vector3Int(-11, 0, 0), endTilePrefab);

    }

    private void BuildFeature()
    {
        if(thingsForFeatures.Count < 1) { return; }

        List<int> randomPositionsX = randPerm.RandomPerm((maxXPos-1) * 2);
        List<int> randomPositionsZ = randPerm.RandomPerm((maxZPos - 1) * 2);



        for (int i = 0; i < maxXPos; i++)
        {
            bool notClearToBuild = false;

            int positionX = randomPositionsX[i] - maxXPos;
            int positionZ = randomPositionsZ[i] - maxZPos;
            Vector3Int[] positions = new Vector3Int[] { new Vector3Int(positionX, 0, positionZ), new Vector3Int(positionX, 0, positionZ + 1), new Vector3Int(positionX + 1, 0, positionZ), new Vector3Int(positionX + 1, 0, positionZ + 1) };

            foreach (Vector3Int pos in positions)
            {
                if (LocationsStatusDict.ContainsKey(pos))
                {
                    notClearToBuild = true;
                }
            }

            if (notClearToBuild)
            {
                continue;
            }

            Vector3 center1 = grid.CellToWorld(positions[0]);
            Vector3 center2 = grid.CellToWorld(positions[3]);

            Vector3 center = (center1 + center2) / 2;
            Instantiate(thingsForFeatures[0], center + new Vector3(0,0.4f,0), thingsForFeatures[0].transform.rotation);
            thingsForFeatures.RemoveAt(0);

            BuildLevelSection(positions[0], featureTilePrefab);
            BuildLevelSection(positions[1], featureTilePrefab);
            BuildLevelSection(positions[2], featureTilePrefab);
            BuildLevelSection(positions[3], featureTilePrefab);

            break;
        }

    }

    private void BuildDownTile()
    {
        Vector3Int position = new Vector3Int(2, 0, 2);
        BuildLevelSection(position, downTilePrefab);
    }
}
