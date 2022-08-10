using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    /// <summary>
    /// Ref : https://www.youtube.com/watch?v=u7lwL7ubwKA&ab_channel=Sykoo
    /// </summary>
    public class GrenadeBehaviour : MonoBehaviour
    {
        [Header("Travel")]
        [SerializeField] private float moveSpeed;

        [Space]
        [Header("Explosion")] 
        [SerializeField] private float radius;
        [SerializeField] private float power;
        [SerializeField] private float explosionDelay;
        
        private void Start()
        {
            // ThrowGrenade();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(2))
            {
                StartCoroutine("BombTimer");    
            }
        }

        private void ThrowGrenade()
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
        }

        IEnumerator BombTimer()
        {
            yield return new WaitForSeconds(explosionDelay);
            Explode();
        }
        
        public void Explode()
        {
            Vector3 explosionPos = transform.position;
            
            // Physics.OverlapSphere returns an array with all colliders are either touching or are inside the sphere with a certain radius.
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                
                if(rb != null)
                    rb.AddExplosionForce(power,explosionPos,radius,3.0f);
            }
        }
    }
}
