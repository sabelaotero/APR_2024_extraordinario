 using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using System.Data.Common;
using Mono.Data.Sqlite;
using System.Drawing;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public static DBManager Instance { get; private set; }
    private string dbUri = "URI=file:mydb.sqlite";
    private string SQL_COUNT_ELEMNTS = "SELECT count(*) FROM Posiciones;";
    private string SQL_CREATE_POSICIONES = "CREATE TABLE IF NOT EXISTS Posiciones (" +
        "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
        "name TEXT, " +
        "timestamp TIMESTAMP, " +
        "x REAL NOT NULL, " +
        "y REAL NOT NULL, " +
        "z REAL NOT NULL);";
    private string NEW_SQL_CREATE_POSICIONES = "CREATE TABLE IF NOT EXISTS NewPosiciones (" +
        "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
        "unitId INTEGER, " +
        "timestamp TIMESTAMP, " +
        "x REAL NOT NULL, " +
        "y REAL NOT NULL, " +
        "z REAL NOT NULL, " +
        "FOREIGN KEY (unitId) REFERENCES Unit (unitId);";
    private string SQL_CREATE_UNIT = "CREATE TABLE IF NOT EXISTS Unit (" +
        "unitId INTEGER PRIMARY KEY AUTOINCREMENT, " +
        "unit TEXT);";

    private IDbConnection dbConnection;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        OpenDatabase();
        InitializeDB();
        NewInitializeDB();
    }

    private void OpenDatabase()
    {
        dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "PRAGMA foreign_keys = ON";
        dbCommand.ExecuteNonQuery();
    }

    private void InitializeDB()
    {
        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = SQL_CREATE_POSICIONES;
        dbCmd.ExecuteReader();
    }

    private void NewInitializeDB()
    {
        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = SQL_CREATE_UNIT;
        dbCmd.ExecuteReader();

        IDbCommand dbCmd2 = dbConnection.CreateCommand();
        dbCmd2.CommandText = NEW_SQL_CREATE_POSICIONES;
        dbCmd2.ExecuteReader();
    }

    public void SavePosition(CharacterPosition position)
    {
        string command = "INSERT INTO Posiciones (name, timestamp, x, y, z) " +
            "VALUES ('" + position.name + "', " + position.timestamp + ", " +
           position.position.x + ", " + position.position.y + ", " + position.position.z + ");";
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = command;
        dbCommand.ExecuteNonQuery();
    }

    private void OnDestroy()
    {
        dbConnection.Close();
    }
}
