using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthIndicator : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>The color gradient that the image goes through</para>
    /// </summary>
    [SerializeField]
    private Gradient color;

    /// <summary>
    /// <para>The Image component attached</para>
    /// </summary>
    private Image _image;
    #endregion

    #region Properties

    #endregion

    #region Events
    /// <summary>
    /// Awake is called before start
    /// </summary>
    private void Awake()
    {
        _image = GetComponent<Image>();
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
        _image.color = color.Evaluate((float)PlayerPlane.Instance.Health / PlayerPlane.Instance.MaxHealth);
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
