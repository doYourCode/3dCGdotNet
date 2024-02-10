#version 330 core

layout(location = 0) in vec3 aPosition;

uniform float tick;

void main(void)
{
    gl_Position = vec4(aPosition, abs(cos(tick) * 2.0));
}