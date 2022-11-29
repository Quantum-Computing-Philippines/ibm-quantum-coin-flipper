using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class apiKeyGetter: MonoBehaviour
{
    // Input Area
    TMP_InputField outputArea;
    TMP_InputField resultsArea;
    TMP_InputField inputBackendKeyArea;
    TMP_InputField inputAPIKeyArea;
    TMP_InputField OutputLeastBackendKeyArea;

    // Scenes Game Object  
    public GameObject apiKeyScreen;
    public GameObject availableBackendScreen;
    public GameObject chooseYourBackendScreen;
    public GameObject flipScreen;

 
    void Start()
    {
        // Global Vars
        inputAPIKeyArea = GameObject.Find("InputAPIKeyField").GetComponent<TMP_InputField>();
        inputBackendKeyArea = GameObject.Find("InputBackendKeyField").GetComponent<TMP_InputField>();


        GameObject.Find("SaveAPIButton").GetComponent<Button>().onClick.AddListener(Disable_Screen_2);
        GameObject.Find("NextButton").GetComponent<Button>().onClick.AddListener(Disable_Screen_3);
        GameObject.Find("SaveBackendButton").GetComponent<Button>().onClick.AddListener(Disable_Screen_4);

        GameObject.Find("PostButton").GetComponent<Button>().onClick.AddListener(PostData);
        GameObject.Find("FlipButton").GetComponent<Button>().onClick.AddListener(QuantumCoinFlip);
         GameObject.Find("LeastBusyBackendButton").GetComponent<Button>().onClick.AddListener(LeastBusyBackend);

    }
  
    // Functions to be called in Start()
    void PostData() => StartCoroutine(PostData_Coroutine());
    void QuantumCoinFlip() => StartCoroutine(QuantumCoinFlip_Coroutine());
    void LeastBusyBackend() => StartCoroutine(LeastBusyBackend_Coroutine());
    void Disable_Screen_2() => StartCoroutine(Disable_Screen_2_Coroutine());
    void Disable_Screen_3() => StartCoroutine(Disable_Screen_3_Coroutine());
    void Disable_Screen_4() => StartCoroutine(Disable_Screen_4_Coroutine());



    IEnumerator Disable_Screen_2_Coroutine(){
        apiKeyScreen.SetActive(false);
        availableBackendScreen.SetActive(true);
        yield return inputAPIKeyArea.text;
    }

    IEnumerator Disable_Screen_3_Coroutine(){
        apiKeyScreen.SetActive(false);
        availableBackendScreen.SetActive(false);
        yield return inputAPIKeyArea.text;
    }

    IEnumerator Disable_Screen_4_Coroutine(){
        apiKeyScreen.SetActive(false);
        availableBackendScreen.SetActive(false);
        chooseYourBackendScreen.SetActive(false);
        yield return  inputBackendKeyArea.text;
    }

    IEnumerator PostData_Coroutine()
    {
        outputArea = GameObject.Find("outputArea").GetComponent<TMP_InputField>();
        Debug.Log("VALUEEEE");
         Debug.Log(inputAPIKeyArea.text);
        outputArea.text = "Loading...";
        string uri = "localhost:8000/backends/";
        WWWForm form = new WWWForm();
        string apiKeyFromInput = inputAPIKeyArea.text;
        form.AddField("apikey", apiKeyFromInput);
        using(UnityWebRequest request = UnityWebRequest.Post(uri, form))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                outputArea.text = request.error;
            else
                outputArea.text = request.downloadHandler.text;
        }
    }

    IEnumerator QuantumCoinFlip_Coroutine()
    {
        resultsArea = GameObject.Find("resultsArea").GetComponent<TMP_InputField>();
        Debug.Log("VALUEEEE");
        Debug.Log(inputAPIKeyArea.text);
        resultsArea.text = "Flipping...";

        string uri = "localhost:8000/results/";

        WWWForm form = new WWWForm();
        string apiKeyFromInput = inputAPIKeyArea.text;
        string backendKeyFromInput =  inputBackendKeyArea.text;

        form.AddField("apikey", apiKeyFromInput);
        form.AddField("backendkey", backendKeyFromInput);

        using(UnityWebRequest request = UnityWebRequest.Post(uri, form))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                 //resultsArea.text = request.error;
                 resultsArea.text = "Error: Check the format of your IBM API Key or your IBM Backend";
            else
                 resultsArea.text = request.downloadHandler.text;
        }
    }

    IEnumerator LeastBusyBackend_Coroutine()
    {
        OutputLeastBackendKeyArea = GameObject.Find("OutputLeastBackendKeyField").GetComponent<TMP_InputField>();
        OutputLeastBackendKeyArea.text = "Loading...";

        string uri = "localhost:8000/backends/least/";

        WWWForm form = new WWWForm();
        string apiKeyFromInput = inputAPIKeyArea.text;
        string backendKeyFromInput =  inputBackendKeyArea.text;

        form.AddField("apikey", apiKeyFromInput);

        using(UnityWebRequest request = UnityWebRequest.Post(uri, form))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                 OutputLeastBackendKeyArea.text = request.error;
                 //resultsArea.text = "Error: Check the syntax of your IBM API Key or your IBM Backend";
            else
                 OutputLeastBackendKeyArea.text = request.downloadHandler.text;
        }
    }



}