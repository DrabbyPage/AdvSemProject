using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public float attackTime = 1.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckToAttack();
    }

    void CheckToAttack()
    {
        GameObject human = GetComponent<CharacterMove>().target;

        if (human != null && human.tag != "Dead")
        {
            float distX, distY;
            distX = human.transform.position.x - gameObject.transform.position.x;
            distY = human.transform.position.y - gameObject.transform.position.y;

            float dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

            if (dist < 1.5f)
            {
                Debug.Log("player is attacking");
                human.GetComponent<RunAwayScript>().setBeingAttacked(true);
                GetComponent<CharacterMove>().SetMoveAbility(false);

                // do some particle physics maybe?
            }
            else
            {
                GetComponent<CharacterMove>().SetMoveAbility(true);
            }
        }
        else
        {
            GetComponent<CharacterMove>().SetMoveAbility(true);
        }
    }
}
