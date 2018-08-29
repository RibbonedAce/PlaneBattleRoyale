using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextUI : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>The Text component attached</para>
    /// </summary>
    protected Text _text;
	#endregion
	
	#region Properties
	
	#endregion
	
	#region Events
	/// <summary>
    /// Awake is called before start
    /// </summary>
	protected virtual void Awake()
	{
        _text = GetComponent<Text>();
	}

    /// <summary>
    /// Use this for initialization
    /// </summary>
    protected virtual void Start() 
	{
		
	}

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected virtual void Update() 
	{
		
	}

    /// <summary>
    /// Use this for physics-related changes
    /// </summary>
    protected virtual void FixedUpdate()
	{
		
	}
	#endregion
	
	#region Methods
	
	#endregion
	
	#region Coroutines
	
	#endregion
}
