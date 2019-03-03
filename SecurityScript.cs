using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class SecurityScript : MonoBehaviour {

    public string character = "douglas";
    public GameObject salesman;
    SellingSalesman salesman_script;
    public NavMeshAgent agent_douglas;
    public Animator animator_douglas;
    public float InputX;
    Vector3 startposition;

    // Use this for initialization
    void Start () {
        startposition = transform.position;
        // Get the game object NavMeshAgent
        agent_douglas = this.gameObject.GetComponent<NavMeshAgent>();
        // Get the game object animator
        animator_douglas = this.gameObject.GetComponent<Animator>();
        // Get the salesman script data.
        salesman_script = salesman.GetComponent<SellingSalesman>();
        // Set Idle animation by default.
        InputX = 0.0f;
        // Set the animator selection input.
        animator_douglas.SetFloat("InputX", InputX);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if ((salesman_script.startFight) && (Vector3.Distance(transform.position, salesman_script.remyposition) > 2.0f))
        {
            agent_douglas.SetDestination(new Vector3(37.0f, 0.0f, -22.0f));
            // set walking animation
            InputX = 3.0f;
            // Set the animator selection input.
            animator_douglas.SetFloat("InputX", InputX);
        }
        else if (Vector3.Distance(transform.position, salesman_script.remyposition) <= 2.0f)
        {
            agent_douglas.isStopped = true;
            // Set talking animation
            InputX = 2.0f;
            // Set the animator selection input.
            animator_douglas.SetFloat("InputX", InputX);
            salesman_script.securityreached = true;
        }
        if (salesman_script.securityreached)
        {
            StartCoroutine(ArgueWaiting());
        }
    }

    IEnumerator ArgueWaiting()
    {
        // Wait for some time before start fighting.
        yield return new WaitForSeconds(1.0f);
        agent_douglas.isStopped = false;
        // Go back to shop.
        agent_douglas.SetDestination(new Vector3(5.21f, 0.0f, 8.6f));
        // Change animation to walking
        InputX = 3.0f;
        // Set the animator selection input.
        animator_douglas.SetFloat("InputX", InputX);
        if (Vector3.Distance(transform.position, new Vector3(5.21f, 0.0f, 8.6f)) <= 0.5f)
        {
            agent_douglas.isStopped = true;
            // Change animation to walking
            InputX = 0.0f;
            // Set the animator selection input.
            animator_douglas.SetFloat("InputX", InputX);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //salesman_script.securityreached = false;
        }
    }
}
