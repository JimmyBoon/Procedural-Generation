using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    [SerializeField] List<GameObject> clouds = new List<GameObject>();
    [SerializeField] Transform spawnPos;
    [SerializeField] GameObject skyDome;

    Renderer skyMaterial;
    int cloudCounter = 0;
    float offset = 0f;

    public void AddCloudCounter()
    {
        cloudCounter ++;
    }


    // Start is called before the first frame update
    void Start()
    {
        skyMaterial = skyDome.GetComponent<Renderer>();
        StartCoroutine(SpawnClouds());
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * 0.02f;
        skyMaterial.material.mainTextureOffset = new Vector2(offset, 0);
        if(cloudCounter == clouds.Count -1)
        {
            cloudCounter = 0;
            StartCoroutine(SpawnClouds());
        }

    }

    IEnumerator SpawnClouds()
    {
        foreach(GameObject cloud in clouds)
        {
            yield return new WaitForSeconds(Random.Range(1,5));
            cloud.transform.position = spawnPos.position + new Vector3(0,0, Random.Range(-150,150));
            cloud.SetActive(true);
        }
    }
}
