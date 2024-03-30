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
uniform float lightIntensity;
uniform vec3 ambientColor;
uniform float ambientIntensity;
uniform vec3 viewPosition;

vec3 DiffuseLighting(vec3 normal);
float SpecularLighting(vec3 normal);

void main()
{
    vec3 normal = normalize(vNormal.xyz);

    vec3 diffuseLight = DiffuseLighting(normal);

    vec3 ambientLight = ambientColor * ambientIntensity;

    float specularLight = SpecularLighting(normal);

    //vec3 textureColor = texture(texture0, vUv).xyz;

    vec3 finalLight = diffuseLight + ambientLight;

    outputColor = vec4(finalLight, 1.0);
}

vec3 DiffuseLighting(vec3 normal)
{
    vec3 lightDir = normalize(lightPosition - fragPosition.xyz);
    float diffuseTerm = max(dot(normal, lightDir), 0.0);
    return diffuseTerm * lightColor * lightIntensity;
}