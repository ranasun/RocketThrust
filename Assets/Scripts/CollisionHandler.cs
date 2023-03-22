using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip failed;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem failedParticle;
    
    int levelCount;
    int currentSceneIndex;
    bool isTransitioning;

    AudioSource audioSource;


    void Start() {
        isTransitioning = false;
        levelCount = SceneManager.sceneCount;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        if (isTransitioning) return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Bumped into friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                isTransitioning = true;
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence() {
        Transition();
        audioSource.PlayOneShot(success);
        successParticle.Play();
        Invoke("LoadNextLevel", 3.5f);
    }

    void StartCrashSequence() {
        Transition();
        audioSource.PlayOneShot(failed);
        failedParticle.Play();
        Invoke("ReloadLevel", 2f);
    }

    void Transition() {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Thrust>().enabled = false;
    }

    void ReloadLevel() {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel() {
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex > SceneManager.sceneCount) {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
