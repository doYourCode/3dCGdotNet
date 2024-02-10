#version 330 core

in vec2 vUv;

out vec4 outputColor;

uniform sampler2D texture0;

uniform float time;
uniform vec2 mousePosition;

void main()
{
    vec4 texColor = texture(texture0, vUv);
    vec4 uniformColor = vec4(abs(sin(time)), mousePosition.x, mousePosition.y, 1.0);
    outputColor = mix(texColor, uniformColor, 0.5);
}