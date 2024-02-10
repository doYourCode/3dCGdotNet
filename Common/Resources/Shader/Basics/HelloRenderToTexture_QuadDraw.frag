#version 330 core

in vec2 vUv;

out vec3 fragColor;

uniform sampler2D texture1;
uniform float time;

void main(){
    fragColor = texture( texture1, vUv + 0.005*vec2( sin(time+1024.0*vUv.x),cos(time+768.0*vUv.y)) ).xyz;
}