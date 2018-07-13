using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class Blip : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>The origin of the radar</para>
    /// </summary>
    private static Vector2 origin = Vector2.one * 100;

    /// <summary>
    /// <para>The Rect Transform component attached</para>
    /// </summary>
    private RectTransform _rectTransform;

    /// <summary>
    /// <para>The Image component attached</para>
    /// </summary>
    private Image _image;

    /// <summary>
    /// <para>The coroutine controlling the flashing</para>
    /// </summary>
    private Coroutine flashRoutine;

    /// <summary>
    /// <para>The starting color of the image</para>
    /// </summary>
    private Color color;
    #endregion

    #region Properties
    /// <summary>
    /// <para>The target to track</para>
    /// </summary>
    public Transform Target { get; set; }

    /// <summary>
    /// <para>Whether the blip is actively used</para>
    /// </summary>
    public bool Active { get; set; }
    #endregion

    #region Events
    /// <summary>
    /// Awake is called before start
    /// </summary>
    private void Awake()
	{
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        color = _image.color;
        Active = false;
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
        // Check if target destroyed
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Position
        Vector3 diff = Vector3.ProjectOnPlane(Target.position - Targeting.Instance.Source.position, Vector3.up);
        float angle = Vector3.SignedAngle(Vector3.ProjectOnPlane(Targeting.Instance.Source.forward, Vector3.up), diff, Vector3.up);
        _rectTransform.anchoredPosition = origin + Utils.VectorFromAngle(angle, 100 * diff.magnitude / Targeting.Instance.Range);

        // Check if active
        if (!Active && flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            flashRoutine = null;
            _image.color = color;
        }
        else if (Active && flashRoutine == null)
        {
            flashRoutine = StartCoroutine(Flash(1f));
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
    /// <summary>
    /// Flash on the screen once every given time
    /// </summary>
    /// <param name="time">How many seconds one flash takes</param>
    /// <returns></returns>
	private IEnumerator Flash(float time)
    {
        while (true)
        {
            _image.color = Color.clear;
            yield return new WaitForSeconds(time / 2);
            _image.color = color;
            yield return new WaitForSeconds(time / 2);
        }
    }
	#endregion
}
