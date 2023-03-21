using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;
    float speed = 0f;
    float ySpeed = 0f;
    float mass = 1; // kg
    float force = 4;
    float drag = 1;
    float gravity = -9.8f;
    float gAcceleration;
    float acceleration;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        acceleration = force / mass;
        // Accelerate after 1 second
        speed += acceleration * 1;
        // Deccelerate gravity
        gAcceleration = gravity / mass;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Deccelerate bullet
        speed *= (1 - Time.deltaTime * drag);
        ySpeed += gAcceleration * Time.deltaTime;
        this.transform.Translate(0, ySpeed, Time.deltaTime * speed);
    }
}
