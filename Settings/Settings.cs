namespace Kitchen.Settings;

public static class Settings
{
    public static readonly int Cooks = 4;
    // public static readonly string DiningHallUrl = "http://host.docker.internal:7090/distribution"; //docker
    public static readonly string DiningHallUrl = "https://localhost:7090/distribution"; //local
    public static readonly int TimeUnit = 1; //seconds = 1000  ms = 1 minutes = 60000 
}
/*
to run docker for kitchen container: 
BUILD IMAGE:
docker build -t kitchen .

RUN CONTAINER: map local_port:exposed_port
docker run --name kitchen-container -p 7091:80 kitchen
*/