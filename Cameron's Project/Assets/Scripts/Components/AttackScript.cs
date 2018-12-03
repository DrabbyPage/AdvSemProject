using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float attackTime = 1.5f;

    public bool canAttack = true;

    GameObject SoundMan;

    bool canCheckHitSound = true;
    float audioTimer = 1.4f;
    float maxTime = 1.4f;

    // Use this for initialization
    void Start()
    {
        SoundMan = GameObject.Find("SoundManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(canAttack)
        {
            CheckToAttack();
        }
    }

    void CheckToAttack()
    {
        GameObject human;
        if(gameObject.tag == "Zombie")
        {
            human = GetComponent<ZombieScript>().closestHuman;
        }
        else if(gameObject.tag == "Player")
        {
            human = GetComponent<CharacterScript>().target;
        }
        else
        {
            human = GetComponent<MoveScript>().target;
        }

        CheckHitSoundTimer();

        if (human != null)
        {
            float distX, distY;
            distX = human.transform.position.x - gameObject.transform.position.x;
            distY = human.transform.position.y - gameObject.transform.position.y;

            float dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

            if (dist < 1.5f)
            {
                human.GetComponent<BeingAttackedScript>().SetBeingAttacked(true);
                GetComponent<Animator>().SetBool("Attacking", true);

                if(GetComponent<PathHolderScript>())
                {
                    GetComponent<PathHolderScript>().ResetPath();
                }

                // set teh particle system activity to true
                if (transform.childCount > 0)
                {
                    if (transform.GetChild(0) != null)
                    {
                        transform.GetChild(0).gameObject.SetActive(true);
                    }
                }

                if (gameObject.tag == "Zombie")
                {
                    gameObject.GetComponent<ZombieScript>().SetMoveBool(false);
                }
                else if (gameObject.tag == "Player")
                {
                    gameObject.GetComponent<CharacterScript>().SetMoveBool(false);
                    gameObject.GetComponent<MoveScript>().SetMoveVec2(gameObject.transform.position);
                }
                
                if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                    if (SoundMan != null && canCheckHitSound)
                    {
                        SoundMan.GetComponent<SoundManagerScript>().PlayHitSound();
                        canCheckHitSound = false;
                    }
                }

            }
            else
            {
                // shut the particle system off
                gameObject.GetComponent<MoveScript>().SetMoveBool(true);


                if (transform.childCount >= 0)
                {
                    if (transform.GetChild(0) != null)
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                    }
                }

                gameObject.GetComponent<MoveScript>().SetMoveVec2(gameObject.transform.position);

                GetComponent<Animator>().SetBool("Attacking", false);
            }
        }
        else
        {
            GetComponent<MoveScript>().SetMoveBool(true);

            if (transform.childCount > 0)
            {
                if (transform.GetChild(0) != null)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            GetComponent<Animator>().SetBool("Attacking", false);
        }
    }

    void CheckHitSoundTimer()
    {
        if(canCheckHitSound == false)
        {
            if(audioTimer > 0)
            {
                audioTimer = audioTimer - Time.deltaTime;
            }
            else
            {
                audioTimer = maxTime;
                canCheckHitSound = true;
            }
        }
    }

    public void SetAttackAbility(bool newBool)
    {
        canAttack = newBool;
    }
}
