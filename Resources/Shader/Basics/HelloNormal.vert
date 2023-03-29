#version 400 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec3 aColor;
layout(location = 2) in vec2 aUv;
layout(location = 3) in vec3 aNormal;
layout(location = 4) in vec3 aTangent;

out vec3 vColor;
out vec2 vUv;
out vec3 TangentLightPos;
out vec3 TangentViewPos;
out vec3 TangentFragPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

uniform vec3 lightPosition;
uniform vec3 viewPosition;

void main(void)
{
    vUv = aUv;
    vColor = aColor;

    vec3 T = normalize(vec3(model * vec4(aTangent, 0.0)));
    vec3 N = normalize(vec3(model * vec4(aNormal, 0.0)));
    // re-orthogonalize T with respect to N
    T = normalize(T - dot(T, N) * N);
    // then retrieve perpendicular vector B with the cross product of T and N
    vec3 B = cross(N, T);
    
    mat3 TBN = transpose(mat3(T, B, N));

    TangentLightPos = TBN * lightPosition;
    TangentViewPos  = TBN * viewPosition;
    TangentFragPos  = TBN * vec3(model * vec4(aPosition, 1.0));

    gl_Position = vec4(aPosition, 1.0) * model * view * projection;
}