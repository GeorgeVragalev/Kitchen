namespace Kitchen.Settings;

public static class Settings
{
    public static readonly int Cooks = 10;
    public static readonly int Waiters = 5;
    // public static readonly string DiningHallUrl = "http://localhost:5000/distribution"; //docker
    public static readonly string DiningHallUrl = "https://localhost:7090/distribution"; //local
    public static readonly string TimeUnit= "Seconds";
}