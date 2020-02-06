using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters;
using VoxelBusters.NativePlugins;

public class VoxelBusterManager : MonoBehaviour
{
    [HideInInspector]public Texture2D texture;

    public static VoxelBusterManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ShareSheet() {
        ShareSheet _shareSheet = new ShareSheet();

        _shareSheet.Text = "#PetWall #20'20Studio";
        _shareSheet.AttachImage(texture);
        NPBinding.Sharing.ShowView(_shareSheet, FinishSharing);
    }

    private void FinishSharing(eShareResult _result) {
        Debug.Log(_result);
    }
}
