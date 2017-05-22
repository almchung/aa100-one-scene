using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class DataInputModule : MonoBehaviour {
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

    // feed input into ML-module
    public MLModel mlModel;
    public float[,] inputArray;
    private int currentframe = 0;

    private int tau;

    void Start () {
        // linear scale for better velocity calculation
        linear_scale = 100;

        // bind MLModel
        mlModel = GameObject.Find("ML_module").GetComponent<MLModel>();
        tau = mlModel.tau;

        // populate input array with zeros
        ResetInputArray();

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
            UpdateInputMatrix(Time.frameCount, currentPosition, currentAngle, velocity, angularVel);
            WriteVectorsToCSV(currentTime.ToString("hh:mm:ss.fff tt"), currentPosition, currentAngle, velocity, angularVel);
        }
    }

    // INPUT for ML-MODULE
    public void UpdateInputMatrix(int frame, Vector3 pos, Vector3 ang, Vector3 vel, Vector3 avel)
    {
        int ind = frame % tau;
        //Debug.Log("frame : " + frame + " tau :" + tau + " ind:"+ind);
        inputArray[ind, 0] = pos.x;
        inputArray[ind, 1] = pos.y;
        inputArray[ind, 2] = pos.z;
        inputArray[ind, 3] = ang.x;
        inputArray[ind, 4] = ang.y;
        inputArray[ind, 5] = ang.z;
        inputArray[ind, 6] = vel.x;
        inputArray[ind, 7] = vel.y;
        inputArray[ind, 8] = vel.z;
        inputArray[ind, 9] = avel.x;
        inputArray[ind, 10] = avel.y;
        inputArray[ind, 11] = avel.z;
    }

    public void ResetInputArray()
    {
        inputArray = new float[tau, 12];
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
        //Debug.Log(string.Join(delimiter, output));
        sb.AppendLine(string.Join(delimiter, output));

        File.AppendAllText(filePath, sb.ToString());
    }

    public string StringifyVector(Vector3 vec)
    {
        return vec.x + ", " + vec.y + ", " + vec.z;
    }
}
