using System.Diagnostics;

public class Tool
{
    private Stopwatch _watch = new Stopwatch();
    public TimeSpan TimeIt(Action bench)
    {
        _watch.Reset();
        _watch.Start();
        bench.Invoke();
        _watch.Stop();
        return _watch.Elapsed;
    }

    public void WriteToFile(string path, string data)
    {
        File.WriteAllText(path, data);
    }
}
