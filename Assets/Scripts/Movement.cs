using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;
    
    Rigidbody rb;
    AudioSource audioSource;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void ProcessRotation()
    {

        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        StartBooster(mainBooster);
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }


    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        StartBooster(rightBooster);
        leftBooster.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        StartBooster(leftBooster);
        rightBooster.Stop();
    }

    private void StopRotating()
    {
        leftBooster.Stop();
        rightBooster.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //Unfreeze rotation so physics system can take over
    }

    private void StartBooster(ParticleSystem booster)
    {
        if(!booster.isPlaying)
        {
            booster.Play();
        }
    }
}
