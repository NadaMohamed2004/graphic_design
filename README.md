# 3D Graphics Project with OpenGL

## Project Overview
This project demonstrates 3D rendering using OpenGL in C#. It involves drawing a 3D scene with various colored objects, applying transformations, and integrating textures. The project is structured into three milestones:

### Milestone 1: Drawing a 3D Object
- Render a 3D scene using OpenGL.
- Include at least five different primitive shapes (e.g., cubes, pyramids, rectangles, etc.).
- Assign different colors to different parts of the scene.
- Ensure at least one quad (square or rectangle) is present.
- Adjust the camera to properly capture the scene.

### Milestone 2: Applying Transformations
- Implement transformations that can be controlled via the keyboard.
- Apply self-animated transformations that run automatically without disappearing from view.
- Example transformations include translation (left, right, up, down) and scaling (up and down).

### Milestone 3: Applying Textures
- Apply a texture to a quad in the scene.
- Maintain a mix of colored and textured surfaces.
- Use an image file as the texture source.

## Technologies Used
- **Programming Language:** C#
- **Graphics Library:** OpenGL with Tao.OpenGl
- **Math Library:** GlmNet
- **Shader Programming:** GLSL (Vertex and Fragment shaders)
- **Image Processing:** Bitmap textures

## Project Structure
```
Graphics/
│── Shaders/
│   ├── SimpleVertexShader.vertexshader
│   ├── SimpleFragmentShader.fragmentshader
│── Textures/
│   ├── images.jpg
│── Renderer.cs
│── Program.cs
│── Graphics.csproj
│── README.md
```

## Setup Instructions
1. Clone the repository:
   ```sh
   git clone <repository_url>
   ```
2. Open the project in Visual Studio.
3. Ensure the required dependencies (Tao.OpenGl, GlmNet) are installed.
4. Run the project to visualize the 3D scene.

## Controls
- **Arrow Keys:** Move the object left, right, up, or down.
- **Scaling:** Object scales up and down automatically.

## Features Implemented
✔ 3D object rendering with OpenGL
✔ Multiple primitive shapes
✔ Colored scene elements
✔ Camera adjustment
✔ Keyboard-controlled transformations
✔ Self-animated transformations
✔ Texture mapping on quads

## Future Enhancements
- Implement lighting effects for realism.
- Add more complex models using external 3D files.
- Improve user interaction with additional controls.

## Author
**Nada Mohamed Khalil**

