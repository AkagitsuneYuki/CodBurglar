using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    [SerializeField] private float rayLength;
    private RaycastHit rayHit;
    public LayerMask layerMask;

    // THIS WILL STORE THE OBJECT THAT THE RAY HITS IN THE FRAME
    private GameObject curObj;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.blue);       //debug ray

        if(Physics.Raycast(transform.position, transform.forward, out rayHit, rayLength, layerMask))
        {
            SetCurObj();
            if (Input.GetKeyDown(KeyCode.E))
            {
                ObjectInteraction(curObj);
            }
        }
        else
        //if i dont hit an object
        {
            if (curObj != null)
            {
                NullifyCurObj();
            }
        }
    }

    private void SetCurObj()
    {
        curObj = rayHit.transform.gameObject;
    }

    private void NullifyCurObj()
    {
        curObj = null;
    }

    void ObjectInteraction(GameObject obj)
    {
        if (obj.TryGetComponent<Lever>(out Lever theLever))
        {
            theLever.PullTheLever();
        }
    }


}
