using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class Reticle : MonoBehaviour 
{
    #region Variables

    /// <summary>
    /// <para>The sprite used when the Reticle is not locked on</para>
    /// </summary>
    private static Sprite lockFalseSprite;

    /// <summary>
    /// <para>The sprite used when the Reticle is locked on</para>
    /// </summary>
    private static Sprite lockTrueSprite;

    /// <summary>
    /// <para>The color taken on when inactive</para>
    /// </summary>
    private static Color inactiveColor;

    /// <summary>
    /// <para>The transform to base distances on</para>
    /// </summary>
    private Transform source;

    /// <summary>
    /// <para>The target to track</para>
    /// </summary>
    private Transform target;

    /// <summary>
    /// <para>The maximum distance to lock on at</para>
    /// </summary>
    private float lockDistance = 80f;

    /// <summary>
    /// <para>Whether the reticle has been set up</para>
    /// </summary>
    private bool ready;

    /// <summary>
    /// <para>The Rect Transform component attached</para>
    /// </summary>
    private RectTransform _rectTransform;

    /// <summary>
    /// <para>The Image component attached</para>
    /// </summary>
    private Image _image;
    #endregion

    #region Properties

    /// <summary>
    /// <para>Whether the reticle is actively used</para>
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// <para>Whether the Reticle is locked onto the target</para>
    /// </summary>
    private bool Locked
    {
        get
        {
            return Active && Vector3.Distance(source.position, target.position) <= lockDistance;
        }
    }
	#endregion
	
	#region Events
	/// <summary>
    /// Awake is called before start
    /// </summary>
	private void Awake()
	{
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        ready = false;
        Active = false;

        // Assigning static variables
        if (lockFalseSprite == null)
        {
            lockFalseSprite = Resources.Load<Sprite>("Sprites/CursorLockFalse");
        }
        if (lockTrueSprite == null)
        {
            lockTrueSprite = Resources.Load<Sprite>("Sprites/CursorLockTrue");
        }
        if (inactiveColor == Color.clear)
        {
            inactiveColor = new Color(0f, 1f, 0f, 0.4f);
        }
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
            Vector2 canvasPos = Utils.FrontWorldtoViewportPoint(target.position);
            _rectTransform.SetAnchors(canvasPos - Vector2.one * 0.05f, canvasPos + Vector2.one * 0.05f);

            // Check if active
            if (!Active && _image.color != inactiveColor)
            {
                _image.sprite = lockFalseSprite;
                _image.color = inactiveColor;
            }
            else if (Active && Locked && _image.color != Color.red)
            {
                _image.sprite = lockTrueSprite;
                _image.color = Color.red;
            }
            else if (Active && !Locked && _image.color != Color.green)
            {
                _image.sprite = lockFalseSprite;
                _image.color = Color.green;
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
    /// <param name="_source">The source to base distances on</param>
    /// /// <param name="_lockDistance">The maximum distance to lock on at</param>
	public void Setup(Transform _target, Transform _source, float _lockDistance)
    {
        target = _target;
        source = _source;
        lockDistance = _lockDistance;
        ready = true;
    }
    #endregion

    #region Coroutines

    #endregion
}
