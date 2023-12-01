
public class BaseLevel
{
    public string[] GetInput()
    {
        return File.ReadAllLines($"{GetType().Name}.txt");
    }
}
