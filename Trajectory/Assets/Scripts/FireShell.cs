using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShell : MonoBehaviour {

    public GameObject bullet;
    public GameObject turret;
    public GameObject enemy;
    public Transform turretBase;

    private float speed = 15.0f;
    private float rotSpeed = 2.0f;
    private float moveSpeed = 3.0f;

    static float delayReset = 0.2f;
    float delay = delayReset;

    // Creates the shells and shoot
    void CreateBullet() {

        GameObject shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = speed * turretBase.forward;
    }

    // Enemy tank turrent rotatoes towards player tank
    float? RotateTurret() {

        // Controls either high or low angle with boolean
        float? angle = CalculateAngle(false);
        //float? angle = CalculateAngle(true);

        if (angle != null) {

            turretBase.localEulerAngles = new Vector3(360.0f - (float)angle, 0.0f, 0.0f);
        }
        return angle;
    }


    // Low, high angle
    float? CalculateAngle(bool low) {

        // Direction to the target
        Vector3 targetDir = enemy.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0.0f; // only interested in other values as on flat plane
        float x = targetDir.magnitude - 1.0f;
        float gravity = 9.8f;
        float sSqr = speed * speed;
        // Trajectory equation calculation
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0.0f) {

            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            // Atan2 is inverse tan result
            if (low) return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            else return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
        } else
            return null;
    }

    void Update() {

        delay -= Time.deltaTime;
        // Turn the tank around to face the enemy
        Vector3 direction = (enemy.transform.position - this.transform.position).normalized;
        // Look rotation will turn over time rather than lookAt which will snap at, the last part is to make sure tank doesn't look into the air
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));

        // Slerp is a spherical interpolation - a more natural movement
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
        float? angle = RotateTurret();

        if (angle != null && delay <= 0.0f) {

            CreateBullet();
            delay = delayReset;
        } else {

            this.transform.Translate(0.0f, 0.0f, Time.deltaTime * moveSpeed);
        }
    }

    //Vector3 CalculateTrajectory() {

    //    Vector3 p = enemy.transform.position - this.transform.position;
    //    Vector3 v = enemy.transform.forward * enemy.GetComponent<Drive>().speed;
    //    float s = bullet.GetComponent<MoveShell>().speed;

    //    float a = Vector3.Dot(v, v) - s * s;
    //    float b = Vector3.Dot(p, v);
    //    float c = Vector3.Dot(p, p);
    //    float d = b * b - a * c;

    //    if (d < 0.1f) return Vector3.zero;

    //    float sqrt = Mathf.Sqrt(d);
    //    float t1 = (-b - sqrt) / c;
    //    float t2 = (-b + sqrt) / c;

    //    float t = 0.0f;
    //    if (t1 < 0.0f && t2 < 0.0f) return Vector3.zero;
    //    else if (t1 < 0.0f) t = t2;
    //    else if (t2 < 0.0f) t = t1;
    //    else {

    //        t = Mathf.Max(new float[] { t1, t2 });
    //    }
    //    return t * p + v;
    //}
}
