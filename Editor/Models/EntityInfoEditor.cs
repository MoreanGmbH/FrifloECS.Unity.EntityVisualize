using Friflo.Engine.ECS;
using UnityEditor;
using UnityEngine;

namespace FrifloECS.Unity.EntityVisualize.Editor.Models
{
    public partial class EntityInfo
    {
        public void OnInspectorGUI()
        {
            var color = 0;
            if (Components.Count > 0)
            {
                using (new GUILayout.VerticalScope("box"))
                {
                    GUILayout.Label("Components", EditorStyles.boldLabel);
                }

                using (new GUILayout.VerticalScope())
                {
                    for (var i = 0; i < Components.Count; i++)
                    {
                        if (i > 0) GUILayout.Space(8);

                        var componentInfo = Components[i];
                        var componentType = componentInfo.EntityComponent.Type;
                        var rect = EditorGUILayout.GetControlRect();
                        EditorGUI.DrawRect(rect, GetRainbowColor(color++));

                        var labelRect = rect;
                        labelRect.x += 5;
                        labelRect.width -= 35;
                        EditorGUI.LabelField(labelRect, componentInfo.ComponentName, EditorStyles.boldLabel);

                        var removeRect = new Rect(rect.xMax - 20, rect.y, 20, rect.height);
                        if (GUI.Button(removeRect, "x", EditorStyles.miniButton))
                        {
                            if (componentType.Type == typeof(TreeNode))
                            {
                                while (Entity.ChildCount > 0)
                                {
                                    Entity.RemoveChild(Entity.ChildEntities[0]);
                                }
                            }

                            EntityUtils.RemoveEntityComponent(Entity, componentType);
                            continue;
                        }

                        GUILayout.Space(3);
                        using (new GUILayout.HorizontalScope())
                        {
                            GUILayout.Space(20);
                            using (new GUILayout.VerticalScope())
                            {
                                componentInfo.OnInspectorGUI(Entity);
                            }
                        }
                    }
                }
            }

            if (Tags.Count > 0)
            {
                using (new GUILayout.VerticalScope("box"))
                {
                    GUILayout.Label("Tags", EditorStyles.boldLabel);
                }

                using (new GUILayout.VerticalScope())
                {
                    for (var i = 0; i < Tags.Count; i++)
                    {
                        var tag = Tags[i];
                        var rect = EditorGUILayout.GetControlRect();
                        var width = rect.width;
                        rect.x += 15;
                        rect.width -= 15;
                        EditorGUI.DrawRect(rect, GetRainbowColor(color++));
                        rect.x += 5;
                        rect.width -= 35;
                        EditorGUI.LabelField(rect, tag.TagName, EditorStyles.boldLabel);
                        rect.width = 20;
                        rect.x = width - 20;
                        if (GUI.Button(rect, "x", EditorStyles.miniButton))
                        {
                            Entity.RemoveTags(new Tags(tag));
                        }
                    }
                }
            }

            if (Scripts.Count > 0)
            {
                using (new GUILayout.VerticalScope("box"))
                {
                    GUILayout.Label("Scripts", EditorStyles.boldLabel);
                }

                using (new GUILayout.VerticalScope())
                {
                    for (var i = 0; i < Scripts.Count; i++)
                    {
                        var script = Scripts[i];
                        var rect = EditorGUILayout.GetControlRect();
                        rect.x += 15;
                        rect.width -= 15;
                        EditorGUI.DrawRect(rect, GetRainbowColor(color++));
                        rect.x += 5;
                        rect.width -= 5;
                        EditorGUI.LabelField(rect, script.GetType().Name, EditorStyles.boldLabel);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the rainbow color using the specified index
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The color</returns>
        private static Color GetRainbowColor(int index)
        {
            var color = Color.HSVToRGB(index / 16f % 1f, 1, 1) * 0.25f;
            color.a = 1;
            return color;
        }
    }
}
