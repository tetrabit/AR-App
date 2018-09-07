using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;

public class MetadataParse : MonoBehaviour {
	
	private SimpleCloudHandler cloudHandler;
	Text estateTitle;
	Text estateBody;
	Image estatePicture;
	

	bool estateCard;
	
	// Use this for initialization
	public void ParseData (string metaData, GameObject imageTarget) 
	{
		//find cloned objects
		//estateTitle = cloudHandler.newImageTarget.transform.Find("Canvas/estateCard/title").GetComponent<Text>();
		//estateBody = cloudHandler.newImageTarget.transform.Find("Canvas/estateCard/body").GetComponent<Text>();
		//estatePicture = cloudHandler.newImageTarget.transform.Find("Canvas/picture").GetComponent<Image>();

		using (StringReader reader = new StringReader(metaData))
		{
			string line;
			while ((line = reader.ReadLine()) != null)
			{
                ExecuteLineCommand(line, imageTarget);
			}
		}
	}
	
	IEnumerator WaitToPlay(){
		yield return new WaitForSeconds(1f);
		//Play video here
	}
	
	void ExecuteLineCommand(string line, GameObject imageTarget){
		string[] splitMetadata = line.Split(' ');
		Debug.Log(line);
		if(splitMetadata[1].StartsWith("none")) return;
		
		switch (splitMetadata[0])
		{
          case "videoUrl":
            Debug.Log("Video URL Parsed" + splitMetadata[1]);
            LoadVideo(splitMetadata[1], imageTarget);
            break;
          case "3durl":
			Debug.Log("3dURL Parsed" + splitMetadata[1]);
            //Load3dAsset(splitMetadata[1]);
            break;
          case "phone":
                Debug.Log("Phone" + splitMetadata[1]);
                DisplayPhoneLink(splitMetadata[1]);
                break;
          case "website":
                Debug.Log("Website" + splitMetadata[1]);
                DisplayWebsiteLink(splitMetadata[1]);
                break;
          case "email":
                Debug.Log("email" + splitMetadata[1]);
                DisplayEmailLink(splitMetadata[1]);
                break;
          
          default:
            break;
		}
	}
	
	void LoadVideo(string url, GameObject imageTarget){
        StartCoroutine(imageTarget.GetComponent<PlayVideo>().NewVideo(url));
        //Load Video here
			//MPMPPlayback = cloudHandler.newImageTarget.transform.Find("MPMP.instance").GetComponent<MPMP>();
			//MPMPPlayback.Load(url);
		//StartCoroutine(WaitToPlay());
	}
	
	void Load3dAsset(string url){
		StartCoroutine(GetAssetBundle(url));
	}

    void DisplayPhoneLink(string link)
    {

    }

    void DisplayWebsiteLink(string link)
    {

    }

    void DisplayEmailLink(string link)
    {

    }
	
	void LoadImage(string url){
		StartCoroutine(GetImage(url));
	}
	
    IEnumerator GetAssetBundle(string url) {
        
		UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return www.Send();
 
        if(www.isNetworkError) {
            Debug.Log(www.error);
        }
        else {
			AssetBundle bundle = ((DownloadHandlerAssetBundle)www.downloadHandler).assetBundle;
			Instantiate(bundle.LoadAsset("Maze_1"),cloudHandler.newImageTarget.transform);
			
		}
		
		//not tested
		//byte[] bytes = www.bytes;
		//string filename = test;
		//File.WriteAllBytes(Application.persistentDataPath+"/" + filename, bytes);
		/*reference
		http://answers.unity3d.com/questions/19522/how-to-download-an-asset-bundle-to-local-hard-driv.html
		http://answers.unity3d.com/questions/591912/how-to-download-asset-bundles-from-website-to-appl.html
		*/
    }
	
    IEnumerator GetImage(string url) {
		WWW www = new WWW(url);
		yield return www;
		estatePicture.sprite =  Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }	
	// Update is called once per frame
	void Update () {
		
	}
}
