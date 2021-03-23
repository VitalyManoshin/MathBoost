using UnityEngine;
using UnityEngine.SceneManagement;

public class Stick: MonoBehaviour
{

    [SerializeField] float rcsThrust = 300f;
    [SerializeField] float mainThrust = 3000f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathExplosion;
    [SerializeField] AudioClip loadLevel;

    Score scoreBoard;


    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        scoreBoard = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
        //load next level debug
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; } //ignore collisions

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (state != State.Alive) { return; } //ignore collisions

        switch (other.gameObject.tag)
        {
            case "Correct":
                Destroy(other.gameObject);
                CorrectAnswerSequence();
                break;
            default:
                break;
        }
    }

    private void CorrectAnswerSequence()
    {
        scoreBoard.IncreaseScore();
        //state = State.Transcending;
        //audioSource.Stop();
        //audioSource.PlayOneShot(loadLevel);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathExplosion);
        Invoke("Load—urrentLevel", 1f);
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        //audioSource.PlayOneShot(loadLevel);
        Invoke("LoadNextLevel", 1f);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void Load—urrentLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex;
        int levelsTotal = SceneManager.sceneCountInBuildSettings;

        nextSceneIndex = (currentSceneIndex +      1)%levelsTotal;

        SceneManager.LoadScene(nextSceneIndex); 
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);

        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;
    }

    private void Thrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust(thrustThisFrame);
        }

        else
        {
            audioSource.Stop();
        }
    }

    private void ApplyThrust(float thrustThisFrame)
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }
}
