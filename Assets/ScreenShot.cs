using System.Collections;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    public int captureWidth = 1080; // Replace this with your desired resolution width
    public int captureHeight = 1920; // Replace this with your desired resolution height

    // The directory to save the screenshots
    public string screenshotDirectory = "D:/Color-Hole-Copy";


    // Update is called once per frame
    void Update()
    {
        // Check if the user wants to take a screenshot (you can change the input key if needed)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(CaptureScreenshotCoroutine());
        }
    }

    // Coroutine to capture the screenshot
    private IEnumerator CaptureScreenshotCoroutine()
    {
        // Wait for the end of the frame to ensure rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture to store the screenshot
        Texture2D screenshotTexture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);

        // Read the screen content into the texture
        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshotTexture.Apply();

        // Convert the texture to bytes
        byte[] screenshotBytes = screenshotTexture.EncodeToPNG();
        Destroy(screenshotTexture);

        // Create the screenshot directory if it doesn't exist
        if (!System.IO.Directory.Exists(screenshotDirectory))
        {
            System.IO.Directory.CreateDirectory(screenshotDirectory);
        }

        // Generate a file name for the screenshot (you can customize the name if needed)
        string fileName = string.Format("{0}/screenshot_{1}.png", screenshotDirectory, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

        // Save the screenshot to a file
        System.IO.File.WriteAllBytes(fileName, screenshotBytes);

        //Debug.Log("Screenshot saved to: " + fileName);
    }
}
