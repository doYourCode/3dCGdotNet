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

uniform float roughness;
uniform float specularIntensity;
uniform float specularPower;
uniform vec3 specularColor;

float SpecularTerm(vec3 ViewDirection, vec3 LightDirection, vec3 NormalDirection)
{
    vec3 reflectDir = reflect(LightDirection, NormalDirection);
    float NdotV = dot(ViewDirection, reflectDir);

    return pow(max(NdotV, 0.0), specularPower);
}

float LambertTerm(vec3 LightDirection, vec3 NormalDirection)
{
    float NdotL = dot(NormalDirection, LightDirection);

    return max(NdotL, 0.0);
}

void main()
{
    vec3 lightDir = normalize(lightDirection);
    vec3 viewDir = normalize(-viewPosition - fragPosition.xyz);
    vec3 normalDir = normalize(vNormal.xyz);

    vec3 diffuseLight = LambertTerm(lightDir, normalDir) * lightColor * lightIntensity;

    vec3 ambientLight = ambientColor * ambientIntensity;

    vec3 specularLight = specularColor * (SpecularTerm(viewDir, lightDir, normalDir) * specularIntensity * lightIntensity);

    vec3 textureColor = texture(texture0, vUv).xyz;

    vec3 finalLight = (diffuseLight + ambientLight) * textureColor + specularLight;

    outputColor = vec4(finalLight, 1.0);
}
