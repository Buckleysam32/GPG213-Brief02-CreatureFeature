using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class ImageProcessingScript : MonoBehaviour
{
    public Texture2D imageToProcess;
    [SerializeField] private int width, depth; 
    public GameObject spherePrefab, emptyPrefab;
    [SerializeField] private bool useSpheres;
    private Coroutine generateCoroutine;
    [SerializeField] private float verticalTolerance;
    private void Start()
    {
        generateCoroutine = StartCoroutine(ReadImageRoutine());
    }
    private void OnDisable()
    {
        if (generateCoroutine != null) StopCoroutine(generateCoroutine); 
        // StopAllCoroutines();
    }
    private Vector4 ReadTexture(int i, int j)
    {
        Color32 pixel = imageToProcess.GetPixel(i, j);
        return new Vector4(pixel.r, pixel.g, pixel.b, pixel.a);
    }
    private IEnumerator ReadImageRoutine()
    {
        
        //If we have an image, use that image and generate game objects using the image information
        if(imageToProcess)
        {                
            float lastIOffset = 0f;

            for (var i = 0; i < imageToProcess.height; i++)
            {                    
                float lastJOffset = 0f;
                
                for (var j = 0; j < imageToProcess.width; j++)
                {
                    var pixelValue = ReadTexture(i, j);
                    float verticalOffset = pixelValue.w / 10f;
                    
                    if (i == 0)
                    {
                        lastIOffset = verticalOffset;
                    }
                    if (j == 0)
                    {
                        lastJOffset = verticalOffset;
                    }
                    
                    
                    
                    
                    
                    if (verticalOffset > lastJOffset + verticalTolerance && lastJOffset + verticalTolerance < pixelValue.w / 10f)
                    {
                        verticalOffset = lastJOffset + verticalTolerance;
                    }
                    else if (verticalOffset < lastJOffset - verticalTolerance && lastJOffset -verticalTolerance > 0f)
                    {
                        verticalOffset = lastJOffset - verticalTolerance;
                    }
                    /*if (verticalOffset > lastIOffset + verticalTolerance && lastIOffset + verticalTolerance < pixelValue.w / 10f)
                    {
                        verticalOffset = lastIOffset + verticalTolerance;
                    }
                    else if (verticalOffset < lastIOffset - verticalTolerance && lastIOffset -verticalTolerance > 0f)
                    {
                        verticalOffset = lastIOffset - verticalTolerance;
                    }*/
                    
                    var spawnPos = new Vector3(i, verticalOffset, j);
                    if (useSpheres)
                    {
                        var prefab = Instantiate(spherePrefab, spawnPos, Quaternion.identity, gameObject.transform);
                        prefab.GetComponent<MeshRenderer>().material.color = new Color32((byte)pixelValue.x, (byte)pixelValue.y,(byte)pixelValue.z, (byte)pixelValue.w);
                    }
                    else
                    {
                        var prefab = Instantiate(emptyPrefab, spawnPos, Quaternion.identity, gameObject.transform);
                    }
                    lastJOffset = verticalOffset; 
                    if (j % imageToProcess.width / 2f == 0)
                    {
                        yield return null;
                    }
                }
            }
        }
        /*//If we don't have an image, generate a random assortment of game objects, using spheres if choosing to
        else
        {
            for (var i = 0; i < depth; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    var randomHeight = Random.Range(0, 25);
                    var spawnPos = new Vector3(i, randomHeight, j);
                    if (useSpheres)
                    {
                        var prefab = Instantiate(spherePrefab, spawnPos, Quaternion.identity, gameObject.transform);
                        prefab.GetComponent<MeshRenderer>().material.color = new Color32((byte)Random.Range(0f, 255f),
                            (byte)Random.Range(0f, 255f), (byte)Random.Range(0f, 255f), (byte)255f);
                    }
                    else
                    {
                        var prefab = Instantiate(emptyPrefab, spawnPos, Quaternion.identity, gameObject.transform);
                    }

                    if (j % width == 0)
                    {
                        yield return null;
                    }
                }
            }
        }*/
        Debug.Log("All done!");
        generateCoroutine = null;
    }
}
