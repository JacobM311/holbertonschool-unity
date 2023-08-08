using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    private Timer Script;
    public Animator playerAnimator;

    private IsFalling isFallingScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        Script = player.GetComponent<Timer>();

        // Directly find the IsFalling script from the IsFallingZone GameObject
        isFallingScript = GameObject.Find("IsFallingZone").GetComponent<IsFalling>();
    }

    // Update is called once per frame
    void Update()
    {

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
            Vector3 adjustedPosition = other.transform.position;
            adjustedPosition.y -= 2.55f;
            other.transform.position = adjustedPosition;

            playerAnimator.SetBool("HasFallen", true);
            isFallingScript.ResetHasFallen();
        }
    }
}
