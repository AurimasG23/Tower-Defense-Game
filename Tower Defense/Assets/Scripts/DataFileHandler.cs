using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataFileHandler
{
    //-------------------------------------------------------------------------------------------------
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

    public static void SetBuildingsLocationsOnFirstLaunch(string dataFile, int countOfBuildings)
    {
        string dataPath = Application.persistentDataPath;
        string FilePath = Path.Combine(dataPath, dataFile + ".txt");
        if (!File.Exists(FilePath))
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

    public static int[,] ReadBaseSquares(string dataFile, int gridDimension)
    {
        string dataPath = Application.persistentDataPath;
        string FilePath = Path.Combine(dataPath, dataFile + ".txt");
        int[,] squares = new int[gridDimension, gridDimension];
        if (File.Exists(FilePath))
        {
            StreamReader reader = new StreamReader(new FileStream(FilePath, FileMode.Open));
            int i = 0;
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] values = line.Split(' ');
                for(int j = 0; j < gridDimension; j++)
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

    public static void ChangeBaseSquares(string dataFile, int[,] squares, int dimension)
    {
        string dataPath = Application.persistentDataPath;
        string FilePath = Path.Combine(dataPath, dataFile + ".txt");
        if (squares != null)
        {
            StreamWriter writer = new StreamWriter(new FileStream(FilePath, FileMode.Create));
            for (int i = 0; i < dimension; i++)
            {
                for(int j = 0; j < dimension; j++)
                {
                    if(j < dimension - 1)
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

    public static void SetBuildingsSquaresOnFirstLaunch(string dataFile, int dimension)
    {
        string dataPath = Application.persistentDataPath;
        string FilePath = Path.Combine(dataPath, dataFile + ".txt");
        if (!File.Exists(FilePath))
        {
            StreamWriter writer = new StreamWriter(new FileStream(FilePath, FileMode.Create));
            for (int i = 0; i < dimension; i++)
            {
                for(int j = 0; j < dimension; j++)
                {
                    if (j < dimension - 1)
                    {
                        writer.Write("-1 ");
                    }
                    else
                    {
                        writer.WriteLine("-1");
                    }
                }
            }
            writer.Close();
        }
    }
}
