using System;
using UnityEngine;

namespace Scripts.Entity.Flask
{
    public class FlaskGameObject : MonoBehaviour
    {
        private const float heightRiseOffset = 0.4f;

        [SerializeField] private SpriteRenderer[] layersSprite;
        [SerializeField] private Color emptyColor;

        public bool IsSelect { get; private set; }

        public FlaskUnit flask { get; private set; }
        private Transform transform_;
        private Action<FlaskGameObject> flaskSelectAction;

        private void Start()
        {
            transform_ = transform;
        }

        public void Init(FlaskUnit flaskUnit, Action<FlaskGameObject> flaskSelectAction)
        {
            this.flask = flaskUnit;
            this.flaskSelectAction = flaskSelectAction;
            UpdateLiquidsSprite();
        }
        
        public void PutUpFlask()
        {
            MoveFlask(heightRiseOffset);
            IsSelect = true;
        }
        
        public void PutDownFlask()
        {
            MoveFlask(-heightRiseOffset);
            IsSelect = false;
        }

        private void OnMouseDown()
        {
            flaskSelectAction(this);
        }
        

        private void MoveFlask(float height)
        {
            var oldPosition = transform.position;
            transform_.position = new Vector3(oldPosition.x, oldPosition.y + height);
        }

        public void UpdateLiquidsSprite()
        {
            var liquids = flask.Liquids;
            for (int i = 0; i < FlaskUnit.LiquidLayerQuantity; i++)
            {
                layersSprite[i].color = liquids[i]?.Color ?? emptyColor;
            }
        }
    }
}