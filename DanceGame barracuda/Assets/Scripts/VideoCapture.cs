
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoCapture : MonoBehaviour
{
	public RawImage rawImage;
	public float scale = 1;
	public bool useWebCam = true;
	public int webCamIndex = 0;

	public RenderTexture renderTexture { get; private set; }

	private WebCamTexture webCamTexture;
	private RenderTexture videoTexture;

	private VideoPlayer videoPlayer;
	private int rawImageWidth = 2560;
	private int width, height;

	public UDPSender udpSender;

	private void Awake()
	{
		videoPlayer = this.GetComponent<VideoPlayer>();
	}

	public void Initialize(int width, int height)
	{
		this.width = width;
		this.height = height;

		if (useWebCam)
		{
			PlayWebCamera();
		}
		else
		{
			PlayVideo();
		}
	}

	private void PlayWebCamera()
	{
		WebCamDevice[] devices = WebCamTexture.devices;
		if (devices.Length <= webCamIndex)
		{
			webCamIndex = 0;
		}
		//해상도 제한
		webCamTexture = new WebCamTexture(devices[webCamIndex].name,512,512);
		webCamTexture.Play();

		RectTransform rectTransform = rawImage.GetComponent<RectTransform>();
		rawImage.texture = webCamTexture;
		
		
		rectTransform.sizeDelta = new Vector2(rawImageWidth, rawImageWidth * webCamTexture.height / webCamTexture.width);
		float aspect = (float)webCamTexture.width / webCamTexture.height;
		this.transform.localScale = new Vector3(-aspect, 1, 1) * scale;
		this.GetComponent<Renderer>().material.mainTexture = webCamTexture;

		InitializeTexture();
		
		StartCoroutine(SendWebcamFrames());
	}

	private void PlayVideo()
	{
		videoTexture = new RenderTexture((int)videoPlayer.clip.width, (int)videoPlayer.clip.height, 24);

		videoPlayer.renderMode = VideoRenderMode.RenderTexture;
		videoPlayer.targetTexture = videoTexture;

		RectTransform rectTransform = rawImage.GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(rawImageWidth, (int)(rawImageWidth * videoPlayer.clip.height / videoPlayer.clip.width));
		rawImage.texture = videoTexture;

		videoPlayer.Play();

		float aspect = (float)videoTexture.width / videoTexture.height;

		this.transform.localScale = new Vector3(aspect, 1, 1) * scale;
		this.GetComponent<Renderer>().material.mainTexture = videoTexture;

		InitializeTexture();
	}

	private void InitializeTexture()
	{
		GameObject go = new GameObject("Camera", typeof(Camera));

		go.transform.parent = this.transform;
		go.transform.localScale = new Vector3(-1, -1, 1);
		go.transform.localPosition = new Vector3(0, 0, -2);
		go.transform.localEulerAngles = Vector3.zero;

		Camera camera = go.GetComponent<Camera>();
		camera.orthographic = true;
		camera.orthographicSize = 0.5f;
		camera.depth = -5;
		camera.depthTextureMode = 0;
		camera.clearFlags = CameraClearFlags.Color;
		camera.backgroundColor = Color.black;
		camera.useOcclusionCulling = false;
		camera.nearClipPlane = 1.0f;
		camera.farClipPlane = 5.0f;
		camera.allowMSAA = false;
		camera.allowHDR = false;

		renderTexture = new RenderTexture(width, height, 0, RenderTextureFormat.RGB565, RenderTextureReadWrite.sRGB)
		{
			useMipMap = false,
			autoGenerateMips = false,
			wrapMode = TextureWrapMode.Clamp,
			filterMode = FilterMode.Point,
		};

		camera.targetTexture = renderTexture;
	}
	
	IEnumerator SendWebcamFrames()
	{
		while (true)
		{
			if (webCamTexture.isPlaying && webCamTexture.didUpdateThisFrame)
			{
				// 1. RenderTexture 생성 (저해상도)
				RenderTexture rt = new RenderTexture(160, 120, 0);
				Graphics.Blit(webCamTexture, rt);

// 2. Texture2D에 읽기
				RenderTexture.active = rt;
				Texture2D resized = new Texture2D(160, 120, TextureFormat.RGB24, false);
				resized.ReadPixels(new Rect(0, 0, 160, 120), 0, 0);
				resized.Apply();
				RenderTexture.active = null;

// 3. JPG 인코딩
				byte[] jpgBytes = resized.EncodeToJPG(30);

// 4. 정리 및 전송
				Destroy(resized);
				rt.Release();

				udpSender.Send(jpgBytes);
				yield return new WaitForSeconds(0.02f); // 50fps 정도
			}
			else
			{
				yield return null;
			}
		}
	}
}