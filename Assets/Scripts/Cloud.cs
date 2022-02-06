using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float destroyPos = -180f;

    Weather weather;

    private void Start()
    {
        weather = FindObjectOfType<Weather>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if(transform.position.x < destroyPos)
        {
            weather.AddCloudCounter();
            gameObject.SetActive(false);
        }
    }
}
