#version 330 core

in vec3 vColor;
in vec2 vUv;

out vec4 outputColor;

uniform sampler2D texture0;

void main()
{
    outputColor = texture(texture0, vUv);
}