using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMira : MonoBehaviour
{
    public static LookMira lookinstance;
    public float maxAimDistance = 5f; 
    public GameObject crosshair;
    public Camera maincamera;

    public float speed;
    public LayerMask layer;
    public bool translocar;

    [SerializeField] float maxDistance = 10f;
    [SerializeField] float grappleSpeed = 10f;
    [SerializeField] float grappleShootSpeed = 20f;
    public bool isgrapling;
    public GameObject graplePoint;
    public MoveGrapleHook mo;
    public Transform spawn;
    public bool tocou = true;
    public bool grapLiberado;
    private void Awake()
    {
        lookinstance = this;
    }
    private void Update()
    {
        if (grapLiberado)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                isgrapling = true;
                crosshair.gameObject.SetActive(true);
                Vector3 mousePosition = maincamera.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                Vector3 aimDirection = mousePosition - transform.position;
                aimDirection.z = 0f;
                aimDirection.Normalize();
                float aimDistance = Vector3.Distance(transform.position, mousePosition);
                if (aimDistance > maxAimDistance)
                {
                    aimDirection = (mousePosition - transform.position).normalized;
                    aimDirection.z = 0f;
                    mousePosition = transform.position + aimDirection * maxAimDistance;
                }
                if (crosshair != null)
                {
                    crosshair.transform.position = mousePosition;
                }
                transform.right = aimDirection;


            }
            else { crosshair.SetActive(false); }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                isgrapling = false;
                if (tocou)
                {
                    GameObject ob = Instantiate(graplePoint, spawn.transform.position, spawn.rotation);
                    ob.GetComponent<MoveGrapleHook>().bola = spawn;
                    mo = ob.GetComponent<MoveGrapleHook>();
                    tocou = false;

                }

            }
            if (Input.GetKeyDown(KeyCode.Space) && mo)
            {
                isgrapling = false;
                mo.volta = true;
                gameObject.GetComponentInParent<SpringJoint2D>().enabled = false;
            }

        }
        else { crosshair.gameObject.SetActive(false); }
    }
    public void cancela()
    {
        crosshair.gameObject.SetActive(false);
        if(mo != null)
        Destroy(mo.gameObject);
    }
}
