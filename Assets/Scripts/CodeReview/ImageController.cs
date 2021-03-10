using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    Image image;

    private void OnEnable()
    {
        image = GetComponent<Image>();
    }

    public void RefreshImage(string url, ThumbnailDownloader downloader)
    {

        downloader.LoadThumbnail("Thumbnails", image);
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            downloader.StartThumbnailDownload(url, "Thumbnails", image);
        }
    }
}
