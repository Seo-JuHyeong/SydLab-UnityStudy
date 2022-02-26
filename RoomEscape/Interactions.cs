using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Interactions : MonoBehaviour
{
    public GameObject CabinetOpenMessage;
    public GameObject Cabinet;
    bool isCabinet;

    public GameObject Cabinet2OpenMessage;
    public GameObject Cabinet2;
    bool isCabinet2;
    bool isGetKey;

    public GameObject CorpseDrawerMessage;
    public GameObject CorpseDrawerMessage2;
    public GameObject CorpseDrawer;
    bool isCorpseDrawer;
    bool isCardKey;

    public GameObject FinalDoorMessage;
    public GameObject FinalDoorMessage2;
    public GameObject FinalDoor;
    bool isFinalDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1") == true)
        {
            if (isCabinet)
            {
                Cabinet.GetComponent<Animator>().SetBool("isOpen", true);
                CabinetOpenMessage.SetActive(true);
            }
            else if (isCabinet2)
            {
                Cabinet2.GetComponent<Animator>().SetBool("isOpen", true);
                Cabinet2OpenMessage.SetActive(true);
                isGetKey = true;
            }
            else if (isCorpseDrawer)
            {
                if (isGetKey)
                {
                    CorpseDrawer.GetComponent<Animator>().SetBool("isOpen", true);
                    CorpseDrawerMessage.SetActive(true);
                    isCardKey = true;
                }
                else
                {
                    CorpseDrawerMessage2.SetActive(true);
                }
            }else if (isFinalDoor)
            {
                if (isCardKey)
                {
                    FinalDoor.GetComponent<Animator>().SetBool("isOpen", true);
                    FinalDoorMessage.SetActive(true);
                }
                else
                {
                    FinalDoorMessage2.SetActive(true);
                }
            }
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire2") == true)
        {
            Cabinet.GetComponent<Animator>().SetBool("isOpen", false);
            CabinetOpenMessage.SetActive(false);

            Cabinet2.GetComponent<Animator>().SetBool("isOpen", false);
            Cabinet2OpenMessage.SetActive(false);

            CorpseDrawerMessage.SetActive(false);
            CorpseDrawerMessage2.SetActive(false);

            FinalDoorMessage.SetActive(false);
            FinalDoorMessage2.SetActive(false);
        }



    }

    public void CabinetOpen()
    {
        isCabinet = true;
    }

    public void CabinetClose()
    {
        isCabinet = false;
    }

    public void Cabinet2Open()
    {
        isCabinet2 = true;
    }

    public void Cabinet2Close()
    {
        isCabinet2 = false;
    }

    public void DrawerOpen()
    {
        isCorpseDrawer = true;
    }

    public void DrawerClose()
    {
        isCorpseDrawer = false;
    }

    public void FinalDoorOpen()
    {
        isFinalDoor = true;
    }

    public void FinalDoorClose()
    {
        isFinalDoor = false;
    }



}
