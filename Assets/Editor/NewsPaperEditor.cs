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


