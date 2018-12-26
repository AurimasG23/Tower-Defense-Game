using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataFileHandler
{
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

    public static void ChangeBuildingLocation(string dataFile, BuildingLocation[] locations, int countOfBuildings)
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
        StreamWriter writer = new StreamWriter(new FileStream(FilePath, FileMode.Create));
        for (int i = 0; i < countOfBuildings; i++)
        {
            writer.WriteLine("0 -100 0");
        }
        writer.Close();
    }
}
