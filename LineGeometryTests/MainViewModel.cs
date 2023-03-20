using CommunityToolkit.Mvvm.ComponentModel;
using HelixToolkit.SharpDX.Core;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using System;
using System.IO;

namespace LineGeometryTests {
    public partial class MainViewModel : ObservableObject {
        public MemoryStream LoadFileToMemory(string filePath) {
            if (File.Exists(filePath) == false)
                throw new FileNotFoundException(filePath);

            using (var file = new FileStream(filePath, FileMode.Open)) {
                var memory = new MemoryStream();
                file.CopyTo(memory);
                return memory;
            }

        }
        public static string GetAppDataFolder() {
            string foldername = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Net Designer\";
            if (!Directory.Exists(foldername))
                Directory.CreateDirectory(foldername);
            return foldername;
        }

        public static string BaseFolder { get; set; }
        public MainViewModel() {

            BaseFolder = GetAppDataFolder() + @"Materials\";

            // Init camera
            camera = SetOrthographicCamera();

            // Init shadow camera
            shadowLightCamera = SetShadowCamera();

            // Init floor
            var mesh = new MeshBuilder();
            mesh.AddBox(new Vector3(0, 0, -1), 40, 40, 2);
            floorGeometry = mesh.ToMeshGeometry3D();
            floorMaterial = new PhongMaterial {
                RenderShadowMap = true,
                DiffuseColor = Color.Yellow.ToColor4()
            };

            // Init model
            var mesh2 = new MeshBuilder();
            mesh2.AddBox(new Vector3(3, 3, 1), 2, 2, 2);
            //mesh2.AddCone(shadowLightCamera.Position.ToVector3(), shadowLightCamera.LookDirection.ToVector3(), 10, 20000,100000,true,true,16);
            boxGeometry = mesh2.ToMeshGeometry3D();
            boxMaterial = PhongMaterials.Red;


            // Init linemodel
            var mesh3 = new LineBuilder();
            mesh3.AddLine(new Vector3(0, 0, 1f), new Vector3(0, 1, 0.1f));
            mesh3.AddLine(new Vector3(0, 1, 1f), new Vector3(1, 1, 0.1f));
            mesh3.AddLine(new Vector3(1, 1, 1f), new Vector3(1, 0, 0.1f));
            mesh3.AddLine(new Vector3(1, 0, 1f), new Vector3(0, 0, 0.1f));
            lineGeometry = mesh3.ToLineGeometry3D();
            lineMaterial = new LineMaterial {
                Color = System.Windows.Media.Colors.Red,
                Thickness = 0.05f,
                EnableDistanceFading = false,
                FixedSize = false,
                Smoothness = 1,
            };




            // Init rope
            //var mesh4 = new MeshBuilder();
            //mesh4.AddCylinder(new Vector3(-10, -10, 2), new Vector3(10, 10, 2), 0.05);
            //ropeGeometry = mesh4.ToMeshGeometry3D();
            //ropeMaterial = new PhongMaterial {
            //    DiffuseMap = LoadFileToMemory(BaseFolder + @"Rope2\Rope002_2K_Color.jpg"),
            //    RenderDiffuseMap = true,
            //    RenderShadowMap = false,
            //    DiffuseColor = SharpDX.Color.White,
            //};
            //for (int i = 0; i < ropeGeometry.TextureCoordinates.Count; ++i) {
            //    ropeGeometry.TextureCoordinates[i] *= new Vector2(1, 50);
            //}


            //// Init Chain
            //var mesh5 = new MeshBuilder();
            //mesh5.AddQuad(
            //  new Vector3(0, 0, 0), new Vector3(50, 0, 0),
            //    new Vector3(10, 10, 0), new Vector3(10, 10, 0));
            ////mesh5.AddCylinder(new Vector3(-10000, -10000, 2500), new Vector3(10000, 10000, 2500), 50,5);
            //chainGeometry = mesh5.ToMeshGeometry3D();
            //chainMaterial = new PhongMaterial {
            //    DiffuseMap = LoadFileToMemory(BaseFolder + @"Chain\chain2.png"),
            //    RenderDiffuseMap = true,
            //    RenderShadowMap = false,
            //    DiffuseColor = SharpDX.Color.Red,
            //};
            //for (int i = 0; i < chainGeometry.TextureCoordinates.Count; ++i) {
            //    chainGeometry.TextureCoordinates[i] *= new Vector2(1, 5);
            //}

        }

        [ObservableProperty]
        Camera camera;

        [ObservableProperty]
        Camera shadowLightCamera;

        [ObservableProperty]
        MeshGeometry3D ropeGeometry;

        [ObservableProperty]
        MeshGeometry3D chainGeometry;

        [ObservableProperty]
        Geometry3D boxGeometry;

        [ObservableProperty]
        Geometry3D floorGeometry;

        [ObservableProperty]
        Geometry3D lineGeometry;

        [ObservableProperty]
        Material boxMaterial;

        [ObservableProperty]
        Material floorMaterial;

        [ObservableProperty]
        Material lineMaterial;

        [ObservableProperty]
        Material ropeMaterial;

        [ObservableProperty]
        Material chainMaterial;
        public Camera SetOrthographicCamera() {
            return new OrthographicCamera {
                Position = new System.Windows.Media.Media3D.Point3D(0, 0, 1),
                LookDirection = new System.Windows.Media.Media3D.Vector3D(-1, -1, -1),
                UpDirection = new System.Windows.Media.Media3D.Vector3D(0, 0, 1),
                FarPlaneDistance = 400,
                NearPlaneDistance = -400,
                Width = 100
            };

        }

        public Camera SetShadowCamera() {
            //ShadowLightCamera
            return new OrthographicCamera {
                Position = new System.Windows.Media.Media3D.Point3D(0, 0, 1),
                LookDirection = new System.Windows.Media.Media3D.Vector3D(1, 1, -1),
                UpDirection = new System.Windows.Media.Media3D.Vector3D(0, 0, 1),
                FarPlaneDistance = 400,
                NearPlaneDistance = -400,
                Width = 100
            };
        }
    }
}
