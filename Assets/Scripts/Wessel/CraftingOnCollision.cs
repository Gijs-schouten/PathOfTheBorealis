using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class CraftingOnCollision : MonoBehaviour
    {
        public GameObject hand1;
        public GameObject hand2;
        public int particleNumber;
        public int soundNumber;
        public float rotationIncrease;
        public float startTime = 0f;
        bool isCrafting = false;
        private bool mayCraft = false;
        private GameObject object1;
        private GameObject object2;
        private MeshCollider collider1;
        private MeshCollider collider2;
        private Rigidbody rb1;
        private Rigidbody rb2;
        public GameObject part;
        public GameObject part2;
        public GameObject part3;
        public GameObject Player;
        private Vector3 velocity = Vector3.zero;
        private Vector3 spawnPos;
        private Vector3 spawnPosParticle;
        private bool isHand1;

        void Start()
        {
             //hand1 = GetComponentInParent<Hand>();
             //hand2 = GetComponentInParent<Hand>();
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
                RecepeItemName crafting = object2.transform.GetComponent<RecepeItemName>();
                RecepeItemName crafting2 = transform.GetComponent<RecepeItemName>();
                GameObject obj = CraftingRecepe.instance.CheckRecepe(crafting.itemName, crafting2.itemName);
                if (obj != null)
                {
                    spawnPos = (hand1.transform.position + hand2.transform.position) / 2;
                    spawnPosParticle = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z);


                    part = (GameObject)Instantiate(part, spawnPos, Quaternion.identity);

                    while (startTime < duration && mayCraft)
                    {//Start craft animation

                        startTime += rotationIncrease;

                        distanceHands = Vector3.Distance(hand1.transform.position, hand2.transform.position);
                        rotationIncrease = (rotationSpeed / (distanceHands));
                        collider1.enabled = false;
                        collider2.enabled = false;
                        rb2.isKinematic = true;
                        rb1.isKinematic = true;

                        //Rotate and Lerp into other object
                        object1.transform.Rotate(startTime * -rotationIncrease, startTime * -rotationIncrease, startTime * -rotationIncrease);
                        object2.transform.Rotate(startTime * rotationIncrease, startTime * rotationIncrease, startTime * rotationIncrease);
                        object1.transform.position = Vector3.Lerp(object1.transform.position, spawnPos, .1f);
                        object2.transform.position = Vector3.Lerp(object2.transform.position, spawnPos, .1f);

                        yield return new WaitForSecondsRealtime(1 / 60);

                    }
                    if (mayCraft)
                    {//Instantiate Output
                        var heading = transform.position - Player.transform.position;
                        heading.Normalize();
                        spawnPosParticle = spawnPosParticle - heading;

                        part3 = (GameObject)Instantiate(part3, spawnPosParticle, Quaternion.identity);
                        part2 = (GameObject)Instantiate(part2, spawnPosParticle, Quaternion.identity);
                        GameObject spawnObj = Instantiate(obj);
                        spawnObj.transform.position = spawnPos;
                        GameObject objectToAttach = GameObject.Instantiate(obj);
                        objectToAttach.SetActive(true);
                       // hand1.AttachObject(objectToAttach, GrabTypes.Scripted);
                      //  hand1.TriggerHapticPulse(800);

                        Destroy(gameObject);                  
                        Destroy(part);
                        Destroy(part2, 1.3f);
                        Destroy(object1);
                        Destroy(object2);
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
       
    }
}
    