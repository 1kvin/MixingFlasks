using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Scripts.Entity;
using Scripts.Entity.Flask;
using Scripts.GameLogic.Entity;
using Scripts.Generator.LiquidMixers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Generator
{
    [RequireComponent(typeof(ColorSets))]
    public class LevelGenerator : MonoBehaviour
    {
        private const float widthOffset = 1.5f;
        private const float heightOffset = 3.5f;

        [SerializeField] private Camera camera;
        [SerializeField] private FlaskGameObject flaskPrefab;
        [SerializeField] private ColorSets colorSets;
        [SerializeField] private int[] numberFlaskPerDifficulty;
        [SerializeField] private float[] cameraSizePerDifficulty;

        private readonly List<FlaskGameObject> flasks = new List<FlaskGameObject>();

        public ReadOnlyCollection<FlaskGameObject> Flasks => new ReadOnlyCollection<FlaskGameObject>(flasks);

        public void ReGenerateMap(Action<FlaskGameObject> flaskSelectAction, Map map)
        {
            InstanceFlasksOnMap(flaskSelectAction, (Map)map.Clone());
        }

        public Map Generate(Action<FlaskGameObject> flaskSelectAction)
        {
            var levelDifficulty = Random.Range(0, numberFlaskPerDifficulty.Length);
            var flaskCount = numberFlaskPerDifficulty[levelDifficulty];

            camera.orthographicSize = cameraSizePerDifficulty[levelDifficulty];

            return Generate(flaskSelectAction, flaskCount, 2);
        }

        private Map Generate(Action<FlaskGameObject> flaskSelectAction, int flaskCount, int empty)
        {
            var flaskUnits = GenerateFlaskUnits(flaskCount, empty);
            IRandomLiquidMixer liquidMixer = new RandomRearrangementMixer();
            liquidMixer.Mix(flaskUnits);

            var map = new Map(flaskUnits, flaskCount);
            InstanceFlasksOnMap(flaskSelectAction, (Map)map.Clone());

            return map;
        }

        private List<FlaskUnit> GenerateFlaskUnits(int flaskCount, int emptyCount)
        {
            var colors = colorSets.GetRandomColorSet();
            int generateFlaskNum = flaskCount - emptyCount;
            var flaskUnits = new List<FlaskUnit>(generateFlaskNum);

            for (int i = 0; i < generateFlaskNum; i++)
            {
                var liquids = new Liquid[FlaskUnit.LiquidLayerQuantity];

                for (int j = 0; j < FlaskUnit.LiquidLayerQuantity; j++)
                {
                    liquids[j] = new Liquid(colors[i]);
                }

                flaskUnits.Add(new FlaskUnit(liquids));
            }

            for (int i = 0; i < emptyCount; i++)
            {
                flaskUnits.Add(new FlaskUnit());
            }

            return flaskUnits;
        }

        private void InstanceFlasksOnMap(Action<FlaskGameObject> flaskSelectAction, Map map)
        {
            InstanceFlasksOnMap(flaskSelectAction, map.flaskUnits, map.numberFlask);
        }

        private void InstanceFlasksOnMap(Action<FlaskGameObject> flaskSelectAction,
            IEnumerable<FlaskUnit> flaskUnits, int numberFlask)
        {
            int sqrtN = (int)Math.Ceiling(Math.Sqrt(numberFlask));

            int row = (int)Math.Ceiling((decimal)numberFlask / sqrtN);
            int column = sqrtN;

            var flaskStack = new Stack<FlaskUnit>(flaskUnits);
            var middlePoint = new Vector2((column - 1) * widthOffset / 2, (row - 1) * heightOffset / 2);
            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < column; x++)
                {
                    if (flasks.Count >= numberFlask) return;
                    
                    var instFlask = Instantiate(flaskPrefab,
                        new Vector3(x * widthOffset - middlePoint.x, y * heightOffset - middlePoint.y, 0),
                        transform.rotation);
                    instFlask.Init(flaskStack.Pop(), flaskSelectAction);

                    flasks.Add(instFlask);
                }
            }
        }

        public void Clear()
        {
            foreach (var flask in flasks)
            {
                Destroy(flask.gameObject);
            }

            flasks.Clear();
        }
    }
}