using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataFileHandler
{
    //-------------------------------------------------------------------------------------------------
    //nuskaito pastatų pozicijų duomenis
    public static BuildingLocation[] ReadBuildingLocations(string dataFile, int countOfBuildings)
    {
        string dataPath = Application.persistentDataPath;
        string FilePath = Path.Combine(dataPath, dataFile + ".txt");
        BuildingLocation[] locations = new BuildingLocation[countOfBuildings];
        if (File.Exists(FilePath))
        {
            StreamReader reader = new StreamReader(new FileStream(FilePath, FileMode.Open));
            int i = 0;
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] values = line.Split(' ');
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);
                float z = float.Parse(values[2]);
                locations[i] = new BuildingLocation(x,y,z);
                line = reader.ReadLine();
                i++;
            }
            reader.Close();
        }
        return locations;
    }

    //Išsaugo pastatų pozicijų duomenis
    public static void ChangeBuildingLocations(string dataFile, BuildingLocation[] locations, int countOfBuildings)
    {
        string dataPath = Application.persistentDataPath;
        string FilePath = Path.Combine(dataPath, dataFile + ".txt");
        if (locations != null)
        {
            StreamWriter writer = new StreamWriter(new FileStream(FilePath, FileMode.Create));
            for (int i = 0; i < countOfBuildings; i++)
            {
                writer.WriteLine(locations[i].x.ToString() + " " + locations[i].y.ToString() + " " + locations[i].z.ToString());
            }
            writer.Close();
        }
    }

    //Pirmą kart įsijungus bazės statymą, nustato visų pastatų pozicijas į (0, -100, 0)
    public static void SetBuildingsLocationsOnFirstLaunch(string dataFile, int countOfBuildings)
    {
        string dataPath = Application.persistentDataPath;
        string FilePath = Path.Combine(dataPath, dataFile + ".txt");
        if (!File.Exists(FilePath)) // pirma kart paleidziant, failas neegzistuos, todėl bus sukurtas
        {
            StreamWriter writer = new StreamWriter(new FileStream(FilePath, FileMode.Create));
            for (int i = 0; i < countOfBuildings; i++)
            {
                writer.WriteLine("0 -100 0");
            }
            writer.Close();
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------
    //Nuskaito bazės pagrindo laukelių užpildymo duomenis
    public static int[,] ReadBaseSquares(string dataFile, int dimension_X, int dimension_Z)
    {
        string dataPath = Application.persistentDataPath;
        string FilePath = Path.Combine(dataPath, dataFile + ".txt");
        int[,] squares = new int[dimension_Z, dimension_X];
        if (File.Exists(FilePath))
        {
            StreamReader reader = new StreamReader(new FileStream(FilePath, FileMode.Open));
            int i = 0;
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] values = line.Split(' ');
                for (int j = 0; j < dimension_X; j++)
                {
                    squares[i, j] = int.Parse(values[j]);
                }
                line = reader.ReadLine();
                i++;
            }
            reader.Close();
        }
        return squares;
    }

    // Atnaujina bazės pagrindo laukelių užpildymo duomenis
    public static void ChangeBaseSquares(string dataFile, int[,] squares, int dimension_X, int dimension_Z)
    {
        string dataPath = Application.persistentDataPath;
        string FilePath = Path.Combine(dataPath, dataFile + ".txt");
        if (squares != null)
        {
            StreamWriter writer = new StreamWriter(new FileStream(FilePath, FileMode.Create));
            for (int i = 0; i < dimension_Z; i++)
            {
                for (int j = 0; j < dimension_X; j++)
                {
                    if (j < dimension_X - 1)
                    {
                        writer.Write(squares[i, j].ToString() + " ");
                    }
                    else
                    {
                        writer.WriteLine(squares[i, j].ToString());
                    }
                }
            }
            writer.Close();
        }
    }


    // Pirmo paleidimo metu sužymi bazės pagrindo laukelius
    //-1 reiškia kad laukelis tuščias
    //-2 žymi kad ten yra kelias
    public static void SetBuildingsSquaresOnFirstLaunch(string dataFile, int dimension_X, int dimension_Z)
    {
        string dataPath = Application.persistentDataPath;
        string FilePath = Path.Combine(dataPath, dataFile + ".txt");
        if (!File.Exists(FilePath))  // pirma kart paleidziant, failas neegzistuos, todėl bus sukurtas
        {
            StreamWriter writer = new StreamWriter(new FileStream(FilePath, FileMode.Create));
            for (int i = 0; i < dimension_Z; i++)
            {
                for (int j = 0; j < dimension_X; j++)
                {
                    if (i >= 14 && i <= 25) // kelias
                    {
                        if (j < dimension_X - 1)
                        {
                            writer.Write("-2 ");
                        }
                        else
                        {
                            writer.WriteLine("-2");
                        }
                    }
                    else // tušti langeliai
                    {
                        if (j < dimension_X - 1)
                        {
                            writer.Write("-1 ");
                        }
                        else
                        {
                            writer.WriteLine("-1");
                        }
                    }                   
                }
            }
            writer.Close();
        }
    }
    //-----------------------------------------------------------------------------------------------------------

    public static void ReadHighScores(string dataFile, int countOfLeaders, out string[] leaderNames, out int[] leaderScores)
    {
        string dataPath = Application.persistentDataPath;
        string FilePath = Path.Combine(dataPath, dataFile + ".txt");
        leaderNames = new string[countOfLeaders];
        leaderScores = new int[countOfLeaders];
        if (File.Exists(FilePath))
        {
            StreamReader reader = new StreamReader(new FileStream(FilePath, FileMode.Open));
            int i = 0;
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] values = line.Split(' ');
                leaderNames[i] = values[0];
                leaderScores[i] = int.Parse(values[1]);
                line = reader.ReadLine();
                i++;
            }
            reader.Close();
        }
    }

    public static void SaveHighScores(string dataFile, int countOfLeaders, string[] leaderNames, int[] leaderScores)
    {
        string dataPath = Application.persistentDataPath;
        string FilePath = Path.Combine(dataPath, dataFile + ".txt");
        if (leaderNames != null && leaderScores != null)
        {
            StreamWriter writer = new StreamWriter(new FileStream(FilePath, FileMode.Create));
            for (int i = 0; i < countOfLeaders; i++)
            {
                writer.WriteLine(leaderNames[i].ToString() + " " + leaderScores[i].ToString());
            }
            writer.Close();
        }
    }
}
