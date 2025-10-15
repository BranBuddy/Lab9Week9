using UnityEngine;

public class Enemy
{

    public float Size { get; internal set; }
    public int PointValue { get; internal set; }
    public float Speed { get; internal set; }

    public class Builder
    {
        private Enemy enemy = new Enemy();

        public Builder SetParameters(float size, int pointAmount, float speed)
        {
            enemy.Size = size;
            enemy.PointValue = pointAmount;
            enemy.Speed = speed;

            return this;

        }

        public Enemy Build()
        {
            return enemy;
        }
    }
}
