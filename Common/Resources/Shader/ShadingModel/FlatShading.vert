#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec3 aColor;
layout(location = 2) in vec2 aUv;
layout(location = 3) in vec3 aNormal;

out vec3 vColor;
out vec2 vUv;
flat out vec4 vNormal;
out vec4 fragPosition;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main(void)
{
    vUv = aUv;
    vColor = aColor;
    vNormal = vec4(aNormal, 1.0) * model;
    fragPosition = vec4(aPosition, 1.0) * model;
    gl_Position = fragPosition * view * projection;
}