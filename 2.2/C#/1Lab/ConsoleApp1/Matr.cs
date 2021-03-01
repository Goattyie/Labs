using System;

public class Matr
{
	public Matr(int[] size, float[][] matr)
	{
		matrix = matr;
		height = size[0];
		width = size[1];
	}
	public float[][] matrix;
	public int height, width;
    public override string ToString()
    {
        string line = "\nМатрица:\n";
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                line += matrix[i][j] + " ";
            }
            line += "\n";
        }
        line += "\nДлина: " + width + " Высота: " + height + "\n";
        return line;
    }
}

