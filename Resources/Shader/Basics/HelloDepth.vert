#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec3 aColor;
layout(location = 2) in vec2 aUv;
layout(location = 3) in vec3 aNormal;
layout(location = 4) in vec3 aTangent;

out VS_OUT
{
    vec3 color;
    vec2 uv;
    vec3 tangentLightPos;
    vec3 tangentViewPos;
    vec3 tangentFragPos;
} vs_out;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

uniform vec3 lightPosition;
uniform vec3 viewPosition;

void main(void)
{
    vec3 fragPosition = vec3(model * vec4(aPosition, 1.0));
    vs_out.uv = aUv;
    vs_out.color = aColor;

    mat3 normalMatrix = transpose(inverse(mat3(model)));
    vec3 T = normalize(vec3(normalMatrix * aTangent));
    vec3 N = normalize(vec3(normalMatrix * aNormal));
    // re-orthogonalize T with respect to N
    T = normalize(T - dot(T, N) * N);
    // then retrieve perpendicular vector B with the cross product of T and N
    vec3 B = cross(N, T);
    
    mat3 TBN = transpose(mat3(T, B, N));

    vs_out.tangentLightPos = TBN * lightPosition;
    vs_out.tangentViewPos  = TBN * viewPosition;
    vs_out.tangentFragPos  = TBN * fragPosition;

    gl_Position = vec4(fragPosition, 1.0) * view * projection ;
}