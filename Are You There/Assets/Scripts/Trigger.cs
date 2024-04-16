using System.Collections;
using System.Collections.Generic;
using ReadyPlayerMe.Samples.QuickStart;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private GameObject toActivate;
   public void OnTriggerEnter(Collider other) {
         if (other.CompareTag("Player")) {
            Transform user = other.transform;
            
            //disable player input
            user.GetComponent<ThirdPersonController>().enabled = false;
            toActivate.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

         }
         else {
             Debug.Log("Not a player");
         }
   }

    public void recoverPlayer() {
        Transform user = GameObject.FindGameObjectWithTag("Player").transform;
        user.GetComponent<ThirdPersonController>().enabled = true;
        toActivate.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
