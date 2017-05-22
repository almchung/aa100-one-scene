﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class MeasuringStats : MonoBehaviour {
    public Vector3 prevAngle;
    public Vector3 prevPosition;
    public Vector3 currentPosition;
    public Vector3 currentAngle;
    public Vector3 velocity;
    public Vector3 angularVel;
    public float linear_scale;
    private DateTime currentTime;

    private string filePath;
    private string delimiter = ",";

    void Start () {
        // linear scale for better velocity calculation
        linear_scale = 100;

        // debug purpose: initialized CSV file
        currentTime = DateTime.Now;
        filePath = Directory.GetCurrentDirectory() + "\\data_"+ currentTime.ToString("yyyy-MM-dd-HH-mm-ss") + ".csv";
        Debug.Log("CSV file writing to : " + filePath);

        string[] output = new string[] { filePath, "linear scale factor", linear_scale.ToString()};
        WriteToCSV(output);

        output = new string[] { "time", "posX,posY,posZ", "angAlpha,angBeta,angGamma", "velX,velY,velZ", "aVelAlpha,aVelBeta,aVelGamma"};
        WriteToCSV(output);
    }

    // Update is called once per frame
    void Update()
    {
        MeasureVRHead();
    }

    private void MeasureVRHead()
    {
        currentPosition = transform.position * linear_scale;
        currentAngle = transform.rotation.eulerAngles;

        if (prevPosition != null)
        {
            velocity = currentPosition - prevPosition;
            prevPosition = currentPosition;

            angularVel = currentAngle - prevAngle;
            prevAngle = currentAngle;

            //Debug.Log("Pos: " + currentPosition + ", Angle: " + currentAngle + ", Linear Vel:" + velocity + ", Angular Vel:" + angularVel);
            currentTime = DateTime.Now;
            WriteVectorsToCSV(currentTime.ToString("hh:mm:ss.fff tt"), currentPosition, currentAngle, velocity, angularVel);
        }
    }

    // debug purpose: recording raw data
    private void WriteVectorsToCSV(String time, Vector3 pos, Vector3 ang, Vector3 vel, Vector3 avel)
    {
        string[] output = new string[] {time, StringifyVector(pos), StringifyVector(ang), StringifyVector(vel), StringifyVector(avel) };

        WriteToCSV(output);
    }

    private void WriteToCSV(string[] output)
    {
        StringBuilder sb = new StringBuilder();
        Debug.Log(string.Join(delimiter, output));
        sb.AppendLine(string.Join(delimiter, output));

        File.AppendAllText(filePath, sb.ToString());
    }

    public string StringifyVector(Vector3 vec)
    {
        return vec.x + ", " + vec.y + ", " + vec.z;
    }
}
