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
        GameObject human = GetComponent<MoveScript>().target;

        if (human != null)
        {
            float distX, distY;
            distX = human.transform.position.x - gameObject.transform.position.x;
            distY = human.transform.position.y - gameObject.transform.position.y;

            float dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

            if (dist < 1.5f)
            {
                human.GetComponent<BeingAttackedScript>().SetBeingAttacked(true);

                // set teh particle system activity to true
                transform.GetChild(0).gameObject.SetActive(true);

                if (gameObject.tag == "Zombie")
                {
                    gameObject.GetComponent<ZombieScript>().SetMoveBool(false);
                }
                else if (gameObject.tag == "Player")
                {
                    gameObject.GetComponent<CharacterScript>().SetMoveBool(false);
                    gameObject.GetComponent<MoveScript>().SetMoveVec2(gameObject.transform.position);
                }
                GetComponent<Animator>().SetBool("Attacking", true);

            }
            else
            {
                // shut the particle system off
                gameObject.GetComponent<MoveScript>().SetMoveBool(true);
                transform.GetChild(0).gameObject.SetActive(false);
                gameObject.GetComponent<MoveScript>().SetMoveVec2(gameObject.transform.position);

                GetComponent<Animator>().SetBool("Attacking", false);
            }
        }
        else
        {
            GetComponent<MoveScript>().SetMoveBool(true);
            transform.GetChild(0).gameObject.SetActive(false);

            GetComponent<Animator>().SetBool("Attacking", false);
        }
    }
}
