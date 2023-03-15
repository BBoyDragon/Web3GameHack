public interface IMaze
{
    public int SizeX();
    public int SizeY();

    public bool IsHorizontalWall(int x, int y);
    public bool IsVerticalWall(int x, int y);    
}
