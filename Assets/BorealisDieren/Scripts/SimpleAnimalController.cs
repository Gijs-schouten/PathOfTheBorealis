using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimalController : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] GameObject animalObject;
    [SerializeField] private float speed, wpRadius, damping;

    private int destPoint = 1;
    private bool isBusy = false;

    void Start()
    {
        ResetAnimal();
    }
    private void Update()
    {
        if (isBusy == false)
        {
            if (Vector3.Distance(waypoints[destPoint].position, animalObject.transform.position) < wpRadius)
            {
                destPoint++;
                if (destPoint >= waypoints.Length)
                {
                    destPoint = 0;
                    isBusy = true;
                    StartCoroutine(ResetAnimal(2f));
                }

                if (destPoint == (waypoints.Length /2))
                {
                    isBusy = true;
                    StartCoroutine(ResetAnimal(2f));
                }
            }

            LookAtTarget();
            animalObject.transform.position = Vector3.MoveTowards(animalObject.transform.position, waypoints[destPoint].position, Random.Range(speed / 10, speed) * Time.deltaTime);
        }
    }
    private void ResetAnimal()
    {
        animalObject.transform.position = waypoints[0].transform.position;
    }

    private void LookAtTarget()
    {
        var lookPos = waypoints[destPoint].position - animalObject.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        animalObject.transform.localRotation = Quaternion.Slerp(animalObject.transform.localRotation, rotation, Time.deltaTime * damping);
    }

    private IEnumerator ResetAnimal(float time)
    {
        yield return new WaitForSeconds(time);
        isBusy = false;
    }
}
