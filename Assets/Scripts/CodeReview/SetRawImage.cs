using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//CLASS NAMING SHOULD REPRESENT A DESCRIPTION OF OBJECT. I SUGGEST IMAGECONTROLLER
public class SetRawImage : MonoBehaviour
{
  

    public void RefreshImage(string url, ThumbnailDownloader downloader)
    {
        //CHANGE VARIABLE SCOPE TO CLASS, NO NEED TO SEARCH FOR THE REFERENCE ON EACH REFRESH
        RawImage rawImage = GetComponent<RawImage>();

        //WHEN REFRESHING IMAGE, CHECK IF THE SAME IMAGE WAS ALREADY DOWNLOADED
        downloader.LoadThumbnail("Thumbnails", rawImage);
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            downloader.StartThumbnailDownload(url, "Thumbnails", rawImage);
        }
    }


}
