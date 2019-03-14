using System.Collections; // Required for Arrays & other Collections
using System.Collections.Generic; // Required to use Lists or Dictionaries
using UnityEngine; // Required for Unity
using UnityEngine.SceneManagement; // For loading & reloading of scenes
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    static public Main S; // A singleton for Main
    static Dictionary <WeaponType, WeaponDefinition> WEAP_DICT;
    private bool paused;

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies; // Array of Enemy prefabs
    public float enemySpawnPerSecond = 0.5f; // # Enemies/second
    public float enemyDefaultPadding = 1.5f; // Padding for position
    public WeaponDefinition[] weaponDefinitions;
    public GameObject prefabPowerUp;
    public WeaponType[] powerUpFrequency = new WeaponType[]
    {
        WeaponType.blaster, WeaponType.blaster, WeaponType.spread, WeaponType.shield };

    private BoundsCheck bndCheck;

    [SerializeField]
    private Text scoretxt;

    [SerializeField]
    private Text timertxt;



    private int _score;
    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            scoretxt.text = "Score: " + value;
        }
    }



    private float Startime, _timer;



    public float timer
    {
        get { return _timer; }
        set
        {
            _timer = value;
            timertxt.text = "Time: " + (int)value;
        }
    }





    //potentially generate power up
    public void shipDestroyed(Enemy e)
    {
        if(Random.value <= e.powerUpDropChance)
        {
            int ndx = Random.Range(0, powerUpFrequency.Length);
            WeaponType puType = powerUpFrequency[ndx];
            //spawn a power up
            GameObject go = Instantiate(prefabPowerUp) as GameObject;
            PowerUp pu = go.GetComponent<PowerUp>();
            pu.SetType(puType);
            pu.transform.position = e.transform.position;
        }
    }
    void Awake()
    {
        Startime = Time.time;
        S = this;
        // Set bndCheck to reference the BoundsCheck component on this
         bndCheck = GetComponent<BoundsCheck>();
        // Invoke SpawnEnemy() once (in 2 seconds, based on default values)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);

        //A generic dictionary with weapontype as the key 
        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach(WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }
    }

    void Update()
    {
        timer = Time.time - Startime;
    }

    public void SpawnEnemy()
    {
        // Pick a random Enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
        // Position the Enemy above the screen with a random x position
        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }
        // Set the initial position for the spawned Enemy 
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;
        // Invoke SpawnEnemy() again
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }
    public void DelayedRestart(float delay)
    {
        // Invoke the Restart() method in delay seconds
        Invoke("Restart", delay);
    }
    public void Restart()
    {
        // Reload _Scene_0 to restart the game
        SceneManager.LoadScene("_Scene_0");
    }




    /// <summary>
    /// Static function that gets a WeaponDefinition from the WEAP_DICT static
/// protected field of the Main class.
/// </summary>
/// <returns>The WeaponDefinition or, if there is no WeaponDefinition with
/// the WeaponType passed in, returns a new WeaponDefinition with a
/// WeaponType of none..</returns>
/// <param name="wt">The WeaponType of the desired WeaponDefinition</param>
    static public WeaponDefinition GetWeaponDefinition (WeaponType wt)
    {
        if (WEAP_DICT.ContainsKey(wt))
        {
            return (WEAP_DICT[wt]);
        }

        // This returns a new WeaponDefinition with a type of WeaponType.none,
        // which means it has failed to find the right WeaponDefinition

        return (new WeaponDefinition());
    }

    public void onPause()
    {
        if(paused == true)
        {
            Time.timeScale = 1.0f;
            paused = false;
        }

        else
        {
            paused = true;
            Time.timeScale = 0;
        }

    }
}
