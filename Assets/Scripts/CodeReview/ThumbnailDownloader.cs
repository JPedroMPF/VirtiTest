using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class ThumbnailDownloader : MonoBehaviour
{
    /*I SUGGEST REARRANGE METHODS ORDER, SOMETHING LIKE:
     * NETWORK RELATED
     * CONVERSION RELATED
     * SYSTEM SAVE AND LOAD     
     */

    void SaveThumbnail(string subDirectory, byte[] bytes)
    {
        if (!System.IO.Directory.Exists(Application.persistentDataPath + "/Thumbnails/" + subDirectory))
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/Thumbnails/" + subDirectory);
        }

        System.IO.FileStream file = System.IO.File.Open(Application.persistentDataPath + "/Thumbnails/" + subDirectory +  "image.png", System.IO.FileMode.Create);
        System.IO.BinaryWriter binary = new System.IO.BinaryWriter(file);
        binary.Write(bytes);
        binary.Close();
        file.Close();
    }

    public Texture2D LoadImage(string subDirectory)
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/Thumbnails/" + subDirectory + "image.png"))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/Thumbnails/" + subDirectory + "image.png");
            Texture2D texture = new Texture2D(1, 1, TextureFormat.DXT5, false);
            texture.LoadImage(bytes);

            return texture;
            //DELETE UNWANTED COMMENTS
            //SetRawImageThumbnailObject(thumbnail, texture, thumbnailMinimumHeight, thumbnailMaxHeight);
        }

        return null;
    }

    public void OnDownloadComplete(Texture2D texture, string subDirectory, MaskableGraphic graphic)
    {
       
        if (graphic.GetType().Name == "RawImage")
            SetRawImageThumbnailObject(graphic as RawImage, texture);
        if (graphic.GetType().Name == "Image")
            SetImageThumbnailObject(graphic as Image, texture);

        SaveThumbnail(subDirectory, texture.EncodeToPNG());
    }

    public void StartThumbnailDownload(string thumbnailURI, string subDirectory, MaskableGraphic thumbnail)
    {
        IEnumerator downloadObj = UpdateThumbnail(thumbnailURI, thumbnail, subDirectory, OnDownloadComplete);
        StartCoroutine(downloadObj);
    }

    public void LoadThumbnail(string subDirectory, MaskableGraphic thumbnail)
    {
        Texture2D texture = LoadImage(subDirectory);
        if (texture != null)
        {
            if (thumbnail.GetType().Name == "RawImage")
                SetRawImageThumbnailObject(thumbnail as RawImage, texture);
            if (thumbnail.GetType().Name == "Image")
                SetImageThumbnailObject(thumbnail as Image, texture);
        }
    }    

    protected IEnumerator UpdateThumbnail(string thumbnailURI, MaskableGraphic graphic, string subDirectory, System.Action<Texture2D, string, MaskableGraphic> onDownloadComplete)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(thumbnailURI);

        yield return www.SendWebRequest();

        while (!www.isDone)
        {
            yield return 0;
        }

        if (www.error == null)
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

            if (onDownloadComplete != null)
            {
                onDownloadComplete(((DownloadHandlerTexture)www.downloadHandler).texture, subDirectory, graphic);
            }

            onDownloadComplete = null;

        }

        www.downloadHandler.Dispose();
        www.Dispose();
    }

    public void SetRawImageThumbnailObject(RawImage thumbnail, Texture2D texture)
    {

        thumbnail.color = Color.white;
        thumbnail.texture = texture;

    }

    public void SetImageThumbnailObject(Image thumbnail, Texture2D texture)
    {
        Sprite textureToSprite = Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),Vector2.zero);
        thumbnail.sprite = textureToSprite;

    }
}

