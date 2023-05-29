using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public Material liveMat;

    public Material[] newMats;
    public Material defaultMat;


    public void ChangeLiveMat(int newMatNum)
    {
        //Find the material in the array
        Material materialToUse = newMats[newMatNum];

        //Copy the properties to the live material
        liveMat.CopyPropertiesFromMaterial(materialToUse);
    }

    public void SetDefaultMat()
    {
        liveMat.CopyPropertiesFromMaterial(defaultMat);
    }
}
