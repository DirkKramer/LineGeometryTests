using CommunityToolkit.Mvvm.ComponentModel;
using HelixToolkit.SharpDX.Core;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;

namespace LineGeometryTests {
    public partial class MainViewModel : ObservableObject {

        public MainViewModel() {
            // Init camera
            camera = SetOrthographicCamera();

            // Init shadow camera
            shadowLightCamera = SetShadowCamera();

            // Init floor
            var mesh = new MeshBuilder();
            mesh.AddBox(new Vector3(0, 0, -2000), 40000, 40000, 2000);
            floorGeometry = mesh.ToMeshGeometry3D();
            floorMaterial = new PhongMaterial {             
                RenderShadowMap = true,
                DiffuseColor = Color.Yellow.ToColor4()
            };

            // Init model
            var mesh2 = new MeshBuilder();
            mesh2.AddBox(new Vector3(3000, 3000, 0), 2000, 2000, 2000);
            //mesh2.AddCone(shadowLightCamera.Position.ToVector3(), shadowLightCamera.LookDirection.ToVector3(), 10, 20000,100000,true,true,16);
            boxGeometry = mesh2.ToMeshGeometry3D();
            boxMaterial = PhongMaterials.Red;

            // Init linemodel
            var mesh3 = new LineBuilder();
            mesh3.AddLine(new Vector3(0, 0, 100), new Vector3(0, 1000, 100));
            mesh3.AddLine(new Vector3(0, 1000, 100), new Vector3(1000, 1000, 100));
            mesh3.AddLine(new Vector3(1000, 1000, 100), new Vector3(1000, 0, 100));
            mesh3.AddLine(new Vector3(1000, 0, 100), new Vector3(0, 0, 100));
            lineGeometry = mesh3.ToLineGeometry3D();
            lineMaterial = new LineMaterial {
                Color = System.Windows.Media.Colors.Red,
                Thickness = 50,
                EnableDistanceFading = false,
                FixedSize = false,
                Smoothness = 1,
            };
        }

        [ObservableProperty]
        Camera camera;

        [ObservableProperty]
        Camera shadowLightCamera;

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
        public Camera SetOrthographicCamera() {
            return new OrthographicCamera {
                Position = new System.Windows.Media.Media3D.Point3D(0, 0, 10000),
                LookDirection = new System.Windows.Media.Media3D.Vector3D(-1, -1, -1),
                UpDirection = new System.Windows.Media.Media3D.Vector3D(0, 0, 1),
                FarPlaneDistance = 400000,
                NearPlaneDistance = -400000,
                Width = 100000
            };

        }

        public Camera SetShadowCamera() {
            //ShadowLightCamera
            return new OrthographicCamera {
                Position = new System.Windows.Media.Media3D.Point3D(-20000, -20000, 20000),
                LookDirection = new System.Windows.Media.Media3D.Vector3D(1, 1, -1),
                UpDirection = new System.Windows.Media.Media3D.Vector3D(0, 0, 1),
                FarPlaneDistance = 400000,
                NearPlaneDistance = -400000,
                Width = 100000
            };
        }
    }
}
