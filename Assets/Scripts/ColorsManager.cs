using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "colorManager", menuName = "Colors Manager")]
public class ColorsManager : ScriptableObject
{
    private class ArrayEqualityComparer : IEqualityComparer<bool[]>
    {
        public bool Equals(bool[] x, bool[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(bool[] obj)
        {
            int result = 17;
            for (int i = 0; i < obj.Length; i++)
            {
                unchecked
                {
                    result = result * 23 + obj[i].GetHashCode();
                }
            }
            return result;
        }
    }

    public Material blackMaterial;
    public Material redMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    public Material yellowMaterial;
    public Material magentaMaterial;
    public Material cyanMaterial;
    public Material whiteMaterial;

    private Dictionary<bool[], Color> colorTable = new Dictionary<bool[], Color>(new ArrayEqualityComparer());

    private Color[] allColorValues;

    private Material[] allMaterials;

    public void OnEnable()
    {
        colorTable.Add(new bool[] { false, false, false }, blackMaterial.color);

        colorTable.Add(new bool[] { true, false, false }, redMaterial.color);
        colorTable.Add(new bool[] { false, true, false }, greenMaterial.color);
        colorTable.Add(new bool[] { false, false, true }, blueMaterial.color);

        colorTable.Add(new bool[] { true, false, true }, yellowMaterial.color);
        colorTable.Add(new bool[] { true, true, false }, magentaMaterial.color);
        colorTable.Add(new bool[] { false, true, true }, cyanMaterial.color);

        colorTable.Add(new bool[] { true, true, true }, whiteMaterial.color);

        allColorValues = new Color[colorTable.Values.Count];
        colorTable.Values.CopyTo(allColorValues, 0);

        allMaterials = new Material[] { blackMaterial, redMaterial, greenMaterial, blueMaterial, yellowMaterial, magentaMaterial, cyanMaterial, whiteMaterial };
    }
    
    public Color GetColor(bool red, bool green, bool blue)
    {
        return colorTable[new bool[]{ red, green, blue }];
    }

    public Material GetRandomMaterial()
    {
        return allMaterials[Random.Range(0, allMaterials.Length - 1)];
    }



}
