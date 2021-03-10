using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//THE CLASS NAME CREATENTRIES SOUNDS/INDICATE LIKE A METHOD NAME. I SUGGEST ENTRIES
public class CreateEntries : MonoBehaviour
{
    [SerializeField]
    GameObject entryPrefab;

    //FIELD SHOULD BE PRIVATE, AND SET ON START, SINCE IT WILL ALWAYS BE THE PARENT FOR THE NEW IMAGES. 
    [SerializeField]
    Transform parent;

    [SerializeField]
    string[] urls;

    //FIELD SHOULD BE PRIVATE, AND SET ON START, SINCE IT WILL ALWAYS BE DOWNLOADER FOR THE NEW IMAGES.
    [SerializeField]
    ThumbnailDownloader downloader;
    


    /*THIS SHOULD BE CALLED CREATE ENTRIES TO MATCH THE UI TEXT
     *I DONT KNOW IF THE INTENTION IS TO CREATE NEW ENTRIES OR JUST TO UPDATE EXISTING ONES
     */
    public void RefreshEntries()
    {
        for(int i = 0; i < urls.Length; i++)
        {
            GameObject go = Instantiate(entryPrefab, parent);

            if(go.GetComponent<SetRawImage>() != null)
            {
                SetRawImage rawImage = go.GetComponent<SetRawImage>();
                rawImage.RefreshImage(urls[i], downloader);
            }
            else
            {
                ImageController image = go.GetComponent<ImageController>();
                image.RefreshImage(urls[i], downloader);
            }

            
        }
    }
}
