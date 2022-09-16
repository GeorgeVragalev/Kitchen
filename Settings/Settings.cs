namespace Kitchen.Settings;

public static class Settings
{
    public static readonly int Cooks = 10;
    public static readonly int Waiters = 5;
    public static readonly string DiningHallUrl = "http://host.docker.internal:7090/distribution"; //docker
    // public static readonly string DiningHallUrl = "host.docker.container:7090/distribution"; //local
    public static readonly string TimeUnit= "Seconds";
}
/*
to run docker for kitchen container: 
BUILD IMAGE:
docker build -t kitchen .

RUN CONTAINER: map local_port:exposed_port
docker run --name kitchen-container -p 7091:80 kitchen
*/