using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(UIDictionary))]
    [CustomPropertyDrawer(typeof(ItemDictionary))]
[CustomPropertyDrawer(typeof(MEDSstring))]
[CustomPropertyDrawer(typeof(TileColors))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}


public class AnySerializableDictionaryStoragePropertyDrawer: SerializableDictionaryStoragePropertyDrawer {}
