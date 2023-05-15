# Master thesis
## Deepfake Detection Framework

**Author:** Bernard Jan, Bc. - xberna18@stud.fit.vutbr.cz

---

Master thesis text: [VUT-DIP-Code Github repository](https://github.com/PlayerBerny12/VUT-DIP)

---

The project hierarchy is as follows:

1. `clients` - Implementations of client applications 
2. `server` - Framework components and deployment files
   -  `api` - API Endpoint implementation
   -  `k8s` - Kuberentes deployment manifests files
   -  `processing_unit` - Request Processing Controller implementation
   -  `tests` - Test and graphs plotting scripts
3. `templates` - Templates for detection methods integration

## Client aplications

It is required to have installed [Node.js](https://nodejs.org/en). Then you can foolow official [Angular Guide](https://angular.io/guide/setup-local) to install it. When everything is prepared you have to install all required dependencies via command:

```
npm install
```

This command should be executed in `clients/browser-extension` folder. After that you can build application by command:

```
npm run build
```
or

```
npm run watch
```
Second command will rebuild aplication when any file is changed.

## API Endpoint

It is required to have installed [ASP.NET Core 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0). Then you can use command (in folder `server/api`):

```
dotnet build
```
to build API Endpoint on local machine. Then you can run this aplication by command:

```
dotnet run --project DeepfakeDetectionFramework
```

To be able run aplication locally it is required to have RabbitMQ and MSSQL database connection strings configured in `appsettings.json`.

Alternativly you can use Visual Studio.

Repository contains definition of automatic build performed by Github Actions. To propaget newly developed features push everything to `main` branch and new Docker image will be automatically published in GHCR registry. Kuberentes then can use newer version of image.

## Deployment

All Kuberenetes deployment manifest files are stored in `server/k8s`. Copy those files to cluster and use `deploy.sh` script for deployment.

You can also use `delete.sh` to compleatly remove framework from cluster.

## Tests

It is required to install packages, `requirements.txt` file is prepared for this purpose. All tests are saved in `server/tests`. Following list contains all commands how to run tests, in `test_core.py` you only have to setup `DFDF_ENDPOINT`.

```
python test_case1.py --real_dir /path/to/real/dir --fake_dir /path/to/fake/dir
python test_case2.py --dir /path/to/test/dir
python test_case3.py --dir /path/to/test/dir
```

For the first test case you can use another script to generate graphs.

```
python graphs_audio.py output_audio.csv
python graphs_video.py output_video.csv
```

## Integrated detection methods
-   [AudioDeepFakeDetection](https://github.com/PlayerBerny12/AudioDeepFakeDetection)
-   [fakeVideoForensics](https://github.com/PlayerBerny12/fakeVideoForensics)
