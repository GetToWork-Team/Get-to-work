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

        // Sélectionnez ou créez un ScriptableObject de type NewsPaper
        newsPaper = (NewsPaperScriptableObject)EditorGUILayout.ObjectField("Sélectionnez un Journal", newsPaper, typeof(NewsPaperScriptableObject), false);

        if (newsPaper == null)
        {
            if (GUILayout.Button("Créer un nouveau NewsPaper"))
            {
                CreateNewspaperAsset();
            }
        }
        else
        {
            // Afficher les champs pour configurer le ScriptableObject
            newsPaper.isFakeNews = EditorGUILayout.Toggle("Intox ?", newsPaper.isFakeNews);
            newsPaper.newsPaperTexture = (Sprite)EditorGUILayout.ObjectField("Photo", newsPaper.newsPaperTexture, typeof(Sprite), false);


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
        // Crée une instance de NewsPaper
        NewsPaperScriptableObject newNewsPaper = ScriptableObject.CreateInstance<NewsPaperScriptableObject>();

        // Ouvre une boîte de dialogue pour sauvegarder l'asset dans le projet
        string path = EditorUtility.SaveFilePanelInProject("Enregistrer un nouveau NewsPaper", "NewNewsPaper", "asset", "Veuillez entrer un nom pour votre nouveau journal");

        if (path == "")
            return; // Si l'utilisateur annule la boîte de dialogue, ne fait rien

        // Crée et sauvegarde l'asset
        AssetDatabase.CreateAsset(newNewsPaper, path);
        AssetDatabase.SaveAssets();

        // Sélectionne le nouvel asset dans le project
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = newNewsPaper;

        // Met à jour la référence de l'objet ScriptableObject
        newsPaper = newNewsPaper;
        //Utilisation ChatGPT
    }
}


