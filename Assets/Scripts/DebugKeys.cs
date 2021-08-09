using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugKeys : MonoBehaviour
{
    CollisionHandler collisionHandler;

    void Start()
    {
        collisionHandler = GetComponent<CollisionHandler>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currSceneIndex + 1;
            if(nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            collisionHandler.ToggleCollision();
        }
    }
}