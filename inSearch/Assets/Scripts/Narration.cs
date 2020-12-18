using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// see TMP_TextSelector_B.cs

public class Narration : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler//, IPointerClickHandler, IPointerUpHandler
{
    private static Narration _instance;

    private static TextMeshProUGUI textMeshPro;
    private Canvas canvas;
    private Camera cam;

    private static int selectedWord = -1;

    private TMP_MeshInfo[] m_cachedMeshInfoVertexData;

    void Awake()
    {
        if (_instance == null)
        {
            // create singleton
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);

            textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
            canvas = gameObject.GetComponentInParent<Canvas>();
            // Get a reference to the camera if Canvas Render Mode is not ScreenSpace Overlay.
            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                cam = null;
            else
                cam = canvas.worldCamera;
        }
        else
        {// singleton already exists, don't create
            Destroy(this);
        }
    }

    void OnEnable()
    {
        // Subscribe to event fired when text object has been regenerated.
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
    }
    void OnDisable()
    {
        // UnSubscribe to event fired when text object has been regenerated.
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
    }

    void ON_TEXT_CHANGED(Object obj)
    {
        if (obj == textMeshPro)
        {
            // Update cached vertex data.
            m_cachedMeshInfoVertexData = textMeshPro.textInfo.CopyMeshInfoVertexData();
        }
    }

    void Update()
    {
        if (true)
        {

            #region Word Selection Handling
            //Check if Mouse intersects any words and if so assign a random color to that word.
            int wordIndex = TMP_TextUtilities.FindIntersectingWord(textMeshPro, Input.mousePosition, cam);

            // Clear previous word selection.
            if (selectedWord != -1 && (wordIndex == -1 || wordIndex != selectedWord))
            {
                TMP_WordInfo wInfo = textMeshPro.textInfo.wordInfo[selectedWord];

                // Iterate through each of the characters of the word.
                for (int i = 0; i < wInfo.characterCount; i++)
                {
                    int characterIndex = wInfo.firstCharacterIndex + i;

                    // Get the index of the material / sub text object used by this character.
                    int meshIndex = textMeshPro.textInfo.characterInfo[characterIndex].materialReferenceIndex;

                    // Get the index of the first vertex of this character.
                    int vertexIndex = textMeshPro.textInfo.characterInfo[characterIndex].vertexIndex;

                    // Get a reference to the vertex color
                    Color32[] vertexColors = textMeshPro.textInfo.meshInfo[meshIndex].colors32;

                    Color32 c = vertexColors[vertexIndex + 0].Tint(1.33333f);

                    vertexColors[vertexIndex + 0] = c;
                    vertexColors[vertexIndex + 1] = c;
                    vertexColors[vertexIndex + 2] = c;
                    vertexColors[vertexIndex + 3] = c;
                }

                // Update Geometry
                textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

                selectedWord = -1;
            }


            // Word Selection Handling
            if (wordIndex != -1 && wordIndex != selectedWord && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                selectedWord = wordIndex;

                TMP_WordInfo wInfo = textMeshPro.textInfo.wordInfo[wordIndex];

                // Iterate through each of the characters of the word.
                for (int i = 0; i < wInfo.characterCount; i++)
                {
                    int characterIndex = wInfo.firstCharacterIndex + i;

                    // Get the index of the material / sub text object used by this character.
                    int meshIndex = textMeshPro.textInfo.characterInfo[characterIndex].materialReferenceIndex;

                    int vertexIndex = textMeshPro.textInfo.characterInfo[characterIndex].vertexIndex;

                    // Get a reference to the vertex color
                    Color32[] vertexColors = textMeshPro.textInfo.meshInfo[meshIndex].colors32;

                    Color32 c = vertexColors[vertexIndex + 0].Tint(0.75f);

                    vertexColors[vertexIndex + 0] = c;
                    vertexColors[vertexIndex + 1] = c;
                    vertexColors[vertexIndex + 2] = c;
                    vertexColors[vertexIndex + 3] = c;
                }

                // Update Geometry
                textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

            }
            #endregion
        }
    }

    public static void InteractWithDraggedWord(string draggedWord)
    {
        if (selectedWord != -1)
        {
            TMP_WordInfo wInfo = textMeshPro.textInfo.wordInfo[selectedWord];
            string selWord = wInfo.GetWord();
            Debug.Log("INTERACT: " + draggedWord + " " + selWord);
        }

    }
}
