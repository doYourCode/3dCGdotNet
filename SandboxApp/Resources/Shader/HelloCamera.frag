#version 420 core

in vec3 vColor;
in vec2 vUv;

out vec4 outputColor;

layout (binding = 3) uniform sampler2D diffuseTexture;

void main()
{
    outputColor = texture(diffuseTexture, vUv);
}