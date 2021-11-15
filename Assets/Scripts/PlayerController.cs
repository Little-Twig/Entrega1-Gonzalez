using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int lifePlayer = 3;

    [SerializeField] private float cameraAxisX = -90f;
    [SerializeField] private float speedPlayer = 3f;

    [SerializeField] private Vector3 swordPosition = new Vector3(0, 0, 0.3f);

    [SerializeField] private Animator animPlayer;
    [SerializeField] private AudioClip punchSound;
    [SerializeField] private AudioClip walkSound;

    private AudioSource audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        animPlayer.SetBool("isRun", false);
        audioPlayer = GetComponent<AudioSource>();
    }
    void Update()
    {
        RotatePlayer();
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioPlayer.PlayOneShot(punchSound, 1f);
            animPlayer.SetBool("isPunch", true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animPlayer.SetBool("isPunch", false);
        }


    }
    private void Move()
    {
        float ejeHorizontal = Input.GetAxis("Horizontal");
        float ejeVertical   = Input.GetAxis("Vertical");

        if (ejeHorizontal != 0 || ejeVertical != 0) {
            animPlayer.SetBool("isRun", true);
            Vector3 direction = new Vector3(ejeHorizontal, 0, ejeVertical);
            transform.Translate(speedPlayer * Time.deltaTime * direction);
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(walkSound, 0.5f);
            }
        }
        else
        {
            animPlayer.SetBool("isRun", false);
        }
    }
    private void RotatePlayer()
    {
        cameraAxisX += Input.GetAxis("Mouse X");
        Quaternion angulo   = Quaternion.Euler(0, cameraAxisX, 0);
        transform.rotation = angulo;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lifePlayer--;
            Destroy(collision.gameObject);
            if(lifePlayer < 0)
            {
                Debug.Log("GAME OVER");
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Generator"))
        {
            Destroy(other.gameObject);
        }
    }
}
