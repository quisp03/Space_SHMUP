using System.Collections;           // Required for some Arrays manipulation
using System.Collections.Generic;   // Required for Lists and Dictionaries
using UnityEngine;                  // Required for Unity

[RequireComponent(typeof(BoundsCheck))]
public class Enemy : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 10f; // The movement speed is 10m/s
    public float fireRate = 0.3f; // Seconds/shot (Unused)
    public float health = 10; // Damage needed to destroy this enemy
    public int score = 100; // Points earned for destroying this
    public float powerUpDropChance = 1f; // Chance to drop a PowerUp
    protected bool calledShipDestroyed = false;

    protected BoundsCheck bndCheck;
    // This is a Property: A method that acts like a field

    void Awake() {
        bndCheck = GetComponent<BoundsCheck>();
    }

    public Vector3 pos {
        get {
            return this.transform.position;
        }
        set {
            this.transform.position = value;
        }
    }

    void Update() {
        Move();
        
        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown)) {
            Destroy(gameObject);
        }
       /* if (!bndCheck.isOnScreen) {
            if (pos.y < bndCheck.camHeight - bndCheck.radius) {
                Destroy(gameObject);
            }
        }
        */
    }

    public virtual void Move() {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

  //  void OnCollisionEnter(Collision coll) {

      void OnCollisionEnter(Collision coll) {  
        GameObject otherGO = coll.gameObject;
        ProjectileHero p = otherGO.GetComponent<ProjectileHero>();
        if (p != null) {
            if (bndCheck.isOnScreen) {
                health -=Main.GET_WEAPON_DEFINITION(p.type).damageOnHit;
                if (health <= 0) {
                    AudioManager.Instance.PlaySound(AudioManager.Instance.shipExplosionSound);
                    Destroy(this.gameObject);
                
                if (!calledShipDestroyed) {
                    calledShipDestroyed = true;
                    Main.SHIP_DESTROYED(this);
                }
                
                Destroy(this.gameObject);
                }
            }
        
            Destroy(otherGO);
        }
        
        else {
            print("Enemy hit by non-ProjectileHero: "+otherGO.name);
        }
        /*if (otherGO.GetComponent<ProjectileHero>() != null) {
            Destroy(otherGO);
            Destroy(gameObject);
        }
        else {
            Debug.Log("Enemy hit by non-ProjectileHero: "+otherGO.name);
        } */
    } 
}
