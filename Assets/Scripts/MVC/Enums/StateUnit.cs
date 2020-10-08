namespace Enums
{
    public enum StateUnit
    {
        Idle = 0, //спокойный (может стоять, идти, бежать)
        Stunned = 1,//оглушенный, страх, заморозка
        Normal = 2,
        Battle = 3,//battle (стоит, бежит, бьет, смотрит)
        Fly = 4,//fly (прыжок, плавает, летит)
        Died = 5
    }
}