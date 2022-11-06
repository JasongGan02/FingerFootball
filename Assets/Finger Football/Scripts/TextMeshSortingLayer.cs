using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FingerFootball
{
    //It is attached on the "CurrentLevelNumber" which is a child object of a "LevelNumber" prefab (it is located in the "Prefab" folder.
    //This is used to change the sorting order of a Mesh Renderer
    public class TextMeshSortingLayer : MonoBehaviour
    {
        void Start()
        {
            var renderer = GetComponent<MeshRenderer>();
            renderer.sortingOrder = 6;
        }
    }
}