#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 2) in vec2 aUv;

out vec3 vColor;
out vec2 vUv;

void main(void)
{
    vUv = aUv;
    gl_Position = vec4(aPosition, 1.0);
}