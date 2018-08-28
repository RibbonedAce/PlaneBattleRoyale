using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MissileAlertUI : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>The transform to track missile targeting</para>
    /// </summary>
    [SerializeField]
    private Transform source;

    /// <summary>
    /// <para>A missile that is attacking the source</para>
    /// </summary>
    private Missile currentMissile;

    /// <summary>
    /// <para>The Text component attached</para>
    /// </summary>
    private Text _text;
	#endregion
	
	#region Properties
	
	#endregion
	
	#region Events
	/// <summary>
    /// Awake is called before start
    /// </summary>
	private void Awake()
	{
        _text = GetComponent<Text>();	
	}
	
	/// <summary>
    /// Use this for initialization
    /// </summary>
	private void Start() 
	{
		
	}
	
	/// <summary>
    /// Update is called once per frame
    /// </summary>
	private void Update() 
	{
        if (currentMissile == null)
        {
            foreach (Missile m in Missile.Missiles)
            {
                if (m.Target == source)
                {
                    currentMissile = m;
                    break;
                }
            }
        }
        _text.text = currentMissile != null ? "MISSILE ALERT" : "";
	}
	
	/// <summary>
    /// Use this for physics-related changes
    /// </summary>
	private void FixedUpdate()
	{
		
	}
	#endregion
	
	#region Methods
	
	#endregion
	
	#region Coroutines
	
	#endregion
}
