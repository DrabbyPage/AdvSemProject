using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneClickSelectionScript : MonoBehaviour
{
    GameObject GameMan;
    GameObject Player;
    GameObject target;

    GameObject crosshair;

    float lockOnRange = 1.0f;

    Vector2 newPoint;

    Vector4 redColor;
    Vector4 greenColor;
	// Use this for initialization
	void Start ()
    {
        GameMan = GameObject.Find("GameManager");
        Player = GameObject.Find("Player");

        if (crosshair == null)
        {
            crosshair = Instantiate(Resources.Load("Prefabs/crosshair")) as GameObject;
        }

        if(Player != null)
        {
            crosshair.transform.position = Player.transform.position;
            newPoint = Player.transform.position;
            Player.GetComponent<CharacterScript>().SetMovePoint(Player.transform.position);
        }

        redColor = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        greenColor = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update ()
    {
        CheckForMouseInput();
        CheckCrosshairDist();
	}

    void CheckForMouseInput()
    {
        if (Input.GetMouseButton(0) && !GameObject.Find("GameManager").GetComponent<GameManagerScript>().gamePaused)
        {
            GameObject SoundMan = GameObject.Find("SoundManager");
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z));
            newPoint = new Vector2(mouseWorld.x, mouseWorld.y);
            
            if(target != null)
            {
                target = null;
            }

            if (Player != null)
            {
                Player.GetComponent<CharacterScript>().SetNewTarget(null);
                Player.GetComponent<MoveScript>().SetMoveVec2(newPoint);

                if (!Player.GetComponent<HealthScript>().isDying)
                {
                    Player.GetComponent<CharacterScript>().SetMoveBool(true);
                }
            }

            if (SoundMan != false)
            {
                SoundMan.GetComponent<SoundManagerScript>().PlayerZombieMoveSound();
            }

            crosshair.SetActive(true);
            crosshair.GetComponent<SpriteRenderer>().color = greenColor;

            crosshair.transform.position = newPoint;

            //CheckZombieDist(mouseWorld);

        }
        if (Input.GetMouseButton(1))
        {
            GameObject SoundMan = GameObject.Find("SoundManager");

            if (Player != null)
            {
                Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z));


                if (Player != null)
                {
                    Player.GetComponent<CharacterScript>().SetNewTarget(null);
                    Player.GetComponent<MoveScript>().SetMoveVec2(newPoint);

                    if (!Player.GetComponent<HealthScript>().isDying)
                    {
                        Player.GetComponent<CharacterScript>().SetMoveBool(true);
                    }
                }

                if(SoundMan != false)
                {
                    SoundMan.GetComponent<SoundManagerScript>().PlayerZombieMoveSound();
                }

                CheckHumanDist(mouseWorld);
            }
        }

        if(target != null)
        {
            crosshair.transform.position = target.transform.position;
        }
    }

    void CheckHumanDist(Vector3 mouse)
    {
        target = GameMan.GetComponent<GameManagerScript>().CloseToHuman(mouse, lockOnRange);

        if (target != null)
        {
            Player.GetComponent<CharacterScript>().SetNewTarget(target);

            crosshair.SetActive(true);
            crosshair.GetComponent<SpriteRenderer>().color = redColor;
            crosshair.transform.position = target.transform.position;
        }
        else
        {
            Player.GetComponent<CharacterScript>().SetNewTarget(null);
        }
    }

    void CheckCrosshairDist()
    {
        if(Player != null)
        {
            float dist;
            float crossX = crosshair.transform.position.x;
            float crossY = crosshair.transform.position.y;
            float playerX = Player.transform.position.x;
            float playerY = Player.transform.position.y;

            dist = Mathf.Sqrt(Mathf.Pow(crossX- playerX, 2) + Mathf.Pow(crossY - playerY, 2));

            if(dist < lockOnRange)
            {
                crosshair.SetActive(false);
            }
        }
        else
        {
            crosshair.SetActive(false);
        }
    }

    public void SetPlayer(GameObject newPlayer)
    {
        Player = newPlayer;
    }
}
