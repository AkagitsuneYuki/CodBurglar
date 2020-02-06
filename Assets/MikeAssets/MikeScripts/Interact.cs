using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    [SerializeField] private float rayLength;
    private RaycastHit rayHit;
    public LayerMask layerMask;

    [SerializeField] private Material hightLightMaterial;
    private Material savedMaterial;

    // THIS WILL STORE THE OBJECT THAT THE RAY HITS IN THE FRAME
    private GameObject curObj;

    [SerializeField] private int keysNeeded;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.blue);       //debug ray

        if(Physics.Raycast(transform.position, transform.forward, out rayHit, rayLength, layerMask))
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                ObjectInteraction(curObj);
            }

            if(curObj == null)
            {
                SetCurObjMat();
            }
            else if(curObj != null && curObj != rayHit.transform.gameObject)
            {
                NullifyCurObj();
                SetCurObjMat();
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

    private void SetCurObjMat()
    {
        curObj = rayHit.transform.gameObject;
        savedMaterial = curObj.GetComponent<Renderer>().material;
        curObj.GetComponent<Renderer>().material = hightLightMaterial;
    }

    private void NullifyCurObj()
    {
        curObj.GetComponent<Renderer>().material = savedMaterial;
        curObj = null;
    }

    void ObjectInteraction(GameObject objFromRaycast)
    {
        if(objFromRaycast.tag == "Door" && PlayerInventory.keyCount >= keysNeeded)
        {
            //print("im interacting with the door");
            //SceneManager.LoadScene("scene1");
        }
        /*
        if (objFromRaycast.tag == "Key")
        {
            //print("im interacting with the key");
            PlayerInventory.keyCount++;
            print(PlayerInventory.keyCount);
            GameObject.Destroy(objFromRaycast);
        }*/
    }


}
