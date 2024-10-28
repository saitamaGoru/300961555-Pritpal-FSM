using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
   void OnCollisionEnter(Collision other)
   {
        if(other.gameObject)
        {
            Destroy(other.gameObject);
        }
   }
}
