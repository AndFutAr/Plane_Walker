using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using UnityEngine;

public class Planer : MonoBehaviour
{
    [SerializeField] private Transform dotUp, dotDown, dotForward, dotBack, dotLeft, dotRight;
    private char plane = ' ', planeVector = ' ';
    private char dirPlane = ' ', dirVector = ' ';
    private char linePlane = ' ', lineVector = ' ';
    
    public char Plane => plane;
    public char PlaneVector => planeVector;
    public char DirPlane => dirPlane;
    public char DirVector => dirVector;
    public char LinePlane => linePlane;
    public char LineVector => lineVector;
    
    private void Update()
    {
        float planeX = dotUp.position.x - dotDown.position.x;
        float planeY = dotUp.position.y - dotDown.position.y;
        float planeZ = dotUp.position.z - dotDown.position.z;

        if (planeX > 0.15) { plane = 'x'; planeVector = '+'; }
        else if (planeX < -0.15) { plane = 'x'; planeVector = '-'; }
        if (planeY > 0.15) { plane = 'y'; planeVector = '+'; }
        else if (planeY < -0.15) { plane = 'y'; planeVector = '-'; }
        if (planeZ > 0.15) { plane = 'z'; planeVector = '+'; }
        else if (planeZ < -0.15) { plane = 'z'; planeVector = '-'; }
        
        
        float diffX = dotForward.position.x - dotBack.position.x;
        float diffY = dotForward.position.y - dotBack.position.y;
        float diffZ = dotForward.position.z - dotBack.position.z;

        if (diffX > 0.15) { dirPlane = 'x'; dirVector = '+'; }
        else if (diffX < -0.15) { dirPlane = 'x'; dirVector = '-'; }
        if (diffY > 0.15) { dirPlane = 'y'; dirVector = '+';}
        else if (diffY < -0.15) { dirPlane = 'y'; dirVector = '-'; }
        if (diffZ > 0.15) { dirPlane = 'z'; dirVector = '+'; }
        else if (diffZ < -0.15) { dirPlane = 'z'; dirVector = '-'; }
        
        
        float lineX = dotLeft.position.x - dotRight.position.x;
        float lineY = dotLeft.position.y - dotRight.position.y;
        float lineZ = dotLeft.position.z - dotRight.position.z;
        
        if (lineX > 0.15) { linePlane = 'x'; lineVector = '+';}
        else if (lineX < -0.15) { linePlane = 'x'; lineVector = '-'; }
        if (lineY > 0.15) { linePlane = 'y'; lineVector = '+';}
        else if (lineY < -0.15) { linePlane = 'y'; lineVector = '-'; }
        if (lineZ > 0.15) { linePlane = 'z'; lineVector = '+'; }
        else if (lineZ < -0.15) { linePlane = 'z'; lineVector = '-'; }
    }
}