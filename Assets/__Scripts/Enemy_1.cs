using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    [Header("Enemy_1 Inscribed Fields")]
    [Tooltip("# of seconds for a full sine wave")]
    public float waveFreqeuency = 2;
    [Tooltip("Sine wave width in meters")]
    public float waveWidth = 4;
    [Tooltip("Amount the ship will roll left and right with the sine wave")]
    public float waveRotY = 45;

    private float x0; // Initial x value of pos
    private float birthTime;

    void Start() {
        // Set x0 to the initial x position of Enemy_1
        x0 = pos.x;

        birthTime = Time.time;
    }

    // Override the Move function on Enemy
    public override void Move() {
        Vector3 tempPos = pos;
        // Theta adjusts based on time
        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFreqeuency;
        float sin = Mathf.Sin(theta);
        tempPos.x = x0 + waveWidth * sin;
        pos = tempPos;

        // Rotate a bit about y
        Vector3 rot = new Vector3(0, sin *waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        // base.Move() still handles the movement down in y
        base.Move();
        // print (bndCheck.isOnScreen);
    }
}
