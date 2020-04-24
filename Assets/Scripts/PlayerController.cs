using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidBody;
    private Animator playerAnim;
    public ParticleSystem obstacleExplosion;
    public ParticleSystem dirtSplat;
    public AudioClip jumpSound;
    public AudioClip explosionSound;
    private AudioSource audioSource;
    public float forceMultiplier;
    public float gravityMultiplier;
    public bool onGround = true;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Physics.gravity *= gravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            playerRigidBody.AddForce(Vector3.up * forceMultiplier, ForceMode.Impulse);
            onGround = false;
            dirtSplat.Stop();
            playerAnim.SetTrigger("Jump_trig");
            audioSource.PlayOneShot(jumpSound, 1.0f);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //game over if the player hits an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            dirtSplat.Stop();
            gameOver = true;
            obstacleExplosion.Play();
            Debug.Log("Game Over");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            audioSource.PlayOneShot(explosionSound, 1.0f);
        }

        //set on ground state to true if we hit the ground
        else if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            dirtSplat.Play();
        }
    }
}
