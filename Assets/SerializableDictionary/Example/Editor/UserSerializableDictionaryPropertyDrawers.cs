using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(UIDictionary))]


public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}


public class AnySerializableDictionaryStoragePropertyDrawer: SerializableDictionaryStoragePropertyDrawer {}
