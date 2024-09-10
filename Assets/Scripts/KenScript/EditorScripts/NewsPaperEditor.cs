using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewsPaperEditor : EditorWindow
{
    private static NewsPaperEditor _Window;
    private NewsPaperScriptableObject newsPaper;

    [MenuItem("Window/NewsPaper Editor")]
    public static void ShowWindow()
    {
        _Window = GetWindow<NewsPaperEditor>(title: "Create Patient");
        _Window.minSize = new Vector2(500, 600);
    }

    private void OnGUI()
    {
        GUILayout.Label("Configuration du Journal", EditorStyles.boldLabel);

        // S�lectionnez ou cr�ez un ScriptableObject de type NewsPaper
        newsPaper = (NewsPaperScriptableObject)EditorGUILayout.ObjectField("S�lectionnez un Journal", newsPaper, typeof(NewsPaperScriptableObject), false);

        if (newsPaper == null)
        {
            if (GUILayout.Button("Cr�er un nouveau NewsPaper"))
            {
                CreateNewspaperAsset();
            }
        }
        else
        {
            // Afficher les champs pour configurer le ScriptableObject
            newsPaper.newsPaperType = (NewsPaperEnum)EditorGUILayout.EnumPopup("Type de Journal", newsPaper.newsPaperType);
            newsPaper.isFakeNews = EditorGUILayout.Toggle("Intox ?", newsPaper.isFakeNews);
            newsPaper.title = EditorGUILayout.TextField("Titre", newsPaper.title);
            newsPaper.numberOfInformation = EditorGUILayout.IntField("Nombre d'informations", newsPaper.numberOfInformation);

            // Affiche les champs sp�cifiques en fonction du type de NewsPaper
            switch (newsPaper.newsPaperType)
            {
                case NewsPaperEnum.Photo:
                    newsPaper.picture2D = (Texture2D)EditorGUILayout.ObjectField("Photo", newsPaper.picture2D, typeof(Texture2D), false);
                    break;
                
            }
            GUILayout.Label("Informations du Journal:");
            for (int i = 0; i < newsPaper.Information.Count; i++)
            {
                newsPaper.Information[i] = EditorGUILayout.TextField($"Information {i + 1}", newsPaper.Information[i]);
            }

            // Bouton pour ajuster la liste d'informations
            if (GUILayout.Button("Ajuster la liste d'informations"))
            {
                newsPaper.AdjustInformationList();
                EditorUtility.SetDirty(newsPaper);
            }

            // Bouton pour sauvegarder les modifications
            if (GUILayout.Button("Sauvegarder le Journal"))
            {
                EditorUtility.SetDirty(newsPaper);
                AssetDatabase.SaveAssets();
            }
        }
    }

    private void CreateNewspaperAsset()
    {
        // Cr�e une instance de NewsPaper
        NewsPaperScriptableObject newNewsPaper = ScriptableObject.CreateInstance<NewsPaperScriptableObject>();

        // Ouvre une bo�te de dialogue pour sauvegarder l'asset dans le projet
        string path = EditorUtility.SaveFilePanelInProject("Enregistrer un nouveau NewsPaper", "NewNewsPaper", "asset", "Veuillez entrer un nom pour votre nouveau journal");

        if (path == "")
            return; // Si l'utilisateur annule la bo�te de dialogue, ne fait rien

        // Cr�e et sauvegarde l'asset
        AssetDatabase.CreateAsset(newNewsPaper, path);
        AssetDatabase.SaveAssets();

        // S�lectionne le nouvel asset dans le project
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = newNewsPaper;

        // Met � jour la r�f�rence de l'objet ScriptableObject
        newsPaper = newNewsPaper;
        //Utilisation ChatGPT
    }
}


