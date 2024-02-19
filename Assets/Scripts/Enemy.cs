using UnityEngine;
using Mono.Data.Sqlite;

public class Enemy : MonoBehaviour
{
    public static readonly int hpCap = 4;

    public static int hp = 4;
    public static int block = 0;
    public static int action;

    public static int addBlock;
    public static int attack;

    public static Card dazed;

    private static readonly SqliteConnection db = DBContext.db;
    private static readonly SqliteCommand cmd = DBContext.cmd;

    private void Start()
    {
        action = Random.Range(0, 5);

        db.Open();
        cmd.CommandText = "Select * from Card where title = 'Dazed' ";
        var reader = cmd.ExecuteReader();

        dazed = new()
        {
            Id = int.TryParse(reader["id"].ToString(), out int id) ? id : (int?)null,
            Title = reader["title"].ToString(),
            Description = reader["description"].ToString(),
            Cost = int.TryParse(reader["cost"].ToString(), out int cost) ? cost : (int?)null,
            Effect = reader["effect"].ToString(),
            Experience = int.TryParse(reader["experience"].ToString(), out int experience) ? experience : (int?)null,
            Status = reader["status"].ToString()
        };

        db.Close();
        reader.Close();
    }
}
