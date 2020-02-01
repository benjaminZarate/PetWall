using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Accesories_Position : MonoBehaviour
{
    public Change_Object change;

    [SerializeField] List<Vector3> position = new List<Vector3>();
    [SerializeField] List<Vector3> rotation = new List<Vector3>();

    private void Update()
    {
        transform.position = position[change.index];
        transform.eulerAngles = rotation[change.index];
    }
}
