using UnityEngine;

public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        const float tau = Mathf.PI * 2; //Constant value of 2.683...

        if (period == Mathf.Epsilon){return;}
        float cycles = Time.time / period; //Continually grows over time
        float rawSinWave = Mathf.Sin(cycles * tau); //Going from -1 to 1
        float movementFactor = (rawSinWave+1f) / 2f;  //Recalculated to go from 0 to 1

        Vector3 offset = movementVector * movementFactor ;
        transform.position = startingPos + offset;
    }
}
