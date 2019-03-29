using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    static public Player S; // Singleton 
    [Header("Set in Inspector")]
    // These fields control the movement of the ship
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public Weapon[] weapons;


    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;
    private GameObject lastTriggerGo = null;
    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;
    void Start()
    {
        if (S == null)
        {
            S = this; // Set the Singleton 
            ClearWeapons();
            weapons[0].SetType(WeaponType.blaster);
        }
        else
        {
            Debug.LogError("Player.Awake() - Attempted to assign second Player.S!");
        }
       
    }
    void Update()
    {
        // Pull in information from the Input class
        float xAxis = Input.GetAxis("Horizontal"); 
        float yAxis = Input.GetAxis("Vertical"); 
      // Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;
        // Rotate the ship to make it feel more dynamic 
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
        // Allow the ship to fire
     //   if (Input.GetKeyDown(KeyCode.Space))
       // { 
         //   TempFire();
        //}

        if(Input.GetAxis("Jump")==1 && fireDelegate != null)
        {
            fireDelegate();
        }
    }



    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //print("Triggered: " + go.name);

        // Make sure it's not the same triggering go as last time
        if (go == lastTriggerGo)
        { 
            return;
        }
        lastTriggerGo = go; 
        if (go.tag == "Enemy")
        { // If the shield was triggered by an enemy
            shieldLevel--; // Decrease the level of the shield by 1
            Destroy(go); // … and Destroy the enemy 
        }
        else if (go.tag == "PowerUp")
        {
            AbsorbPowerUp(go);
        }
        else if (other.gameObject.tag == "ProjectileEnemy")
        {
            shieldLevel--; // Decrease the level of the shield by 1
            Destroy(other.gameObject);
        }
        else
        {
            print("Triggered by non-Enemy: " + go.name); 
        }
    
    }

    void OnCollisionEnter(Collision other)
    {
         if (other.gameObject.tag == "ProjectileEnemy")
        {
            shieldLevel--; // Decrease the level of the shield by 1
            Destroy(other.gameObject);
            Main.S.DelayedRestart(gameRestartDelay);
        }
    }

    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();
        switch (pu.type)
        {
            case WeaponType.shield:
                shieldLevel++;
                break;

            default: 
                if (pu.type == weapons[0].type)
                { // If it is the same type 
                    Weapon w = GetEmptyWeaponSlot();
                    if (w != null)
                    {
                        // Set it to pu.type
                        w.SetType(pu.type);
                    }
                }
                else
                { // If this is a different weapon type
                    ClearWeapons();
                    weapons[0].SetType(pu.type);
                }
                
               break;
        }
    
        pu.AbsorbedBy(this.gameObject);
    }
    public float shieldLevel
    {
        get
        {
            return (_shieldLevel); 
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
           // If the shield is going to be set to less than zero
            if (value < 0)
            { 
                Destroy(this.gameObject);
                PrefsHelper.EndHistory(Main.S.score, GameType.Space);
                // Tell Main.S to restart the game after a delay
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }

    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].type == WeaponType.none)
            {
                return (weapons[i]);
            }
        }
        return(null);
    }
    void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
            w.SetType(WeaponType.none);
        }
    }

}
