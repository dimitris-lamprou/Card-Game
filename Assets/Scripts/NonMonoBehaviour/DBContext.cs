using Mono.Data.Sqlite;
using System.IO;
using UnityEngine;

public static class DBContext
{
    private static readonly string dbPath = Path.Combine(Application.streamingAssetsPath, "Database.db");
    private static readonly string connString = "URI=file:" + dbPath;

    public static SqliteConnection db = new(connString);
    public static SqliteCommand cmd = db.CreateCommand();
}
