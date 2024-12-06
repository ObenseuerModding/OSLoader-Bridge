using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OSLoader
{
    [CreateAssetMenu(fileName = "Mod States")]
    internal class ModStates : ScriptableObject
    {
        public Sprite error;
        public Sprite dependencyIssue;
        public Sprite unloaded;
        public Sprite loaded;
    }
}
