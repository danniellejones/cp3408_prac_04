using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Move : MonoBehaviour {

    public GameObject goal;

    Vector3 direction;
    public float speed = 2f;

    void Start() {

        //direction = goal.transform.position - this.transform.position;
        //this.transofrm.Translate(direction);
    }

    private void LateUpdate() {
        
        direction = goal.transform.position - this.transform.position;
        // Make the character look at their goal
        this.transform.LookAt(goal.transform.position);
        // Stop within a threshold value to goal, using magnitude checking will allow to follow goal
        if (direction.magnitude > 2)
        {
            // Normalise scales the vector to set units, deltaTime ensures it runs consistently
            Vector3 velocity = direction.normalized * speed * Time.deltaTime;
            this.transform.position = this.transform.position + velocity;
        }
    }
}
