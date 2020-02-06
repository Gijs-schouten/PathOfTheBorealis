using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Glyph List", menuName = "Puzzle Assets", order = 1)]
public class GlyphList : ScriptableObject
{
    public List<Mesh> PieceList;

}
