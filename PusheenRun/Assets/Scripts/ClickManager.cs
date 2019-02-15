using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Mouse Clicked");
		    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

		    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			
			if (hit.collider != null) {
                Debug.Log(hit.collider.gameObject.name);
                GameObject clickedObject = hit.collider.gameObject;
                System.String clickedObjectName = clickedObject.name;
                switch (clickedObjectName){
                	case "start-button":
		                SceneManager.LoadScene("Level1");
                		break;
                	case "musicOn":
				    	GameObject.Find("musicOn").SetActive(true);
				    	GameObject.Find("musicOff").SetActive(false);
                		break;
                	case "musicOff":
				    	GameObject.Find("musicOn").SetActive(false);
				    	GameObject.Find("musicOff").SetActive(true);
                		break;
                	case "question":
                		break;
                	default:
                		break;
                }
			}
		}
    }
}
