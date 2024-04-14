#version 420 core

out vec4 FragColor;
in vec2 texCoords;

//layout (binding = 0) uniform sampler2D fboTexture;

uniform sampler2D fboTexture;
uniform float gamma;

void main()
{
    vec4 fragment = texture(fboTexture, texCoords);
    FragColor = vec4(fragment);
}