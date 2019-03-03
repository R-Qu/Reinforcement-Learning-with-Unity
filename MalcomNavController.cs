using UnityEngine;
using UnityEngine.AI;

public class MalcomNavController : MonoBehaviour {

    public Camera cam;
    public NavMeshAgent agent;
    public GameObject SalesMan;
	
	// Update is called once per frame
	void Update () {
        Ray ray = cam.ScreenPointToRay(SalesMan.transform.position);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);
        agent.SetDestination(hit.point);
	}
}
