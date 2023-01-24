using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoTest : MonoBehaviour
{
    private SerialPort stream;
    private bool pressedUp;
    private bool pressedDown;

    [SerializeField] GameObject up, down;

    private void Awake()
    {
        LookForArduino();
    }

    private void Start()
    {
        StartCoroutine(ReadDataFromSerialPort());
    }

    private void Update()
    {
        CustomMove();
    }

    private void LookForArduino()
    {
        Time.timeScale = 0;
        for (int i = 0; i < 12; i++)
        {

            try { stream = new SerialPort($"COM{i}", 9600); stream.Open(); i = 14; }
            catch { }
        }
        if (stream.IsOpen) Debug.Log("Arduino found!");
        Time.timeScale = 1;
    }

    //Getting the inputs
    private IEnumerator ReadDataFromSerialPort()
    {
        while (true)
        {
            try
            {
                if (stream.BytesToRead > 0)
                {
                    string tmpA = stream.ReadLine();
                    string[] tmpB = tmpA.Split(',');
                    if (tmpB[0] == "D7")
                    {
                        pressedDown = Convert.ToBoolean(int.Parse(tmpB[1]));
                    }
                    if (tmpB[0] == "D8")
                    {
                        pressedUp = Convert.ToBoolean(int.Parse(tmpB[1]));
                    }
                }
            }
            catch
            {
                LookForArduino();
            }
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    private void CustomMove()
    {
        if (pressedDown && !pressedUp)
        {
            pressedDown = false;
            down.SetActive(true);
            up.SetActive(false);
            Debug.Log("Pressed Down");
        }
        if (pressedUp && !pressedDown)
        {
            pressedUp = false;
            up.SetActive(true);
            down.SetActive(false);
            Debug.Log("Pressed Up");
        }
    }
}
