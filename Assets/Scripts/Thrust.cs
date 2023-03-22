using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] float thrust = 1000f;
    [SerializeField] float rotationAngle = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem leftThruster;
    [SerializeField] ParticleSystem rightThruster;
    [SerializeField] ParticleSystem booster;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            ApplyThrust();
        } else {
            audioSource.Stop();
            booster.Stop();
        }

        if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(rotationAngle);
            if (!rightThruster.isPlaying) {
                rightThruster.Play();
            }
        } else {
            rightThruster.Stop();
        }

        if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotationAngle);
            if (!leftThruster.isPlaying) {
                leftThruster.Play();
            }
        } else {
            leftThruster.Stop();
        }

    }

    void ApplyThrust() {
        rigidbody.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!booster.isPlaying) {
            booster.Play();
        }
    }

    void ApplyRotation(float rotationAngle) {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationAngle);
    }
}
