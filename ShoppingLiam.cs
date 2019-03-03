using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShoppingLiam : MonoBehaviour
{

    public string character = "liam";
    public GameObject salesman;
    SellingSalesman salesman_script;
    public NavMeshAgent agent_liam;
    public Animator animator_liam;
    // Used in SalesMan script.
    public int ask;
    int question_liam;
    // Used in SalesMan script.
    public bool bought;
    bool shopOccuppied;
    bool shopFree;
    bool shopStarted;
    public float InputX;
    int shoppingQuest;
    // Used in SalesMan script.
    public bool salesDone;
    bool findfoe;

    // Use this for initialization
    void Start()
    {
        findfoe = false;
        shopOccuppied = false;
        shopFree = true;
        shopStarted = false;
        InputX = 0.0f;
        ask = 0;
        // Fill the shopping quest.
        shoppingQuest = 1000;
        bought = false;
        // Get the game object NavMeshAgent
        agent_liam = this.gameObject.GetComponent<NavMeshAgent>();
        // Get the game object animator
        animator_liam = this.gameObject.GetComponent<Animator>();
        // Get the salesman script data.
        salesman_script = salesman.GetComponent<SellingSalesman>();
        // Signaling end of customer buying interest when true.
        salesDone = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("f"))
        {
            findfoe = true;
        }
        if (findfoe)
        {
            foefind();
        }

        //Decrement the shopping quest threshold periodically.
        if (shoppingQuest > 0)
        {
            shoppingQuest -= 1;
        }
        Debug.Log("shoppingQuest Liam:" + shoppingQuest);
        Debug.Log("shopOccuppied:" + salesman_script.shopOccuppied);
        // Intiate the sales when shopping quest is maximum and customer is walking through the streets.
        if ((shoppingQuest == 0) && (InputX <= 0.0f) && (salesman_script.shoppingStarted < 2))
        {
            if (!shopStarted)
            {
                // Increment only once per agent.
                salesman_script.shoppingStarted += 1;
                shopStarted = true;
            }
            salesDone = false;
            bought = false;
            ask = 0;
            if (salesman_script.shopOccuppied < 2)
            {
                // Get a new question for new sale.
                question_liam = Random.Range(1, 10);
                // Make the agent moves to the sales man
                agent_liam.SetDestination(new Vector3(0.07f, 0.0f, 1.20f));
            }
            // Change the animation to Talking
            InputX = 1.0f;
        }
        // Check if destination is reached.
        if (((salesman_script.shopOccuppied == 0) || (!shopFree)) && ((Vector3.Distance(transform.position, new Vector3(0.07f, 0.0f, 1.0f)) <= 0.5f) && !salesDone))
        {
            shopFree = false;
            if (!shopOccuppied)
            {
                // Increment only once per agent.
                salesman_script.shopOccuppied += 1;
                shopOccuppied = true;
            }
            // Stop agent navigation
            agent_liam.isStopped = true;
            // Set the animator selection input.
            animator_liam.SetFloat("InputX", InputX);
            // Initiate agent communication to Salesman
            CommunicateToSalesman();
        }
        else if (shopFree && (salesman_script.shopOccuppied >= 1) && (Vector3.Distance(transform.position, new Vector3(0.07f, 0.0f, 1.0f)) <= 3.5f) && !salesDone)
        {
            if (!shopOccuppied)
            {
                // Increment only once per agent.
                salesman_script.shopOccuppied += 1;
                shopOccuppied = true;
            }
            // Stop agent navigation
            agent_liam.isStopped = true;
            // Change animation to idle.
            InputX = 2;
            // Set the animator selection input.
            animator_liam.SetFloat("InputX", InputX);
            if (salesman_script.shopOccuppied == 1)
            {
                // resume agent navigation
                agent_liam.isStopped = false;
                // Change animation to talking + walking.
                InputX = 0.5f;
                // Set the animator selection input.
                animator_liam.SetFloat("InputX", InputX);
            }
            if (Vector3.Distance(transform.position, new Vector3(0.07f, 0.0f, 1.0f)) <= 0.5f)
            {
                // Stop agent navigation
                agent_liam.isStopped = true;
                // Change animation to talking.
                InputX = 1.0f;
                // Set the animator selection input.
                animator_liam.SetFloat("InputX", InputX);
                // Initiate agent communication to Salesman
                CommunicateToSalesman();
            }
        }
    }

    // Custom function for customer communication.
    void CommunicateToSalesman()
    {
        // Ask a question to salesman
        ask = question_liam;
        // Check for salesman's relpy. If same as question, sales is salesDone.
        if (salesman_script.reply == ask)
        {
            SalesDone();
        }
        else
        {
            // Call the function to wait.
            StartCoroutine(Waiting());
        }
    }

    // Custom function for sales done state
    void SalesDone()
    {
        bought = true;
        salesDone = true;
        ask = 0;
        // Resume agent navigation
        agent_liam.isStopped = false;
        // Make the agent move to the normal walking road
        agent_liam.SetDestination(new Vector3(37.0f, 0.0f, -22.0f));
        // Change the animation to Walking.
        InputX = 0;
        // Set the animator selection input.
        animator_liam.SetFloat("InputX", InputX);
        if (shoppingQuest == 0)
        {
            //Decrement only once.
            salesman_script.shopOccuppied -= 1;
            salesman_script.shoppingStarted -= 1;
        }
        shopOccuppied = false;
        // Restore shopping quest.
        shoppingQuest = 1000;
        shopFree = true;
        shopStarted = false;
    }

    // Custom function for sales cancelled state
    void SalesCancelled()
    {
        salesDone = true;
        ask = 0;
        // Resume agent navigation
        agent_liam.isStopped = false;
        // Make the agent move to the normal walking road
        agent_liam.SetDestination(new Vector3(37.0f, 0.0f, -22.0f));
        // Change the animation to Walking.
        InputX = 0;
        // Set the animator selection input.
        animator_liam.SetFloat("InputX", InputX);
        if (shoppingQuest == 0)
        {
            //Decrement only once.
            salesman_script.shopOccuppied -= 1;
            salesman_script.shoppingStarted -= 1;
        }
        shopOccuppied = false;
        // Restore shopping quest.
        shoppingQuest = 1000;
        shopFree = true;
        shopStarted = false;
    }

    // Custom function for implementing customer wait time.
    IEnumerator Waiting()
    {
        // Wait for some time to get the response from salesman.
        yield return new WaitForSeconds(10.0f);
        SalesCancelled();
    }
    IEnumerator ArgueWaiting()
    {
        // Wait for some time before start fighting.
        yield return new WaitForSeconds(5.0f);
        if (salesman_script.securityreached)
        {
            agent_liam.SetDestination(new Vector3(37.0f, 0.0f, -22.0f));
            agent_liam.isStopped = false;
            InputX = 0.0f;
            // Set the animator selection input.
            animator_liam.SetFloat("InputX", InputX);
            salesman_script.startFight = false;
        }
        else
        {
            // Change animation to fist fight.
            InputX = 4.0f;
            // Set the animator selection input.
            animator_liam.SetFloat("InputX", InputX);
            salesman_script.startFight = true;
        }
    }

    void foefind()
    {
        if ((Vector3.Distance(transform.position, salesman_script.remyposition) <= 5.0f) && !shopStarted)
        {
            // Change animation to stand argue.
            agent_liam.isStopped = true;
            InputX = 3.0f;
            // Set the animator selection input.
            animator_liam.SetFloat("InputX", InputX);
            // Keep arguing for sometime.
            StartCoroutine(ArgueWaiting());

            // Update fighting status to SecurityGuard.
            // Change animation to argue with Security Guard.
            // Resume normal walking after some time.
        }
    }
}
