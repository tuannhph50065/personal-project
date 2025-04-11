using UnityEngine;

[CreateAssetMenu(fileName = "NewEnamy", menuName = "FPT/EnemyInformation")]
public class EnemyInfomation : ScriptableObject
{
    public int id;
    public float health;
    public float speed;
    public float atkDmg;
    public float def;
    
}
