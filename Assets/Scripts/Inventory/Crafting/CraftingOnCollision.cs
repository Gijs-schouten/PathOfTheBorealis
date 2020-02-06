using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingOnCollision : MonoBehaviour
{
    public GameObject hand1;
    public GameObject hand2;
    public int particleNumber;
    public int soundNumber;
   public float rotationIncrease;
    public float startTime = 0f;
    bool spawned = false;
    bool isCrafting = false;
    private bool backToHand = true;
    private bool mayCraft = false;
    private GameObject object1;
    private GameObject object2;
   // private Vector3 test;
    private MeshCollider collider1;
    private MeshCollider collider2;
    private Rigidbody rb1;
    private Rigidbody rb2;
    public GameObject part;
    public GameObject part2;
    private SoundManager sound;
    private Vector3 velocity = Vector3.zero;
    private Vector3 spawnPos;
    private Vector3 spawnPosParticle;
    private bool isHand1;
    bool hand1Arrived;
    bool hand2Arrived;
    void Start()
    {
       // part = GameObject.Find("Particles").GetComponent<ParticleManager>();
      //  sound = GameObject.Find("Sounds").GetComponent<SoundManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Craftable"))
        {
            
            object1 = gameObject;
            object2 = collision.gameObject;
            mayCraft = true;
            StartCoroutine(CraftingAnimation());
        }
    }

    IEnumerator CraftingAnimation()
    {
        float distanceHand1 = Vector3.Distance(transform.position, hand1.transform.position);
        float distanceHand2 = Vector3.Distance(transform.position, hand2.transform.position);
        float duration = 75;
       
        float rotationSpeed = 5f;
        
        float distanceHands;

        rb1 = object1.GetComponent<Rigidbody>();
        rb2 = object2.GetComponent<Rigidbody>();
        collider1 = object1.GetComponent<MeshCollider>();
        collider2 = object2.GetComponent<MeshCollider>();

        if (distanceHand1 < distanceHand2)
        {
            isHand1 = true;
        }
        else
        {
            isHand1 = false;
        }

        if (!isCrafting)
        {
            isCrafting = true;
            RecipeCrafting crafting = object2.transform.GetComponent<RecipeCrafting>();
            RecipeCrafting crafting2 = transform.GetComponent<RecipeCrafting>();
            GameObject obj = CraftingRecepe.instance.CheckRecepe(crafting.itemName, crafting2.itemName);
            if (obj != null)
            {
                spawnPos = (hand1.transform.position + hand2.transform.position) / 2;
                spawnPosParticle = new Vector3  (spawnPos.x, spawnPos.y -1.5f, spawnPos.z);
               

                part = (GameObject) Instantiate(part, spawnPos, Quaternion.identity);

                while (startTime < duration && mayCraft)
                {//Start craft animation

                    startTime += rotationIncrease;
                   
                    distanceHands = Vector3.Distance(hand1.transform.position, hand2.transform.position);
                    // test = (object1.transform.position + object2.transform.position) / 2;
                    //rotationIncrease = rotationSpeed / (distanceHands /2);
                    rotationIncrease = (rotationSpeed / (distanceHands));
            
                    
                    collider1.enabled = false;
                    collider2.enabled = false;
                    rb2.isKinematic = true;
                    rb1.isKinematic = true;
                    //Rotate and Lerp into other object
                    object1.transform.Rotate(startTime *-rotationIncrease, startTime * - rotationIncrease, startTime * - rotationIncrease);
                    object2.transform.Rotate(startTime * rotationIncrease, startTime *  rotationIncrease, startTime * rotationIncrease);
                    object1.transform.position = Vector3.Lerp(object1.transform.position, spawnPos, .1f);
                    object2.transform.position = Vector3.Lerp(object2.transform.position, spawnPos, .1f);
                 
                    yield return new WaitForSecondsRealtime(1 / 60);
                 /*   if (rotationIncrease <= 1)
                    {
                        mayCraft = false;
                        StartCoroutine(BackToHand(object1, object2));;
                    }*/
                    if(spawned == false && startTime > 70)
                    {
                        
                        part2 = (GameObject)Instantiate(part2, spawnPosParticle, Quaternion.identity);
                        spawned = true;
                    }

                }
                if (mayCraft)
                {//Instantiate Output
                 // sound.soundList[soundNumber].Play();
                   // part2 = (GameObject)Instantiate(part2, spawnPos, Quaternion.identity);


                    GameObject spawnObj = Instantiate(obj);
                    spawnObj.transform.position = spawnPos;
                    Destroy(part);
                    Destroy(part2 , 1.3f);
                   // Destroy(part2 , 1f);
                    Destroy(object1);
                    Destroy(object2);
                }
            }
        }
        yield return new WaitForEndOfFrame();
    }
   /* IEnumerator BackToHand(GameObject obj1, GameObject obj2)
    {//Cancel animation
        isCrafting = false;
        backToHand = true;
        while (backToHand)
        {//Check  Hand
            if (isHand1)
            {
                obj1.transform.position = Vector3.Lerp(obj1.transform.position, hand1.transform.position, .1f);
                obj2.transform.position = Vector3.Lerp(obj2.transform.position, hand2.transform.position, .1f);
                if(Vector3.Distance(obj1.transform.position, hand1.transform.position )< 0.1f)
                {
                    hand1Arrived = true; 
                }
                if(Vector3.Distance(obj2.transform.position, hand2.transform.position) < 0.1f)
                {
                    hand2Arrived = true;
                }          
            }
 
            if (!isHand1)
            {
                obj2.transform.position = Vector3.Lerp(obj2.transform.position, hand1.transform.position, .1f);
                obj1.transform.position = Vector3.Lerp(obj1.transform.position, hand2.transform.position, .1f);

                if (Vector3.Distance(obj1.transform.position, hand2.transform.position) < 0.1f)
                {
                    hand1Arrived = true;
                }
                if (Vector3.Distance(obj2.transform.position, hand1.transform.position) < 0.1f)
                {
                    hand2Arrived = true;
                }
            }

            if(hand1Arrived && hand2Arrived)
            {//Reset Booleans
                backToHand = false;
                hand1Arrived = false;
                hand2Arrived = false;
                collider1.enabled = true;
                collider2.enabled = true;
                rb2.isKinematic = false;
                rb1.isKinematic = false;
            }
            yield return new WaitForEndOfFrame();*/
        }
    