using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float crashDelay = 1f;
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    
    Movement movement;
    AudioSource audioSource;

    private void Start() 
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        {
            switch(other.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    private void StartCrashSequence()
    {
        movement.enabled = false;
        audioSource.PlayOneShot(crashSFX);
        Invoke("ReloadLevel", crashDelay);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void StartSuccessSequence()
    {
        movement.enabled = false;
        audioSource.PlayOneShot(successSFX);
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
