using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] float thrust = 100f;
    [SerializeField] float rotationSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            rigidbody.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * Time.deltaTime * rotationSpeed);
        }
    }
}
