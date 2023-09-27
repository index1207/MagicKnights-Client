using UnityEngine;

namespace Core
{
    public class Util
    {
        public static T FindChild<T>(GameObject gameObject, string objectName, bool isMultipleComponent = false)
            where T : Component
        {
            if (gameObject == null)
                return null;

            if (isMultipleComponent)
            {
                var components = gameObject.GetComponentsInChildren<T>();
                foreach (var component in components)
                {
                    if (string.IsNullOrEmpty(objectName) || component.name == objectName)
                        return component;
                }
            }
            else
            {
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    Transform t = gameObject.transform.GetChild(i);
                    if (string.IsNullOrEmpty(t.name) || t.name == objectName)
                    {
                        T component = t.GetComponent<T>();
                        if (component != null)
                            return component;
                    }
                }
            }

            return null;
        }
    }
}