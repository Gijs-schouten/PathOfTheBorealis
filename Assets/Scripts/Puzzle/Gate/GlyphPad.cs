using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GlyphPiece
{
    Nine,
    YTree,
    Fish,
    Crosshair,
    NS,
    MaxPiece
}
public class GlyphPad : MonoBehaviour
{

    public GlyphPiece GlyphEnum;
    [SerializeField]
    private GlyphList GlyphMeshes;
    private MeshFilter Filter;

    private Vector3 DefaultPos;
    private Vector3 DownPos;

    private float Pressure;
    private bool Moving;
    private bool Changed;

    private void Awake()
    {
        Filter = GetComponent<MeshFilter>();

        DefaultPos = transform.position;
        DownPos = transform.position + new Vector3(0, -.2f, 0);

        Filter.mesh = GlyphMeshes.PieceList[(int)GlyphEnum];
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(DefaultPos, DownPos, Pressure);

        if (Moving)
            Pressure += 1 * Time.deltaTime;
        else
            Pressure -= 1 * Time.deltaTime;

        Pressure = Mathf.Clamp(Pressure, 0, 1);
        if (Pressure > .9f && !Changed)
        {
            GlyphEnum++;
            if (GlyphEnum >= GlyphPiece.MaxPiece)
                GlyphEnum = 0;

            Filter.mesh = GlyphMeshes.PieceList[(int)GlyphEnum];
            Changed = true;
        }
        else if (Pressure < .5f && Changed)
            Changed = false;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.CompareTag("Player"))
            Moving = true;
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.root.CompareTag("Player"))
            Moving = false;
    }
}
