using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickLink : MonoBehaviour
{
    public string url;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject)
            {
                OnObjectClicked();
            }
        }
    }

    private void OnObjectClicked()
    {
        Debug.Log("clicked");
        Application.OpenURL(url);
    }
}
