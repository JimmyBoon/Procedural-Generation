using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    Rigidbody rb;
    [SerializeField] float jumpForce = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        
        transform.Translate(new Vector3(0, 0, vertical) * Time.deltaTime * speed);

        transform.Rotate(Vector3.up * horizontal * 5);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }   
        
    }
}
