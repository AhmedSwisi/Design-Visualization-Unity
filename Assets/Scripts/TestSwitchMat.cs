using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSwitchMat : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private List<Material> mats;
    int index;
    void Start()
    {
        index = 0;
    }
   
    public void ChangeMat()
    {
        index = (index + 1) % mats.Count;
        mesh.material = mats[index];
    }
   
}
