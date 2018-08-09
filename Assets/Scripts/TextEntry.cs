using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextEntry : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>The Text component attached</para>
    /// </summary>
    private Text _text;
    #endregion

    #region Properties
    /// <summary>
    /// <para>The text stored</para>
    /// </summary>
    public string Text
    {
        get
        {
            return _text.text;
        }
        set
        {
            _text.text = value;
        }
    }

    /// <summary>
    /// <para>The amount of time passed since start</para>
    /// </summary>
	public float Lifetime { get; private set; }
	#endregion
	
	#region Events
	/// <summary>
    /// Awake is called before start
    /// </summary>
	private void Awake()
	{
        _text = GetComponent<Text>();
        Lifetime = 0f;
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
        Lifetime += Time.deltaTime;
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
