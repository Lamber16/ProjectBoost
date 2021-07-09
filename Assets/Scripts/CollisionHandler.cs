using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        {
            switch(other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Friendly collision!)");
                    break;
                case "Finish":
                    Debug.Log("Finished the level!");
                    break;
                case "Fuel":
                    Debug.Log("Obtained more fuel!");
                    break;
                default:
                    Debug.Log("Damage taken!");
                    break;
            }
        }
    }
}
