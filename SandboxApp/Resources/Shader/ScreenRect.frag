#version 420 core

in vec3 vColor;
in vec2 vUv;

out vec4 outputColor;

//layout(binding = 0) uniform sampler2D texture0;
//layout(binding = 1) uniform sampler2D texture1;

uniform sampler2D screenTexture;

void main()
{
    outputColor = texture(screenTexture, vUv);
}