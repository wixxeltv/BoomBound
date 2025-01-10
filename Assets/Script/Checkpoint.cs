using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    AudioSource checkpointAudio;
    Animator animator;
    public Transform spawnPoint;

    private bool getCheckpoint;

    void Start()
    {
        getCheckpoint = false;
        checkpointAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (!getCheckpoint)
            {
                checkpointAudio.Play();
                getCheckpoint = true;
            }
            animator.SetTrigger("Activation");
            
            CatMovements playerMovements = collision.GetComponent<CatMovements>();
            if (playerMovements != null)
            {
                playerMovements.ChangeRespawnPoint(spawnPoint.transform.position);
            }
        }
    }

}
