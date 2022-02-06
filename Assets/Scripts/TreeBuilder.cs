using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBuilder : MonoBehaviour
{
    [SerializeField] GameObject[] treePrefabs;
    [SerializeField] GameObject[] groundThingsPrefabs;
    [Range(0, 1)]
    [SerializeField] float growChance = 0.2f;

    Vector3[] positions = new Vector3[] { new Vector3(4, 0.5f, 4), new Vector3(4, 0.5f, -4), new Vector3(-4, 0.5f, 4), new Vector3(-4, 0.5f, -4) };
    // Start is called before the first frame update
    void Start()
    {

        foreach (Vector3 pos in positions)
        {
            if (treePrefabs.Length == 0) { break; }

            float chance = Random.Range(0, 1f);
            if (chance < growChance)
            {
                Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)], transform.position + pos, transform.rotation, transform);
            }
        }

        Instantiate(groundThingsPrefabs[Random.Range(0, treePrefabs.Length)], transform.position + new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f)), transform.rotation, transform);
        Instantiate(groundThingsPrefabs[Random.Range(0, treePrefabs.Length)], transform.position + new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f)), transform.rotation, transform);
        Instantiate(groundThingsPrefabs[Random.Range(0, treePrefabs.Length)], transform.position + new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f)), transform.rotation, transform);



    }

    // Update is called once per frame
    void Update()
    {

    }
}
