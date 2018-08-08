using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SpeedUI : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>The text to indicate stalling</para>
    /// </summary>
    [SerializeField]
    private Text stallText;

    /// <summary>
    /// <para>The rigidbody to monitor speed</para>
    /// </summary>
    private Rigidbody _rigidbody;

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
        _rigidbody = PlayerPlane.Instance.GetComponent<Rigidbody>();
    }
	
	/// <summary>
    /// Update is called once per frame
    /// </summary>
	private void Update() 
	{
        _text.text = string.Format("Speed:\n{0:0.0}", _rigidbody.velocity.magnitude);
        if (stallText != null && _rigidbody.useGravity)
        {
            stallText.text = "STALLING";
        }
        else if (stallText != null && !_rigidbody.useGravity)
        {
            stallText.text = "";
        }
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
