using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Transform model;
    
    private void Start()
    {
        GenerateScene();
    }

    void GenerateScene()
    {
        Data[] data =DataProvider.GetData();
        foreach (var images in data.Select(s => s.reconstruction.Images).ToList())
        {
            foreach (var image in images)
            {
                var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.SetParent(model);
                quad.transform.localPosition = new Vector3(image.Image.CameraDescription.Position[0], image.Image.CameraDescription.Position[1], image.Image.CameraDescription.Position[2]);
                quad.transform.rotation = new Quaternion(image.Image.CameraDescription.Orientation[0], image.Image.CameraDescription.Orientation[1], image.Image.CameraDescription.Orientation[2], image.Image.CameraDescription.Orientation[3]);
                quad.GetComponent<Renderer>().material.color = Color.red;

                quad.transform.localScale=new Vector3(0.1f, 0.1f, 0.1f);

            }


        }





    }
}
