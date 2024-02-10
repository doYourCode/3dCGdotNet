#version 330 core

in vec3 vColor;

out vec4 outputColor;

void main()
{
    outputColor = vec4(vColor, 1.0);
}