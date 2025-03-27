/*
 Nada Mohamed Khalil Mohamed
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;
using GlmNet;
using System.IO;

using System.IO;
using System.Diagnostics;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;





namespace Graphics
{
    class Renderer
    {
        Shader sh;
        
        uint boxBufferID;
        uint shapesBufferID;
        uint wallgroundBufferID;
        uint xyzAxesBufferID;
      
        //3D Drawing
        mat4 ModelMatrix;
        mat4 ViewMatrix;
        mat4 ProjectionMatrix;

        Texture ground;
      

        int ShaderModelMatrixID;
        int ShaderViewMatrixID;
        int ShaderProjectionMatrixID;

        const float rotationSpeed = 1f;
        float rotationAngle = 0;

        public float translationX=0, 
                     translationY=0, 
                     translationZ=0;

        Stopwatch timer = Stopwatch.StartNew();

        vec3 boxCenter;
        

        public void Initialize()
        {
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader", projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");
            Gl.glClearColor(6.0f, 0.60f, 0.90f, 1);
          
            ground = new Texture(projectPath + "\\Textures\\images.jpg", 1);

            float[] wallgroundVertices = {
             //ground
             0.0f, 0.0f, 0.0f, .0f, 0.0f, 0.0f,  0,0,
             100.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 4,0,
             100.0f, 100.0f, 0.0f, 0.0f, 0.0f, 0.0f,  4,4,
             0.0f, 100.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0,4,

             //wall
             0.0f, 0.0f, 0.0f, 6.0f, 0.60f, 0.80f,0,0,
             0.0f, 100.0f, 0.0f,6.0f, 0.60f, 0.80f,0,0,
             0.0f, 100.0f, 100.0f,6.0f, 0.60f, 0.80f,0,0,
             0.0f, 0.0f, 100.0f, 6.0f, 0.60f, 0.80f,0,0,

            };


            float[] boxVertices = { 
		        // box>bottm >lnestrip  
		    	0.0f, 0.0f, 0.0f,1.0f, 0.80f, 0.80f, //0

                30.0f, 0.0f, 0.0f,1.0f, 0.80f, 0.80f, //R
                30.0f, 30.0f, 0.0f ,1.0f, 0.80f, 0.80f,
                0.0f, 30.0f, 0.0f,1.0f, 0.80f, 0.80f, //B

             //   //line
	            0.0f, 0.0f, 0.0f, 1.0f, 0.80f, 0.70f,//4
                30.0f, 30.0f, 0.0f, 1.0f, 0.80f, 0.70f,

                //box2>top >polygon
                0.0f, 0.0f, 30.0f,  1.0f, 0.70f, 0.30f, //R  //6
                30.0f, 0.0f, 30.0f, 1.0f, 0.70f, 0.30f, //R
                30.0f, 30.0f, 30.0f, 1.0f, 0.70f, 0.30f,
                0.0f, 30.0f, 30.0f,  1.0f, 0.70f, 0.30f,

          //      //box3>>Quads
                0.0f, 0.0f, 0.0f,1.0f, 0.20f, 0.40f, //6
		        0.0f, 0.0f, 30.0f,1.0f, 0.20f, 0.40f, //R
                30.0f, 0.0f, 30.0f, 1.0f, 0.50f, 0.40f,
                30.0f, 0.0f, 0.0f, 1.0f, 0.50f, 0.40f,

                //box4>>lineloop
                0.0f, 0.0f, 0.0f, 1.0f, 0.50f, 0.50f, //R  //ten
		        0.0f, 0.0f, 30.0f, 1.0f, 0.50f, 0.50f,
                0.0f, 30.0f, 30.0f,  1.0f, 0.2f, 0.40f,
                0.0f, 30.0f, 0.0f, 1.0f, 0.2f, 0.40f,
                //box5>>polygon
                0.0f, 30.0f, 30.0f,1.0f, 0.20f, 0.40f,//14
                0.0f, 30.0f, 0.0f,1.0f, 0.20f, 0.40f,
                30.0f, 30.0f, 0.0f, 0.40f, 0.50f, 0.40f,
                30.0f, 30.0f, 30.0f, 0.40f, 0.50f, 0.40f,
                //box6>>lineloop
                30.0f, 30.0f, 0.0f, 0.20f, 0.5f, 0.20f,//15
                30.0f, 30.0f, 30.0f, 0.20f, 0.50f, 0.20f,
                30.0f, 0.0f, 30.0f, 1.0f, 0.20f, 0.40f,
                30.0f, 0.0f, 0.0f, 1.0f, 0.20f, 0.40f,


            };

            float[] ShapesVertices = {
                30.0f, 30.0f, 0.0f, 0.0f, 0.0f, 0.0f,
                30.0f, 0.0f, 30.0f, 0.0f, 0.0f, 0.0f,
                30.0f, 30.0f, 30.0f, 0.0f, 0.0f, 0.0f,
                30.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,

                15.0f,  0.0f,  25.0f,  0.0f, 0.0f, 0.0f, // P1
                23.660f, 0.0f,  20.0f, 0.0f, 0.0f, 0.0f, // P2
                23.66f, 0.0f,  10.0f,  0.0f, 0.0f, 0.0f, // P3
                15.0f, 0.0f,  5.0f,  0.0f, 0.0f, 0.0f,  // P4
                6.34f,  0.0f,  10.0f,0.0f, 0.0f, 0.0f, // P5
               6.34f,  0.0f,  20.0f,0.0f, 0.0f, 0.0f,


                10.0f, 30.0f, 20.0f, 0.0f, 0.0f, 0.0f,
                10.0f, 30.0f, 10.0f,  0.0f, 0.0f, 0.0f,
                20.0f, 30.0f, 10.0f,  0.0f, 0.0f, 0.0f,
                20.0f, 30.0f, 20.0f,  0.0f, 0.0f, 0.0f,
                10.0f, 30.0f, 20.0f, 0.0f, 0.0f, 0.0f,



               0.0f, 10.0f, 5.0f, 0.0f, 0.0f, 0.0f,
               0.0f, 20.0f, 5.0f, 0.20f, 0.20f, 0.20f,
               0.0f, 15.0f, 15.0f,0.50f, 0.50f, 0.70f,
            };
             boxCenter = new vec3(10, 7, -5);
           // boxCenter = new vec3(20, 14, 0);
            float[] xyzAxesVertices = {
		        //x
		        0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, //R
		        100.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, //R
		        //y
	           0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, //G
		       0.0f, 100.0f, 0.0f, 0.0f, 1.0f, 0.0f, //G
		        //z
	            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f,  //B
		       0.0f, 0.0f, 100.0f, 0.0f, 0.0f, 1.0f,  //B
            };

            wallgroundBufferID = GPU.GenerateBuffer(wallgroundVertices);
            boxBufferID = GPU.GenerateBuffer(boxVertices);
            shapesBufferID = GPU.GenerateBuffer(ShapesVertices);
            xyzAxesBufferID = GPU.GenerateBuffer(xyzAxesVertices);

            // View matrix 
            ViewMatrix = glm.lookAt(
                        new vec3(50, 50, 50), // Camera is at (0,5,5), in World Space
                        new vec3(0, 0, 0), // and looks at the origin
                        new vec3(0, 0, 1)  // Head is up (set to 0,-1,0 to look upside-down)
                );
            // Model Matrix Initialization
            ModelMatrix = new mat4(1);

            //ProjectionMatrix = glm.perspective(FOV, Width / Height, Near, Far);
            ProjectionMatrix = glm.perspective(45.0f, 4.0f / 3.0f, 0.1f, 100.0f);
            
            // Our MVP matrix which is a multiplication of our 3 matrices 
            sh.UseShader();


            //Get a handle for our "MVP" uniform (the holder we created in the vertex shader)
            ShaderModelMatrixID = Gl.glGetUniformLocation(sh.ID, "modelMatrix");
            ShaderViewMatrixID = Gl.glGetUniformLocation(sh.ID, "viewMatrix");
            ShaderProjectionMatrixID = Gl.glGetUniformLocation(sh.ID, "projectionMatrix");

            Gl.glUniformMatrix4fv(ShaderViewMatrixID, 1, Gl.GL_FALSE, ViewMatrix.to_array());
            Gl.glUniformMatrix4fv(ShaderProjectionMatrixID, 1, Gl.GL_FALSE, ProjectionMatrix.to_array());
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());
            timer.Start();
        }


        public void Draw()
        {
            sh.UseShader();
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            #region XYZ axis
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, xyzAxesBufferID);

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, new mat4(1).to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
             
            Gl.glDrawArrays(Gl.GL_LINES, 0, 6);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);



            #endregion

            #region wallground
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, wallgroundBufferID);
            
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());

            //  Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));

            ground.Bind();
            Gl.glDrawArrays(Gl.GL_QUADS, 0, 8);


            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            Gl.glDisableVertexAttribArray(2);

            #endregion

            #region squaer
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, boxBufferID);
           
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());

            //  Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 4);//WHITE

            Gl.glDrawArrays(Gl.GL_POLYGON, 6, 4);//BROWEN
            Gl.glDrawArrays(Gl.GL_QUADS, 10, 4);//RED
            Gl.glDrawArrays(Gl.GL_POLYGON, 14, 4);//PINK
            Gl.glDrawArrays(Gl.GL_QUADS, 18, 4);//DARKPINK
            Gl.glDrawArrays(Gl.GL_POLYGON, 22, 4);//GREEN

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion

            #region shapes
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, shapesBufferID);
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());

            //  Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            Gl.glDrawArrays(Gl.GL_LINES, 0, 2 * 2);//black lines

            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 4, 6);//shakl 6 

            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 10, 5);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 15, 3);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion



            #region Animated box
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, boxBufferID);

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());

            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            Gl.glDrawArrays(Gl.GL_QUADS, 0, 4);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion


        }

        public void Update()
        {
            timer.Stop();
            var deltaTime = timer.ElapsedMilliseconds / 1000.0f;
            rotationAngle += deltaTime * rotationSpeed;

            List<mat4> transformations = new List<mat4>();
            transformations.Add(glm.translate(new mat4(1), -1 * boxCenter));
            transformations.Add(glm.rotate(rotationAngle, new vec3(0, 0, 1)));
            transformations.Add(glm.translate(new mat4(1), boxCenter));
            transformations.Add(glm.translate(new mat4(1), new vec3(translationX, translationY, translationZ)));

            ModelMatrix = MathHelper.MultiplyMatrices(transformations);

            timer.Reset();
            timer.Start();
        }
        public void onkeyboard(char key)
        {
            float speed = 5;
            if (key == 'd')
                translationX += rotationSpeed;
            if (key == 'a')
                translationX -= rotationSpeed;
            if (key == 'w')
                translationY += rotationSpeed;
            if (key == 's')
                translationY -= rotationSpeed;
            if (key == 'z')
                translationZ += rotationSpeed;
            if (key == 'c')
                translationZ -= rotationSpeed;

            //if (key == 'n')
            //{
            //    scale.x /= 1.1f;
            //    scale.y /= 1.1f;
            //    scale.z /= 1.1f;
            //}

            //if (key == 't')
            //{
            //    scale.x *= 1.1f;
            //    scale.y *= 1.1f;
            //    scale.z *= 1.1f;
            //}

        }


        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}
