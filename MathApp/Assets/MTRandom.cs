using System;
public static class MTRandom
{
    private static object lockObj = new object();
    private static Random random = new Random();
    public static int Next(int minValueIncl, int maxValueExcl)
    {
        lock (lockObj)
        {
            return random.Next(minValueIncl, maxValueExcl);
        }
    }

}