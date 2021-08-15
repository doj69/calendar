# Calendar

## Run The Project
You will need the following tools:

* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 5 or later](https://dotnet.microsoft.com/download/dotnet-core/5)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Installing
Follow these steps to get your development environment set up: (Before Run Start the Docker Desktop)
1. Clone the repository
2. Once Docker for Windows is installed, go to the **Settings > Advanced option**, from the Docker icon in the system tray, to configure the minimum amount of memory and CPU like so:
* **Memory: 4 GB**
* CPU: 2
3. At the root directory which include **docker-compose.yml** files, run below command:
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
3. Wait for docker compose all microservices. That’s it! (some microservices need extra time to work so please wait if not worked in first shut)

![mainscreen1](https://user-images.githubusercontent.com/12909707/129490855-1e4b8a16-3953-46a8-9b26-cd865f24700c.png)


## Authors

* **Doni Junior** - *Initial work* - [doju69](https://github.com/doj69)
