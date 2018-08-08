using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DirectionUI : MonoBehaviour 
{
    #region Variables
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
        _text.text = string.Format("{0:000}", (Vector3.SignedAngle(Vector3.forward, Vector3.ProjectOnPlane(PlayerPlane.Instance.transform.forward, Vector3.up), Vector3.up) + 360) % 360);
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
