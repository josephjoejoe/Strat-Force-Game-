using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TileGrid : MonoBehaviour
{
    List<List<GameObject>> grid = new List<List<GameObject>>();
    public int width = 10;
    public int hight = 10;
    public float tileSize = 2;
    public GameObject tilePrefab; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < hight; j++)
            {

            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
