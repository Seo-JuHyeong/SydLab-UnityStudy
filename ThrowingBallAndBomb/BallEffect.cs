using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem particle;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player")) return;
        Instantiate(particle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
