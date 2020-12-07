using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JB_Portal : MonoBehaviour, IPointerDownHandler // required interface when using the OnPointerDown method.

{
    // Event used to send teleport player to portal location
    public delegate void ClickingPortal(Vector3 portalLocation);
    public static event ClickingPortal SendPortalLocation;

    public enum Portal { One, Two};

    public Portal portalType;
    public float speed;

    public GameObject portalObj;

    private Rigidbody rigidBody;

    private GameObject obj;

    private Vector3 m_portalPos;

    public bool isMoving = true;

    private Vector3 startPos;

    public Vector3 portalPos { get { return m_portalPos; } }

    // Start is called before the first frame update
    void Start()
    {
        if (portalType == Portal.One)
        {
            obj = Instantiate(portalObj, transform.position, transform.rotation);
            obj.GetComponent<JB_Portal>().portalObj = this.gameObject;
        }
            


        if (portalType == Portal.Two)
            rigidBody = transform.GetComponent<Rigidbody>();

        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_portalPos = transform.position;

        if (isMoving && portalType == Portal.Two)
        {
            // travel forward
            // calculate distance traveled
            // stop moving forward after 8 meters or collides
            rigidBody.velocity = (transform.forward * speed);
        }

        float distance = Vector3.Distance(startPos, transform.position);

        if(distance >= 8f)
        {
            isMoving = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "Enemy")
        {
            isMoving = false;
        }
    }

    private void OnMouseUpAsButton()
    {
       
    }


    public void ClickedThisPortal()
    {
        if (portalType == Portal.One)
        {
            if (!obj.GetComponent<JB_Portal>().isMoving)
            {
                // send event signal to give my other script portal location
                SendPortalLocation(obj.GetComponent<JB_Portal>().portalPos);
            }
        }

        Debug.Log(portalType + " clicked!");
    }
    private void OnMouseDown()
    {
       
    }

    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked. pointer click");
    }

}
