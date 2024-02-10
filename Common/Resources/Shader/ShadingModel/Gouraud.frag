#version 330 core

in vec3 vColor;
in vec2 vUv;
in vec4 vNormal;
in vec4 fragPosition;

out vec4 outputColor;

uniform sampler2D texture0;

uniform vec3 lightPosition;
uniform vec3 lightDirection;
uniform vec3 lightColor;
uniform vec3 viewPosition;

void main()
{

    // Diffuse lighting
    vec3 norm = normalize(vNormal.xyz);
    vec3 lightDir = normalize(lightPosition - fragPosition.xyz);
    float diffuse = max(dot(norm, lightDir), 0.0);

    // Specular lighting
    float specularStrength = 0.24;
    vec3 viewDir = normalize(-viewPosition - fragPosition.xyz);
    vec3 reflectDir = reflect(lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 9);

    outputColor = vec4(1.0, 1.0, 0.0, 1.0) * diffuse + spec * specularStrength;
}