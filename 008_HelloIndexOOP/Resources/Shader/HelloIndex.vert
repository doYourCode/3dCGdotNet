#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 4) in vec3 aColor;
layout(location = 1) in vec2 aUv;

out vec3 vColor;
out vec2 vUv;

void main(void)
{
    vUv = aUv;
    vColor = aColor;
    gl_Position = vec4(aPosition, 1.0);
}