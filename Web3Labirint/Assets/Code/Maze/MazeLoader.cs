using System;
using System.IO;
using System.Text;

public class MazeLoader
{
    public static Maze LoadFromFile(string path)
    {
        using (StreamReader reader = new(path))
        {
            string[] dimentions = reader.ReadLine().Split(' ');
            if (dimentions.Length != 2)
            {
                throw new ArgumentException("Invalid maze dimentions. Expected 2, found: " + dimentions.Length);
            }
            int sizeX = ParseDimention(dimentions[0]);
            int sizeY = ParseDimention(dimentions[1]);
            reader.ReadLine();

            var maze = new Maze(sizeX, sizeY);

            FillDimention(reader, sizeX, sizeY, "Horizontal",
                (int x, int y, string symbol) => { maze.SetHorizontalWall(x, y, symbol == "1"); return true; });
            
            reader.ReadLine();

            FillDimention(reader, sizeX, sizeY, "Vertical",
                (int x, int y, string symbol) => { maze.SetVerticalWall(x, y, symbol == "1"); return true; });

            return maze;
        }
    }

    public static void SaveToFile(string path, Maze maze)
    {
        using (StreamWriter writer = new(path))
        {
            int sizeX = maze.SizeX();
            int sizeY = maze.SizeY();
            writer.WriteLine(sizeX + " " + sizeY);
            writer.WriteLine();
            
            var horizintalBuilder = new StringBuilder();
            var verticalBuilder = new StringBuilder();
            for (int y = 0; y <= sizeY; y++)
            {
                for (int x = 0; x <= sizeX; x++)
                {
                    horizintalBuilder.Append((maze.IsHorizontalWall(x, y) ? "1" : "0") + " ");
                    verticalBuilder.Append((maze.IsVerticalWall(x, y) ? "1" : "0") + " ");
                }
                horizintalBuilder.AppendLine();
                verticalBuilder.AppendLine();
            }

            writer.Write(horizintalBuilder.ToString());
            writer.WriteLine();
            writer.Write(verticalBuilder.ToString());
        }
    }

    private static void FillDimention(StreamReader reader, int sizeX, int sizeY, string dimentionName, Func<int, int, string, bool> func)
    {
        for (int y = 0; y <= sizeY; y++)
        {
            string[] line = reader.ReadLine().Split(' ');
            if (line.Length < sizeX + 1)
            {
                throw new ArgumentException("Invalid " + dimentionName + " maze map x dimention. Expected: " + (sizeX + 1) + ", found: " + line.Length + "\n" + string.Join(",", line));
            }
            for (int x = 0; x <= sizeX; x++)
            {
                if (line[x] != "0" && line[x] != "1")
                {
                    throw new ArgumentException("Invalid " + dimentionName + " maze map entry. Expected: '0' or '1', found: " + line[x]);
                }
                func(x, y, line[x]);
            }
        }
    }

    private static int ParseDimention(string dimention)
    {
        try
        {
            return Int32.Parse(dimention);
        }
        catch (FormatException)
        {
            throw new ArgumentException("Can't parse sizeX to INT. Found: '" + dimention + "'");
        }
    }
}
