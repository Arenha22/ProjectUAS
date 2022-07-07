using System;
using OpenTK.Windowing.Desktop;
using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace projek

{
    static class Constants
    {
        public const string path = "../../../Shaders/";
        
    }
    internal class Window : GameWindow
    {
        Cubemap cubemap;
        private readonly Vector3[] _pointLightPositions =
{
            new Vector3(0f, 3.75f, 0f),
            new Vector3(-17, 4f, 0),
        };




        double _time;
        float degr = 0;
        Camera _camera;
        bool _firstMove = true;
        Vector2 _lastPos;
        
        float _rotationSpeed = 0.4f;
        float angelAroundPlayer = 0;
        
        Asset3d[] character = new Asset3d[6];
        Asset3d lampu = new Asset3d();
        Asset3d[] LightObjects = new Asset3d[2];
        Asset3d floor = new Asset3d();
        Asset3d floor2 = new Asset3d();
        Asset3d[] bed = new Asset3d[9];
        Asset3d[] desk = new Asset3d[6];
        Asset3d[] chair = new Asset3d[6];
        Asset3d[] wall = new Asset3d[8];
        Asset3d[] bookshelf = new Asset3d[6];
        Asset3d[] wardrobe = new Asset3d[10];
        Asset3d[] nightstand = new Asset3d[6];
        Asset3d[] tv = new Asset3d[5];
        Asset3d roof = new Asset3d();
        Asset3d[] tree = new Asset3d[16];
        Asset3d pole = new Asset3d();
        Asset3d moon = new Asset3d();
        bool move = true;
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
     
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            cubemap = new Cubemap("../../../Shaders/skyboxShader.vert",
            "../../../Shaders/skyboxShader.frag");
            cubemap.Load();

            _camera = new Camera(new Vector3(-14, 1f, 2f), Size.X / (float)Size.Y);

            character[0] = new Asset3d();
            character[0].createBoxVertices3(-14, 0.3f, 0, 0.4f, 0.4f, 0.4f);
            character[1] = new Asset3d();
            character[1].createBoxVertices3(-14, -0.5f, 0, 0.7f, 1.2f, 0.5f);
            character[2] = new Asset3d();
            character[2].createBoxVertices3(-14.4f, -0.4f, 0, 0.2f, 1f, 0.5f);
            character[3] = new Asset3d();
            character[3].createBoxVertices3(-13.6f, -0.4f, 0, 0.2f, 1f, 0.5f);
            character[4] = new Asset3d();
            character[4].createBoxVertices3(-14.3f, -1.5f, 0, 0.3f, 1f, 0.5f);
            character[5] = new Asset3d();
            character[5].createBoxVertices3(-13.7f, -1.5f, 0, 0.3f, 1f, 0.5f);
            for (int i = 0; i < 6; i++) { 
                character[i].OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
            }

            floor.createBoxVertices3(0, -2, 0, 10,0.01f,10);
            floor.OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
            floor2.createBoxVertices3(0, -2.2f, 0, 50, 0.01f, 50);
            floor2.OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);

            bed[0] = new Asset3d();
            bed[0].createBoxVertices3(-0.9f, -1.65f, -1.9f, 0.1f, 0.75f, 0.1f);
            bed[1] = new Asset3d();
            bed[1].createBoxVertices3(0.9f, -1.65f, -1.9f, 0.1f, 0.75f, 0.1f);
            bed[2] = new Asset3d();
            bed[2].createBoxVertices3(-0.9f, -1.65f, -4.75f, 0.1f, 0.75f, 0.1f);
            bed[3] = new Asset3d();
            bed[3].createBoxVertices3(0.9f, -1.65f, -4.75f, 0.1f, 0.75f, 0.1f);
            bed[4] = new Asset3d();
            bed[4].createBoxVertices3(0, -1.25f, -3.3f, 2f, 0.25f, 3f);
            bed[5] = new Asset3d();
            bed[5].createBoxVertices3(0, -0.75f, -4.75f, 2f, 1f, 0.1f);
            bed[6] = new Asset3d();
            bed[6].createBoxVertices3(0, -1f, -3.3f, 1.9f, 0.25f, 2.9f);
            bed[7] = new Asset3d();
            bed[7].createBoxVertices3(-0.45F, -0.75f, -4.35f, 0.8f, 0.25f, 0.5f);
            bed[8] = new Asset3d();
            bed[8].createBoxVertices3(0.45F, -0.75f, -4.35f, 0.8f, 0.25f, 0.5f);

            for (int i = 0; i < 9; i++)
            {
                bed[i].OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
            }

            desk[0] = new Asset3d();
            desk[0].createBoxVertices3(1.5f, -1.65f, 3.9f, 0.1f, 2.15f, 0.1f);
            desk[1] = new Asset3d();
            desk[1].createBoxVertices3(4.75f, -1.65f, 3.9f, 0.1f, 2.15f, 0.1f);
            desk[2] = new Asset3d();
            desk[2].createBoxVertices3(1.5f, -1.65f, 4.75f, 0.1f, 2.15f, 0.1f);
            desk[3] = new Asset3d();
            desk[3].createBoxVertices3(4.75f, -1.65f, 4.75f, 0.1f, 2.15f, 0.1f);
            desk[4] = new Asset3d();
            desk[4].createBoxVertices3(3.15f, -0.5f, 4.2f, 3.5f, 0.25f, 1.4f);
            desk[5] = new Asset3d();
            desk[5].createBoxVertices3(2.3f, -0.7f, 4.2f, 1.5f, 0.25f, 1.4f);

            for (int i = 0; i < 6; i++)
            {
                desk[i].OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
            }

            chair[0] = new Asset3d();
            chair[0].createBoxVertices3(2.7f, -1.65f, 3.2f, 0.1f, 0.65f, 0.1f);
            chair[1] = new Asset3d();
            chair[1].createBoxVertices3(3.4f, -1.65f, 3.2f, 0.1f, 0.65f, 0.1f);
            chair[2] = new Asset3d();
            chair[2].createBoxVertices3(2.7f, -1.65f, 2.5f, 0.1f, 0.65f, 0.1f);
            chair[3] = new Asset3d();
            chair[3].createBoxVertices3(3.4f, -1.65f, 2.5f, 0.1f, 0.65f, 0.1f);
            chair[4] = new Asset3d();
            chair[4].createBoxVertices3(3.05f, -1.2f, 2.85f, 1f, 0.25f, 1f);
            chair[5] = new Asset3d();
            chair[5].createBoxVertices3(3f, -0.8f, 2.35f, 1f, 1f, 0.25f);

            for (int i = 0; i < 6; i++)
            {
                chair[i].OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
            }

            wall[0] = new Asset3d();
            wall[0].createBoxVertices3(0f, 1.25f, -4.95f, 10, 6.5f, 0.1f);
            wall[1] = new Asset3d();
            wall[1].createBoxVertices3(0f, 1.25f, 4.95f, 10, 6.5f, 0.1f);
            wall[2] = new Asset3d();
            wall[2].createBoxVertices3(4.95f, 1.25f, 0f, 0.1f, 6.5f, 10f);
            wall[3] = new Asset3d();
            wall[3].createBoxVertices3(-4.95f, 1f, 4f, 0.1f, 6f, 2f);
            wall[4] = new Asset3d();
            wall[4].createBoxVertices3(-4.95f, 1f, -2.5f, 0.1f, 6f, 5f);
            wall[5] = new Asset3d();
            wall[5].createBoxVertices3(-4.95f, 4f, 0f, 0.1f, 1f, 10f);
            wall[6] = new Asset3d();
            wall[6].createBoxVertices3(-4.95f, 2.5f, 0f, 0.1f, 0.75f, 10f);
            wall[7] = new Asset3d();
            wall[7].createBoxVertices3(-3.75f, 0f, 0f, 2.5f, 4.25f, 0.1f);

            for (int i = 0; i < 8; i++)
            {
                wall[i].OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
            }

            bookshelf[0] = new Asset3d();
            bookshelf[0].createBoxVertices3(-1.45f, -1.2f, 4.75f, 3, 2f, 0.1f);
            bookshelf[1] = new Asset3d();
            bookshelf[1].createBoxVertices3(0f, -1.2f, 4.2f, 0.1f, 2f, 1f);
            bookshelf[2] = new Asset3d();
            bookshelf[2].createBoxVertices3(-2.9f, -1.2f, 4.2f, 0.1f, 2f, 1f);
            bookshelf[3] = new Asset3d();
            bookshelf[3].createBoxVertices3(-1.45f, -1.9f, 4.25f, 3f, 0.25f, 1.1f);
            bookshelf[4] = new Asset3d();
            bookshelf[4].createBoxVertices3(-1.45f, -1.05f, 4.25f, 3f, 0.25f, 1.1f);
            bookshelf[5] = new Asset3d();
            bookshelf[5].createBoxVertices3(-1.45f, -0.2f, 4.25f, 3f, 0.25f, 1.1f);

            for (int i = 0; i < 6; i++)
            {
                bookshelf[i].OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
            }

            wardrobe[0] = new Asset3d();
            wardrobe[0].createBoxVertices3(4.2f, 0f, -3.5f, 1.5f, 4f, 0.1f);
            wardrobe[1] = new Asset3d();
            wardrobe[1].createBoxVertices3(4.9f, 0f, -1.5f, 0.1f, 4f, 4f);
            wardrobe[2] = new Asset3d();
            wardrobe[2].createBoxVertices3(4.2f, 0f, 0.5f, 1.5f, 4f, 0.1f);
            wardrobe[3] = new Asset3d();
            wardrobe[3].createBoxVertices3(4.2f, 0.75f, -1.5f, 1.5f, 0.1f, 4f);
            wardrobe[4] = new Asset3d();
            wardrobe[4].createBoxVertices3(4.2f, 1.95f, -1.5f, 1.5f, 0.1f, 4f);
            wardrobe[5] = new Asset3d();
            wardrobe[5].createBoxVertices3(4.2f, -1.95f, -1.5f, 1.5f, 0.1f, 4f);
            wardrobe[6] = new Asset3d();
            wardrobe[6].createBoxVertices3(4f, 0.45f, -1.5f, 0.1f, 0.1f, 4f);
            wardrobe[7] = new Asset3d();
            wardrobe[7].createBoxVertices3(4.2f, 1.35f, -1.5f, 1.4f, 1.2f, 0.1f);
            wardrobe[8] = new Asset3d();
            wardrobe[8].createBoxVertices3(3.5f, 0f, 0.5f, 3, 4f, 0.1f);
            wardrobe[8].rotate(wardrobe[8]._centerPosition, Vector3.UnitY, 10);
            wardrobe[9] = new Asset3d();
            wardrobe[9].createBoxVertices3(3.5f, 0f, -3.5f, 3, 4f, 0.1f);
            wardrobe[9].rotate(wardrobe[9]._centerPosition, Vector3.UnitY, -10);

            for (int i = 0; i < 10; i++)
            {
                wardrobe[i].OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
            }

            nightstand[0] = new Asset3d();
            nightstand[0].createBoxVertices3(-2.25f, -1.3f, -4.75f, 2, 1.5f, 0.1f);
            nightstand[1] = new Asset3d();
            nightstand[1].createBoxVertices3(-1.3f, -1.3f, -4.2f, 0.1f, 1.5f, 1f);
            nightstand[2] = new Asset3d();
            nightstand[2].createBoxVertices3(-3.2f, -1.3f, -4.2f, 0.1f, 1.5f, 1f);
            nightstand[3] = new Asset3d();
            nightstand[3].createBoxVertices3(-2.25f, -1.9f, -4.25f, 2f, 0.25f, 1.1f);
            nightstand[4] = new Asset3d();
            nightstand[4].createBoxVertices3(-2.25f, -1.1f, -4.25f, 2f, 0.25f, 1.1f);
            nightstand[5] = new Asset3d();
            nightstand[5].createBoxVertices3(-2.25f, -0.6f, -4.25f, 2f, 0.25f, 1.1f);

            for (int i = 0; i < 6; i++)
            {
                nightstand[i].OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
            }

            tv[0] = new Asset3d();
            tv[0].createBoxVertices3(0f, 2f, 4.75f, 3, 2f, 0.1f);
            tv[1] = new Asset3d();
            tv[1].createBoxVertices3(0f, 3.05f, 4.75f, 3, 0.15f, 0.1f);
            tv[2] = new Asset3d();
            tv[2].createBoxVertices3(0f, 0.95f, 4.75f, 3, 0.15f, 0.1f);
            tv[3] = new Asset3d();
            tv[3].createBoxVertices3(-1.55f, 2f, 4.75f, 0.15f, 2.25f, 0.1f);
            tv[4] = new Asset3d();
            tv[4].createBoxVertices3(1.55f, 2f, 4.75f, 0.15f, 2.25f, 0.1f);

            for (int i = 0; i < 5; i++)
            {
                tv[i].OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
            }

            tree[0] = new Asset3d();
            tree[0].createBoxVertices3(-14f, -0.8f, -2f, 1, 3f, 1f);
            tree[1] = new Asset3d();
            tree[1].createBoxVertices3(-14f, -1.5f, 4f, 1, 2f, 1f);
            tree[2] = new Asset3d();
            tree[2].createBoxVertices3(-10f, -1.5f, -6f, 1, 2f, 1f);
            tree[3] = new Asset3d();
            tree[3].createBoxVertices3(-10f, -0.8f, 8f, 1, 3f, 1f);
            tree[4] = new Asset3d();
            tree[4].createBoxVertices3(-20f, -0.6f, -10f, 1, 3.5f, 1f);
            tree[5] = new Asset3d();
            tree[5].createBoxVertices3(-17f, -0.6f, 15f, 1, 3.5f, 1f);
            tree[6] = new Asset3d();
            tree[6].createBoxVertices3(-5f, -0.6f, -15f, 1, 4.2f, 1f);
            tree[7] = new Asset3d();
            tree[7].createBoxVertices3(-7f, -0.6f, 14f, 1, 3.2f, 1f);
            tree[8] = new Asset3d();
            tree[8].createBoxVertices3(-14f, 1.25f, -2f, 2.5f, 2.5f, 2.5f);
            tree[9] = new Asset3d();
            tree[9].createBoxVertices3(-14f, 0.25f, 4f, 2.5f, 1.5f, 2.5f);
            tree[10] = new Asset3d();
            tree[10].createBoxVertices3(-10f, 0.25f, -6f, 2.5f, 1.5f, 2.5f);
            tree[11] = new Asset3d();
            tree[11].createBoxVertices3(-10f, 1.25f, 8f, 2.5f, 2.5f, 2.5f);
            tree[12] = new Asset3d();
            tree[12].createBoxVertices3(-20f, 1.75f, -10f, 2.5f, 2.55f, 2.5f);
            tree[13] = new Asset3d();
            tree[13].createBoxVertices3(-17f, 1.75f, 15f, 2.5f, 2.5f, 2.5f);
            tree[14] = new Asset3d();
            tree[14].createBoxVertices3(-5f, 2.45f, -15f, 2.5f, 3.7f, 2.5f);
            tree[15] = new Asset3d();
            tree[15].createBoxVertices3(-7f, 1.45f, 14f, 2.5f, 2.7f, 2.5f);

            for (int i = 0; i < 16; i++)
            {
                tree[i].OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
            }

            pole.createBoxVertices3(-17, -0.8f, 0, 0.5f, 9f, 0.5f);
            pole.OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);

            floor2.createBoxVertices3(0, -2.2f, 0, -50, 0.01f, -50);
            floor2.OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
            roof.createBoxVertices3(0f, 4.5f, 0f, 15f, 0.5f, 15f);
            roof.OnLoad_withnormal(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);

            //lampu.createBoxVertices2(0f, 4.0f, 0f, 0.5f);
            //lampu.OnLoad_withnormal(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);


            for (int i = 0; i < 2; i++)
            {
                LightObjects[i] = new Asset3d();
                LightObjects[i].createBoxVertices2(_pointLightPositions[i].X, _pointLightPositions[i].Y, _pointLightPositions[i].Z, 1f);
                LightObjects[i].OnLoad_withnormal(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
            }
            
            moon.createBoxVertices2(0, 50, 0, 0.5f);
            moon.OnLoad_withnormal(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
            
            //CursorGrabbed = true;
        }
        public Matrix4 generateArbRotationMatrix(Vector3 axis, Vector3 center, float degree)
        {
            var rads = MathHelper.DegreesToRadians(degree);

            var secretFormula = new float[4, 4] {
                { (float)Math.Cos(rads) + (float)Math.Pow(axis.X, 2) * (1 - (float)Math.Cos(rads)), axis.X* axis.Y * (1 - (float)Math.Cos(rads)) - axis.Z * (float)Math.Sin(rads),    axis.X * axis.Z * (1 - (float)Math.Cos(rads)) + axis.Y * (float)Math.Sin(rads),   0 },
                { axis.Y * axis.X * (1 - (float)Math.Cos(rads)) + axis.Z * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Y, 2) * (1 - (float)Math.Cos(rads)), axis.Y * axis.Z * (1 - (float)Math.Cos(rads)) - axis.X * (float)Math.Sin(rads),   0 },
                { axis.Z * axis.X * (1 - (float)Math.Cos(rads)) - axis.Y * (float)Math.Sin(rads),   axis.Z * axis.Y * (1 - (float)Math.Cos(rads)) + axis.X * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Z, 2) * (1 - (float)Math.Cos(rads)), 0 },
                { 0, 0, 0, 1}
            };
            var secretFormulaMatix = new Matrix4
            (
                new Vector4(secretFormula[0, 0], secretFormula[0, 1], secretFormula[0, 2], secretFormula[0, 3]),
                new Vector4(secretFormula[1, 0], secretFormula[1, 1], secretFormula[1, 2], secretFormula[1, 3]),
                new Vector4(secretFormula[2, 0], secretFormula[2, 1], secretFormula[2, 2], secretFormula[2, 3]),
                new Vector4(secretFormula[3, 0], secretFormula[3, 1], secretFormula[3, 2], secretFormula[3, 3])
            );

            return secretFormulaMatix;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 temp = Matrix4.Identity;
            _time += 9.0 * args.Time;
            degr += MathHelper.DegreesToRadians(0.5f);
            //temp = temp * Matrix4.CreateRotationX(degr);
            /*for (int i = 0; i < 10; i++)
            {
                _object3d[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                _object3d[i].setFragVariable(new Vector3(1.0f, 0.0f, 0.0f), _camera.Position);
                _object3d[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                //_object3d[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                //_object3d[i].setSpotLight(new Vector3(0, 10, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                //1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                _object3d[i].setSpotLight(_camera.Position, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                _object3d[i].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }*/

            for (int i = 0; i < 6; i++)
            {
                character[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                character[i].setFragVariable(new Vector3(1f, 1f, 1.0f), _camera.Position);
                character[i].setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.1f, 0.1f, 0.1f));
                character[i].setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.1f, 0.1f, 0.1f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                character[i].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.1f, 0.1f, 0.1f), 1.0f, 0.09f, 0.032f);

            }

            floor.OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            floor.setFragVariable(new Vector3(0.98f, 0.5f, 0.47f), _camera.Position);
            //floor.setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
            floor.setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
            1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            floor.setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);

            floor2.OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            floor2.setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.1f, 0.1f, 0.1f));
            floor2.setFragVariable(new Vector3(0f, 0.8f, 0.0f), _camera.Position);
            floor2.setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.1f, 0.1f, 0.1f), 1.0f, 0.09f, 0.032f);
            floor2.setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.1f, 0.1f, 0.1f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));

            for (int i = 0; i < 6; i++)
            {
                bed[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                bed[i].setFragVariable(new Vector3(0.54f, 0.27f, 0.07f), _camera.Position);
               //bed[i].setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                bed[i].setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.1f, 0.1f, 0.1f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                bed[i].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.1f, 0.1f, 0.1f), 1.0f, 0.09f, 0.032f);
            }
            for (int i = 6; i < 9; i++)
            {
                bed[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                bed[i].setFragVariable(new Vector3(1f, 1f, 1f), _camera.Position);
                //bed[i].setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                bed[i].setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.1f, 0.1f, 0.1f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                bed[i].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.1f, 0.1f, 0.1f), 1.0f, 0.09f, 0.032f);
            }

            for (int i = 0; i < 6; i++)
            {
                desk[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                desk[i].setFragVariable(new Vector3(0.95f, 0.64f, 0.37f), _camera.Position);
                //desk[i].setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                desk[i].setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.1f, 0.1f, 0.1f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                desk[i].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.1f, 0.1f, 0.1f), 1.0f, 0.09f, 0.032f);
            }

            for (int i = 0; i < 6; i++)
            {
                chair[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                chair[i].setFragVariable(new Vector3(0.95f, 0.64f, 0.37f), _camera.Position);
               //chair[i].setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                chair[i].setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.1f, 0.1f, 0.1f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                chair[i].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.1f, 0.1f, 0.1f), 1.0f, 0.09f, 0.032f);

            }

            for (int i = 0; i < 8; i++)
            {
                wall[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                wall[i].setFragVariable(new Vector3(1f, 0.98f, 0.98f), _camera.Position);
                wall[i].setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.1f, 0.1f, 0.1f));
                wall[i].setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.1f, 0.1f, 0.1f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                wall[i].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.1f, 0.1f, 0.1f), 1.0f, 0.09f, 0.032f);

            }
            for (int i = 0; i < 6; i++)
            {
                bookshelf[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                bookshelf[i].setFragVariable(new Vector3(0.54f, 0.27f, 0.07f), _camera.Position);
                //bookshelf[i].setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                bookshelf[i].setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.1f, 0.1f, 0.1f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                bookshelf[i].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.1f, 0.1f, 0.1f), 1.0f, 0.09f, 0.032f);

            }
            for (int i = 0; i < 10; i++)
            {
                wardrobe[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                wardrobe[i].setFragVariable(new Vector3(0.96f, 0.87f, 0.7f), _camera.Position);
                //wardrobe[i].setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                wardrobe[i].setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.1f, 0.1f, 0.1f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                wardrobe[i].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.1f, 0.1f, 0.1f), 1.0f, 0.09f, 0.032f);

            }

            for (int i = 0; i < 6; i++)
            {
                nightstand[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                nightstand[i].setFragVariable(new Vector3(0.54f, 0.27f, 0.07f), _camera.Position);
               // nightstand[i].setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                nightstand[i].setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.1f, 0.1f, 0.1f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                nightstand[i].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.1f, 0.1f, 0.1f), 1.0f, 0.09f, 0.032f);

            }

            tv[0].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            tv[0].setFragVariable(new Vector3(0.3f, 0.3f, 0.3f), _camera.Position);
            //tv[0].setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
            tv[0].setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            tv[0].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);

            for (int i = 1; i < 5; i++)
            {
                tv[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                tv[i].setFragVariable(new Vector3(0f, 0f, 0f), _camera.Position);
                //tv[i].setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                //tv[i].setPointLight(lampu._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.2f, 0.2f, 0.2f), 1.0f, 0.09f, 0.032f);
                tv[i].setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                tv[i].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);

            }
            roof.OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            roof.setFragVariable(new Vector3(1f, 1f, 1f), _camera.Position);
            roof.setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.1f, 0.1f, 0.1f));
            //roof.setPointLight(lampu._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.2f, 0.2f, 0.2f), 1.0f, 0.09f, 0.032f);
            roof.setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.1f, 0.1f, 0.1f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            roof.setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.1f, 0.1f, 0.1f), 1.0f, 0.09f, 0.032f);

            for (int i = 0; i < 8; i++)
            {
                tree[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                tree[i].setFragVariable(new Vector3(0.54f, 0.27f, 0.07f), _camera.Position);
                tree[i].setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.1f, 0.1f, 0.1f));
                //tree[i].setPointLight(lampu._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.2f, 0.2f, 0.2f), 1.0f, 0.09f, 0.032f);
                tree[i].setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.1f, 0.1f, 0.1f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                tree[i].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.1f, 0.1f, 0.1f), 1.0f, 0.09f, 0.032f);

            }

            for (int i = 8; i < 16; i++)
            {
                tree[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
                tree[i].setFragVariable(new Vector3(0.6f, 0.8f, 0.19f), _camera.Position);
                tree[i].setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.1f, 0.1f, 0.1f));
                //tree[i].setPointLight(lampu._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.2f, 0.2f, 0.2f), 1.0f, 0.09f, 0.032f);
                tree[i].setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.1f, 0.1f, 0.1f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                tree[i].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.1f, 0.1f, 0.1f), 1.0f, 0.09f, 0.032f);
            }
            pole.OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            pole.setFragVariable(new Vector3(0f, 0f, 0.0f), _camera.Position);
            pole.setDirectionalLight(new Vector3(0, -50, 0), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.1f, 0.1f, 0.1f));
            //roof.setPointLight(lampu._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0.2f, 0.2f, 0.2f), 1.0f, 0.09f, 0.032f);
            pole.setSpotLight(character[0]._centerPosition, _camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.1f, 0.1f, 0.1f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            pole.setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);

            /*for (int i = 0; i < 4; i++)
            {
                LightObjects[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            }*/
            moon.OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            //lampu.OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            for (int i = 0; i < 2; i++)
            {
                LightObjects[i].OnRender(0, _time, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            }
            
            cubemap.Render(_camera);
            SwapBuffers();
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            _camera.Fov = _camera.Fov - e.OffsetY;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            _camera.AspectRatio = Size.X / (float)Size.Y;
        }
        
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            var input = KeyboardState;
            float cameraSpeed = 3f;
            bed[0].minMax();
            //character.minMax();
            Vector3 _objectPos = new Vector3(character[0]._centerPosition.X, character[0]._centerPosition.Y, character[0]._centerPosition.Z);
            
            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }



            if (KeyboardState.IsKeyDown(Keys.W))
            {             
                if (move == true)
                {
                    _camera.Position += _camera.Front * cameraSpeed * (float)args.Time;
                    for (int i = 0; i < 6; i++)
                    {
                        character[i].translate2(_camera.Front * cameraSpeed * (float)args.Time);
                    }
                }
                else
                {
                    _camera.Position += _camera.Front * cameraSpeed * (float)args.Time;
                }
                _camera.Pitch = 0;
            }
            if (KeyboardState.IsKeyDown(Keys.S))
                {
                if (move == true)
                {
                    _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time;
                    for (int i = 0; i < 6; i++)
                    {
                        character[i].translate2(-_camera.Front * cameraSpeed * (float)args.Time);
                    }
                }
                else
                {
                    _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time;
                }
                
                _camera.Pitch = 0;

            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                
                if (move == true)
                {
                    _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time;
                    for (int i = 0; i < 6; i++)
                    {

                        character[i].translate2(-_camera.Right * cameraSpeed * (float)args.Time);
                    }
                }
                else
                {
                    _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time;
                }
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                if (move == true)
                {
                    _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;
                    for (int i = 0; i < 6; i++)
                    {

                        character[i].translate2(_camera.Right * cameraSpeed * (float)args.Time);
                    }
                }
                else
                {
                    _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;
                }

            }
            if (KeyboardState.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)args.Time;
                for (int i = 0; i < 6; i++)
                {
                    character[i].translate2(_camera.Up * cameraSpeed * (float)args.Time);
                }
                }
            if (KeyboardState.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)args.Time;
                for (int i = 0; i < 6; i++)
                {
                    character[i].translate2(-_camera.Up * cameraSpeed * (float)args.Time);
                }
            }

            if (KeyboardState.IsKeyPressed(Keys.G))
            {
                move = !move;
            }
            var mouse = MouseState;
            var sensitivity = 0.2f;

            //if (_firstMove)
            //{
            //    _lastPos = new Vector2(mouse.X, mouse.Y);
            //    _firstMove = false;
            //}
            //else
            //{
            //    var deltaX = mouse.X - _lastPos.X;
            //    var deltaY = mouse.Y - _lastPos.Y;
            //    _lastPos = new Vector2(mouse.X, mouse.Y);
            //    _camera.Yaw += deltaX * sensitivity;
            //    _camera.Pitch -= deltaY * sensitivity;
            //}

            if (KeyboardState.IsKeyDown(Keys.N))
            {
                var axis = new Vector3(0,1, 0);
                _camera.Position -= _objectPos;
                _camera.Yaw -= _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,generateArbRotationMatrix(axis, _objectPos, -_rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;
                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                for (int i = 0; i < 6; i++)
                {
                    character[i].rotate(_objectPos, Vector3.UnitY, _rotationSpeed);
                }
                }
            if (KeyboardState.IsKeyDown(Keys.Comma))
            {
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _objectPos;
                _camera.Yaw += _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position, generateArbRotationMatrix(axis, _objectPos, _rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;
                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                for (int i = 0; i < 6; i++)
                {
                    character[i].rotate(_objectPos, Vector3.UnitY, -_rotationSpeed);
                }
                }
            /*if (KeyboardState.IsKeyDown(Keys.K))
            {
                var axis = new Vector3(1, 0, 0);
                _camera.Position -= _objectPos;
                _camera.Pitch -= _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,generateArbRotationMatrix(axis, _objectPos, _rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;
                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
               //character.rotate(character._centerPosition, Vector3.UnitX, -0.05f);
            }
            if (KeyboardState.IsKeyDown(Keys.M))
            {
                var axis = new Vector3(1, 0, 0);
                _camera.Position -= _objectPos;
                _camera.Pitch += _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, -_rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;
                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                //character.rotate(character._centerPosition, Vector3.UnitX, 0.05f);
            }*/
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButton.Left)
            {
                float _x = (MousePosition.X - Size.X / 2) / (Size.X / 2);
                float _y = -(MousePosition.Y - Size.Y / 2) / (Size.Y / 2);

                Console.WriteLine("x = " + _x + "y = " + _y);
                //_object[1].updateMousePotition(_x, _y);
            }
        }
        
    }
}
