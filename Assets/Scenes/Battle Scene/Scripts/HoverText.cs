using TMPro;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class HoverText : MonoBehaviour
{
    [SerializeField] private TMP_Text statusDescriptionText;

    private Dictionary<string, string> statusEffectDiscriptionDictionary = new();
    private List<string> statusEffects = new();

    private static readonly SqliteConnection db = DBContext.db;
    private static readonly SqliteCommand cmd = DBContext.cmd;

    private void Start()
    {
        db.Open();
        cmd.CommandText = "Select * from StatusEffectDescription";
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            statusEffectDiscriptionDictionary[reader["StatusEffect"].ToString()] = reader["Description"].ToString();
            statusEffects.Add(reader["StatusEffect"].ToString());
        }
        db.Close();
        reader.Close();
    }

    private void OnMouseEnter()
    {
        TMP_Text text = GetComponent<TMP_Text>();

        if (text.text.Equals(""))
        {
            return;
        }

        statusDescriptionText.enabled = true;

        foreach (var statusEffect in statusEffects)
        {
            if (text.text.Contains(statusEffect))
            {
                statusDescriptionText.text += statusEffectDiscriptionDictionary[statusEffect] + " ";
            }
        }
    }

    private void OnMouseExit()
    {
        statusDescriptionText.text = string.Empty;
        statusDescriptionText.enabled = false;
    }
}
