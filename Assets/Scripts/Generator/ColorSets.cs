using UnityEngine;

namespace Scripts.Generator
{
    public class ColorSets : MonoBehaviour
    {
        [SerializeField] private Color[] colorSet1;
        [SerializeField] private Color[] colorSet2;
        [SerializeField] private Color[] colorSet3;
        [SerializeField] private Color[] colorSet4;
        [SerializeField] private Color[] colorSet5;

        public Color[][] Colors => new[] { colorSet1, colorSet2, colorSet3, colorSet4, colorSet5 };

        public Color[] GetRandomColorSet()
        {
            var colors = Colors;
            return colors[Random.Range(0, colors.Length)];
        }
    }
}