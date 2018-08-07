using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>How many rotations per second the object rotates</para>
    /// </summary>
    [SerializeField]
    private float rotationRate;

    /// <summary>
    /// <para>The color of the pickup</para>
    /// </summary>
    [SerializeField]
    private Color color;

    /// <summary>
    /// <para>How much ammo to receive</para>
    /// </summary>
    [SerializeField]
    private int ammo;

    /// <summary>
    /// <para>How much health to receive</para>
    /// </summary>
    [SerializeField]
    private int health;

    /// <summary>
    /// <para>How much to upgrade speed by</para>
    /// </summary>
    [SerializeField]
    private float speed;

    /// <summary>
    /// <para>How much to upgrade turn speed by</para>
    /// </summary>
    [SerializeField]
    private float turning;
    #endregion

    #region Properties
    /// <summary>
    /// <para>How much ammo to receive</para>
    /// </summary>
    public int Ammo
    {
        get
        {
            return ammo;
        }
    }

    /// <summary>
    /// <para>How much health to receive</para>
    /// </summary>
    public int Health
    {
        get
        {
            return health;
        }
    }

    /// <summary>
    /// <para>How much to upgrade speed by</para>
    /// </summary>
    public float Speed
    {
        get
        {
            return speed;
        }
    }

    /// <summary>
    /// <para>How much to upgrade turn speed by</para>
    /// </summary>
    public float Turning
    {
        get
        {
            return turning;
        }
    }
    #endregion

    #region Events
    /// <summary>
    /// Awake is called before start
    /// </summary>
    private void Awake()
	{

	}
	
	/// <summary>
    /// Use this for initialization
    /// </summary>
	private void Start() 
	{
        foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
        {
            m.material.color = color;
        }
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
        {
            ParticleSystem.MainModule main = p.main;
            main.startColor = color;
        }
	}
	
	/// <summary>
    /// Update is called once per frame
    /// </summary>
	private void Update() 
	{
        transform.Rotate(Vector3.up * rotationRate * 360 * Time.deltaTime);
	}
	
	/// <summary>
    /// Use this for physics-related changes
    /// </summary>
	private void FixedUpdate()
	{
		
	}
    #endregion

    #region Methods
    /// <summary>
    /// Called when something collides with the Game Object's trigger
    /// </summary>
    /// <param name="other">The collider that entered the trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerPlane>().ApplyPickup(this);
            Destroy(gameObject);
        }
    }
    #endregion

    #region Coroutines

    #endregion
}
