using UnityEngine;
using System.Collections;

public class ParticleAttract : MonoBehaviour {

        public ParticleSystem particleSystem;

 
	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {

       
    
    
    }

     private void AddForceOnParticles()
     {
         
         ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[particleSystem.particleCount];
         this.particleSystem.GetParticles(_particles);
         
         for (int i = 0; i < _particles.Length; i++){
             // Here you can do whatever you want with each particle by using _particles[i] 
         }
 
         this.particleSystem.SetParticles(_particles, 15);
     }
}
