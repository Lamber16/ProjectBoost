using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float crashDelay = 1f;
    [SerializeField] float levelLoadDelay = 1f;

    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    
    Movement movement;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    private void Start() 
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        {
            if (isTransitioning || collisionDisabled){return;}

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
        isTransitioning = true;
        movement.enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        Invoke("ReloadLevel", crashDelay);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        movement.enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
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

    public void ToggleCollision()
    {
        collisionDisabled = !collisionDisabled;
    }
}
