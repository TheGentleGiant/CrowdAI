using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWaste : MonoBehaviour
{
    public GameObject obstacle;
    private GameObject[] agents;
    
    // Start is called before the first frame update
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(obstacle, hit.point, obstacle.transform.rotation);
                foreach (GameObject go in agents)
                {
                    go.GetComponent<AICrowdController>().DetectNewObstacle(hit.point);
                }
            }
        }   
    }
}
