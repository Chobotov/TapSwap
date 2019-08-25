using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPipes : MonoBehaviour
{
    [SerializeField] private GameObject RedPipe, GreenPipe, BluePipe;
    [SerializeField] private float RedX = -0.9349999f, GreenX = 0.85f, BlueX = 0f;
    private GameObject Pipe;
    private bool IsDown = true, Click = false;
    private Vector3 Cor;
    private void PipeUpDown()
    {
        if (IsDown == true)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform.gameObject == RedPipe || hit.transform.gameObject == BluePipe || hit.transform.gameObject == GreenPipe)
            {
                IsDown = false;
                Pipe = hit.transform.gameObject;
                Cor = Pipe.transform.position;
                Pipe.transform.position += new Vector3(0f, 0.2f, 0f);
            }
        }
        else if (IsDown == false)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform.gameObject == RedPipe || hit.transform.gameObject == BluePipe || hit.transform.gameObject == GreenPipe)
            {
                GameObject NextPipe = hit.transform.gameObject;
                Vector3 NextCor = NextPipe.transform.position;
                if (Pipe != NextPipe)
                {
                    Pipe.transform.position = NextCor;
                    NextPipe.transform.position = Cor;
                    IsDown = true;
                }
                if (Pipe == NextPipe && Click == true)
                {
                    Pipe.transform.position = Cor;
                    IsDown = true;
                }
            }
        }
    }
    public void choosepipe()
    {
        if (Input.GetMouseButtonUp(0) && Click == false)
        {
            Click = true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            try
            {
                PipeUpDown();
            }
            catch (Exception e) { }
        }
    }

    public void DefaultCor()
    {
        BluePipe.transform.localPosition = (new Vector3(BlueX, 0f, 0f));
        RedPipe.transform.localPosition = (new Vector3(RedX, 0f, 0f));
        GreenPipe.transform.localPosition = (new Vector3(GreenX, 0f, 0f));
    }
}
