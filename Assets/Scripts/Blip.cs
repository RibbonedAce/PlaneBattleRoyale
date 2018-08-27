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

    /// <summary>
    /// <para>The target to track</para>
    /// </summary>
    private Transform target;

    /// <summary>
    /// <para>The transform to base distances on</para>
    /// </summary>
    private Transform source;

    /// <summary>
    /// <para>The maximum distance of the blip from the source</para>
    /// </summary>
    private float range;

    /// <summary>
    /// <para>Whether the blip has been set up yet</para>
    /// </summary>
    private bool ready;
    #endregion

    #region Properties
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
        ready = false;
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
        if (ready)
        {
            // Check if target destroyed
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            // Position
            Vector3 diff = Vector3.ProjectOnPlane(target.position - source.position, Vector3.up);
            float angle = Vector3.SignedAngle(Vector3.ProjectOnPlane(source.forward, Vector3.up), diff, Vector3.up);
            _rectTransform.anchoredPosition = origin + Utils.VectorFromAngle(angle, 100 * diff.magnitude / range);

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
    /// Set up the blip's parameters
    /// </summary>
    /// <param name="_target">The target to track</param>
    /// <param name="_source">The source to base distances on</param>
    /// <param name="_range">The maximum distance of the blip</param>
	public void Setup(Transform _target, Transform _source, float _range)
    {
        target = _target;
        source = _source;
        range = _range;
        ready = true;
    }
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
