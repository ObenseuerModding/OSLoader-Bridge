using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OSLoader;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace OSLoader
{
    internal class LoaderMainMenu : MonoBehaviour
    {
        public static LoaderMainMenu Instance { get; private set; }

        public GameObject loaderMenu;
        public GameObject noModsFoundText;
        public TMP_Text title;

        public TMP_Text confirmationText;
        public int confirmationTextLifetime = 400;
        public int confirmationTextLife = 0;

        public Button closeButton;
        public Button saveButton;

        public GameObject modsContentWindow;

        public List<ModEntryUI> modUIs = new List<ModEntryUI>();

        public const int initialSpacingAtFirstModEntry = 15;
        public const int spacingBetweenModEntries = 10;

        private void Awake()
        {
            Instance = this;
            Loader.Instance.logger.Log("Loader Menu Awake!");
        }

        public void Initialize()
        {
            Loader.Instance.logger.Log("Loader Menu Initialized!");
            title.text = $"OSLoader Menu (F10 to toggle) v{Loader.Instance.config.Version}";
            
            closeButton.onClick.AddListener(OnClose);
            saveButton.onClick.AddListener(OnSave);

            List<ModReference> mods = Loader.Instance.mods;

            if (mods.Count == 0)
            {
                return;
            }

            noModsFoundText.SetActive(false);

            foreach (ModReference mod in Loader.Instance.mods)
            {
                ModEntryUI modUI = Instantiate(Loader.Instance.prefabs.modEntry, modsContentWindow.transform).GetComponent<ModEntryUI>();
                modUIs.Add(modUI);
                modUI.mod = mod;
                modUI.Initialize();
            }

            UpdateModPositions();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                Loader.Instance.logger.Log("Toggling loader window");
                if (loaderMenu.activeSelf)
                    OnClose();
                else
                    OnOpen();
            }
        }

        private void FixedUpdate()
        {
            if (confirmationTextLife > 0)
            {
                confirmationTextLife--;
                if (!confirmationText.gameObject.activeSelf)
                {
                    confirmationText.gameObject.SetActive(true);
                }
            }
            else if (confirmationText.gameObject.activeSelf)
            {
                confirmationText.gameObject.SetActive(false);
            }
        }

        public void OnAnyValueChanged()
        {
            saveButton.interactable = true;
        }

        public void OnSave()
        {
            modUIs.ForEach(modUI => modUI.UISettings.ForEach(setting => setting.OnSave()));
            modUIs.ForEach(modUI => modUI.OnDoneSaving());
            SetConfirmationText("Successfully saved settings!", Color.green);
            saveButton.interactable = false;
        }

        public void SetConfirmationText(string text, Color color)
        {
            if (!enabled) return;

            confirmationText.text = text;
            confirmationText.color = color;
            confirmationTextLife = confirmationTextLifetime;
        }

        public void OnClose()
        {
            loaderMenu.SetActive(false);
        }

        public void OnOpen()
        {
            loaderMenu.SetActive(true);
        }

        public void UpdateModPositions()
        {
            int y = initialSpacingAtFirstModEntry;

            foreach (ModEntryUI mod in modUIs)
            {
                // UI Y-axis points down
                ((RectTransform)mod.transform).localPosition = new Vector3(0, -y, 0);
                y += (int)((RectTransform)mod.transform).sizeDelta.y;
                y += spacingBetweenModEntries;
            }

            ((RectTransform)modsContentWindow.transform).sizeDelta =
                new Vector2(((RectTransform)modsContentWindow.transform).sizeDelta.x, y);
        }
    }
}
