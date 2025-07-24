using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class SettingsManager : MonoBehaviour
{
    public Toggle voiceToggle;
    public Slider voiceSlider;
    public TMP_Dropdown languageDropdown;
    public Toggle arResetToggle;
    public Toggle themeToggle;
    public Slider textSizeSlider;

    void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        voiceToggle.isOn = PlayerPrefs.GetInt("VoiceOn", 1) == 1;
        voiceSlider.value = PlayerPrefs.GetFloat("VoiceVolume", 1f);
        languageDropdown.value = PlayerPrefs.GetInt("Language", 0);
        arResetToggle.isOn = PlayerPrefs.GetInt("ARReset", 1) == 1;
        themeToggle.isOn = PlayerPrefs.GetInt("ThemeDark", 0) == 1;
        textSizeSlider.value = PlayerPrefs.GetFloat("TextSize", 1f);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("VoiceOn", voiceToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("VoiceVolume", voiceSlider.value);
        PlayerPrefs.SetInt("Language", languageDropdown.value);
        PlayerPrefs.SetInt("ARReset", arResetToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("ThemeDark", themeToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("TextSize", textSizeSlider.value);
        PlayerPrefs.Save();
    }

    public void OnBackPressed()
    {
        SaveSettings();
        SceneController.Instance.GoBack();
    }

    public void OnResetPressed()
    {
        PlayerPrefs.DeleteAll();
        LoadSettings(); // Reset UI to default
    }

    public void OnHelpPressed()
    {
        SceneController.Instance.LoadScene("HelpScene");
    }
}
