using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace KTM.Editor
{
    public static class Util
    {
        public static ObjectField ScriptField(UnityEditor.Editor editor)
        {
            var script = new ObjectField("Script");
            if(editor.target is ScriptableObject so)
            {
                script.value = MonoScript.FromScriptableObject(so);
            }
            else if(editor.target is MonoBehaviour mb)
            {
                script.value = MonoScript.FromMonoBehaviour(mb);
            }
            else
            {
                return null;
            }
            script.SetEnabled(false);

            return script;
        }
    }
}