using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    private Timer Script;
    public Animator playerAnimator;

    private IsFalling isFallingScript;

    public AudioSource fallingGrassImpactSound;
    public AudioSource fallingRockImpactSound;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        Script = player.GetComponent<Timer>();
        isFallingScript = GameObject.Find("IsFallingZone").GetComponent<IsFalling>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Script.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isFallingScript.hasFallen)
        {
            playerAnimator.SetBool("HasFallen", true);
            isFallingScript.hasFallen = false;

            if (other.gameObject.tag == "Grass")
            {
                if (fallingGrassImpactSound && !fallingGrassImpactSound.isPlaying)
                {
                    fallingGrassImpactSound.Play();
                }
            }

            if (other.gameObject.tag == "Rock")
            {
                if (fallingRockImpactSound && !fallingRockImpactSound.isPlaying)
                {
                    fallingRockImpactSound.Play();
                }
            }
        }
    }
}
