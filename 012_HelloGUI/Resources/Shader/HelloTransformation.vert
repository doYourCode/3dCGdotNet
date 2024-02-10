#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec3 aColor;
layout(location = 2) in vec2 aUv;

out vec3 vColor;
out vec2 vUv;

uniform mat4 model;

void main(void)
{
    vUv = aUv;
    vColor = aColor;
    gl_Position = vec4(aPosition, 1.0) * model; // Multiplica pelo matriz model
}