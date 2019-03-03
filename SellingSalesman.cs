using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAgents;

public class SellingSalesman : Agent {

    public Animator salesmananimator;
    public List<GameObject> customers;
    GameObject stefani;
    ShoppingStefani stefani_script;
    GameObject remy;
    ShoppingRemy remy_script;
    GameObject shae;
    ShoppingShae shae_script;
    GameObject malcom;
    ShoppingMalcom malcom_script;
    GameObject jasper;
    ShoppingJasper jasper_script;
    GameObject regina;
    ShoppingRegina regina_script;
    GameObject liam;
    ShoppingLiam liam_script;
    public int reply;
    public int shopOccuppied;
    public int shoppingStarted;
    float inputY;
    public GameObject floor;
    Renderer floorRenderer;
    Color floorColor;
    bool changed_remy;
    bool changed_stefani;
    bool changed_shae;
    bool changed_jasper;
    bool changed_liam;
    bool changed_malcom;
    bool changed_regina;
    public bool securityreached;
    public Vector3 liamposition;
    public Vector3 remyposition;
    public bool startFight;

    SalesManAcademy academy;
    public bool useVectorObs;

    void Awake()
    {
        academy = FindObjectOfType<SalesManAcademy>(); //cache the academy
    }

    public override void CollectObservations()
    {
        if (useVectorObs)
        {
            AddVectorObs(reply);
            AddVectorObs(shopOccuppied);
            AddVectorObs(remy_script.ask);
            AddVectorObs(stefani_script.ask);
            AddVectorObs(shae_script.ask);
            AddVectorObs(malcom_script.ask);
            AddVectorObs(jasper_script.ask);
            AddVectorObs(regina_script.ask);
            AddVectorObs(liam_script.ask);
        }
    }

    /// <summary>
    /// Called every step of the engine. Here the agent takes an action.
    /// </summary>
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Move the agent using the action.
        Debug.Log("Float out:" + vectorAction[0]);
        reply = (int)(vectorAction[0]*10);
    }

    // Use this for initialization
    public override void InitializeAgent()
    {
        base.InitializeAgent();
        securityreached = false;
        startFight = false;
        useVectorObs = true;
        changed_remy = false;
        changed_stefani = false;
        shopOccuppied = 0;
        shoppingStarted = 0;
        // Get the animator
        salesmananimator = this.gameObject.GetComponent<Animator>();
        // Get the list of customer objects in the scene
        foreach (Transform child in transform)
        {
            customers.Add(child.gameObject);
        }
        // Get the floor renderer
        floorRenderer = floor.GetComponent<Renderer>();
        floorColor = floorRenderer.material.color;
        // Get the object handles for each customer.
        stefani = customers.Find((obj) => obj.name == "Stefani");
        stefani_script = stefani.GetComponent<ShoppingStefani>();
        remy = customers.Find((obj) => obj.name == "Remy");
        remy_script = remy.GetComponent<ShoppingRemy>();
        shae = customers.Find((obj) => obj.name == "Shae");
        shae_script = shae.GetComponent<ShoppingShae>();
        malcom = customers.Find((obj) => obj.name == "Malcom");
        malcom_script = malcom.GetComponent<ShoppingMalcom>();
        jasper = customers.Find((obj) => obj.name == "Jasper");
        jasper_script = jasper.GetComponent<ShoppingJasper>();
        regina = customers.Find((obj) => obj.name == "Regina");
        regina_script = regina.GetComponent<ShoppingRegina>();
        liam = customers.Find((obj) => obj.name == "Liam");
        liam_script = liam.GetComponent<ShoppingLiam>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        // Update liam position for use in child object scripts.
        liamposition = liam.transform.position;
        // Update remy position for use in child object scripts.
        remyposition = remy.transform.position;
        // Change the animator to talking when the customer asks question.
        if ((remy_script.ask != 0) || (stefani_script.ask != 0) || (shae_script.ask != 0) || (jasper_script.ask != 0) || (liam_script.ask != 0) || (malcom_script.ask != 0) || (regina_script.ask != 0))
        {
            // talking animation
            inputY = -1;
            // Penalty given each step to encourage agent to finish task quickly.
            AddReward(-1f / agentParameters.maxStep);
        }
        else
        {
            // Idle animation
            inputY = 0;
        }
        // Set the animation selection on the animator.
        salesmananimator.SetFloat("inputY", inputY);
        // Initiate agent communication to customers
        CommunicateToCustomer();
        // Quit unity application when "Esc" is pressed.
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        // Restart unity simulation when "r" is pressed.
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Custom function for salesman communication.
    void CommunicateToCustomer()
    {
        if (remy_script.ask != 0)
        {
            //Listen and reply to Remy
            //reply = Random.Range(1,1000);
            Debug.Log("Enquiry from Remy" + remy_script.ask);
            Debug.Log("Reply to Remy" +reply);
            changed_remy = false;
        }
        if (stefani_script.ask != 0)
        {
            //Listen and reply to Stefani
            //reply = Random.Range(1, 1000);
            Debug.Log("Enquiry from Stefani" + stefani_script.ask);
            Debug.Log("Reply to Stefani" + reply);
            changed_stefani = false;
        }
        if (shae_script.ask != 0)
        {
            //Listen and reply to Shae
            //reply = Random.Range(1, 1000);
            Debug.Log("Enquiry from Shae" + shae_script.ask);
            Debug.Log("Reply to Shae" + reply);
            changed_shae = false;
        }
        if (jasper_script.ask != 0)
        {
            //Listen and reply to Jasper
            //reply = Random.Range(1, 1000);
            Debug.Log("Enquiry from Jasper" + jasper_script.ask);
            Debug.Log("Reply to Jasper" + reply);
            changed_jasper = false;
        }
        if (liam_script.ask != 0)
        {
            //Listen and reply to Liam
            //reply = Random.Range(1, 1000);
            Debug.Log("Enquiry from Liam" + liam_script.ask);
            Debug.Log("Reply to Liam" + reply);
            changed_liam = false;
        }
        if (malcom_script.ask != 0)
        {
            //Listen and reply to Malcom
            //reply = Random.Range(1, 1000);
            Debug.Log("Enquiry from Malcom" + malcom_script.ask);
            Debug.Log("Reply to Malcom" + reply);
            changed_malcom = false;
        }
        if (regina_script.ask != 0)
        {
            //Listen and reply to Regina
            //reply = Random.Range(1, 1000);
            Debug.Log("Enquiry from Regina" + regina_script.ask);
            Debug.Log("Reply to Regina" + reply);
            changed_regina = false;
        }

        // Change the floor color when the sales is done. Remy.
        if (remy_script.salesDone && !changed_remy)
        {
            if (remy_script.bought)
            {
                // Customer bought the product
                StartCoroutine(SalesDoneColor(remy_script.character));
                // We use a reward of 5.
                AddReward(5f);
            }
            else
            {
                // Customer didnt buy the product.
                StartCoroutine(SalesCancelledColor(remy_script.character));
                // We use a penalty of -3.
                AddReward(-3f);
            }
        }
        // Change the floor color when the sales is done. Stefani.
        if (stefani_script.salesDone && !changed_stefani)
        {
            if (stefani_script.bought)
            {
                // Customer bought the product
                StartCoroutine(SalesDoneColor(stefani_script.character));
                // We use a reward of 5.
                AddReward(5f);
            }
            else
            {
                // Customer didnt buy the product.
                StartCoroutine(SalesCancelledColor(stefani_script.character));
                // We use a penalty of -3.
                AddReward(-3f);
            }
        }
        // Change the floor color when the sales is done. Shae.
        if (shae_script.salesDone && !changed_shae)
        {
            if (shae_script.bought)
            {
                // Customer bought the product
                StartCoroutine(SalesDoneColor(shae_script.character));
                // We use a reward of 5.
                AddReward(5f);
            }
            else
            {
                // Customer didnt buy the product.
                StartCoroutine(SalesCancelledColor(shae_script.character));
                // We use a penalty of -3.
                AddReward(-3f);
            }
        }
        // Change the floor color when the sales is done. Jasper.
        if (jasper_script.salesDone && !changed_jasper)
        {
            if (jasper_script.bought)
            {
                // Customer bought the product
                StartCoroutine(SalesDoneColor(jasper_script.character));
                // We use a reward of 5.
                AddReward(5f);
            }
            else
            {
                // Customer didnt buy the product.
                StartCoroutine(SalesCancelledColor(jasper_script.character));
                // We use a penalty of -3.
                AddReward(-3f);
            }
        }
        // Change the floor color when the sales is done. Liam.
        if (liam_script.salesDone && !changed_liam)
        {
            if (liam_script.bought)
            {
                // Customer bought the product
                StartCoroutine(SalesDoneColor(liam_script.character));
                // We use a reward of 5.
                AddReward(5f);
            }
            else
            {
                // Customer didnt buy the product.
                StartCoroutine(SalesCancelledColor(liam_script.character));
                // We use a penalty of -3.
                AddReward(-3f);
            }
        }
        // Change the floor color when the sales is done. Liam.
        if (malcom_script.salesDone && !changed_malcom)
        {
            if (malcom_script.bought)
            {
                // Customer bought the product
                StartCoroutine(SalesDoneColor(malcom_script.character));
                // We use a reward of 5.
                AddReward(5f);
            }
            else
            {
                // Customer didnt buy the product.
                StartCoroutine(SalesCancelledColor(malcom_script.character));
                // We use a penalty of -3.
                AddReward(-3f);
            }
        }
        // Change the floor color when the sales is done. Liam.
        if (regina_script.salesDone && !changed_regina)
        {
            if (regina_script.bought)
            {
                // Customer bought the product
                StartCoroutine(SalesDoneColor(regina_script.character));
                // We use a reward of 5.
                AddReward(5f);
            }
            else
            {
                // Customer didnt buy the product.
                StartCoroutine(SalesCancelledColor(regina_script.character));
                // We use a penalty of -3.
                AddReward(-3f);
            }
        }
    }

    // Change the floor color when the sales is successful.
    IEnumerator SalesDoneColor(string character)
    {
        floorRenderer.material.color = Color.green;
        Debug.Log("SalesDone");
        yield return new WaitForSeconds(0.5f); // Wait for 0.5sec
        floorRenderer.material.color = floorColor;
        if (character == "remy")
        {
            changed_remy = true;
        }
        else if (character == "stefani")
        {
            changed_stefani = true;
        }
        else if (character == "shae")
        {
            changed_shae = true;
        }
        else if (character == "jasper")
        {
            changed_jasper = true;
        }
        else if (character == "liam")
        {
            changed_liam = true;
        }
        else if (character == "malcom")
        {
            changed_malcom = true;
        }
        else if (character == "regina")
        {
            changed_regina = true;
        }
    }

    // Change the floor color when the sales is failed.
    IEnumerator SalesCancelledColor(string character)
    {
        floorRenderer.material.color = Color.red;
        Debug.Log("Sales Cancelled");
        yield return new WaitForSeconds(0.5f); // Wait for 0.5sec
        floorRenderer.material.color = floorColor;
        if (character == "remy")
        {
            changed_remy = true;
        }
        else if (character == "stefani")
        {
            changed_stefani = true;
        }
        else if (character == "shae")
        {
            changed_shae = true;
        }
        else if (character == "jasper")
        {
            changed_jasper = true;
        }
        else if (character == "liam")
        {
            changed_liam = true;
        }
        else if (character == "malcom")
        {
            changed_malcom = true;
        }
        else if (character == "regina")
        {
            changed_regina = true;
        }
    }
}
